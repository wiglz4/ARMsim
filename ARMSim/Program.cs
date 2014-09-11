using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace ARMSim
{
    // A struct that mimics memory layout of ELF file header
    // See http://www.sco.com/developers/gabi/latest/contents.html for details
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ELF
    {
        public byte EI_MAG0, EI_MAG1, EI_MAG2, EI_MAG3, EI_CLASS, EI_DATA, EI_VERSION;
        byte unused1, unused2, unused3, unused4, unused5, unused6, unused7, unused8, unused9;
        public ushort e_type;
        public ushort e_machine;
        public uint e_version;
        public uint e_entry;
        public uint e_phoff;
        public uint e_shoff;
        public uint e_flags;
        public ushort e_ehsize;
        public ushort e_phentsize;
        public ushort e_phnum;
        public ushort e_shentsize;
        public ushort e_shnum;
        public ushort e_shstrndx;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ELFTWO 
    {
        public uint p_type;
        public uint p_offset;
        public uint p_vaddr;
        public uint p_paddr;
        public uint p_filesz;
        public uint p_memsz;
        public uint p_flags;
        public uint p_align;
    } 


    class Program
    {
        static void Main(string[] args)
        {
            Options myOptions= new Options();
            if (args.Length > 1)
            {
                myOptions.Parse(args);
            }
            else
            {
                Console.WriteLine("YOU DONE MESSED UP A-A-RON!!");
                Environment.Exit(0);
            }

            if (myOptions.GetTest())
            {
                TestRAM.RunTests();
                TestLoader.RunTests(myOptions);
                Environment.Exit(1);
            }

            RAMsim myRam = new RAMsim(myOptions.GetMemSize());
            Loader myLoader = new Loader(myOptions, myRam);
            myLoader.Load();
        }
    }

    class Options
    {
        private int memSize = 32768;
        private string fileName;
        private bool test;

        public void Parse(string[] args)
        {
            //iterate through command line arguments and fill out variables
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--load":
                        //need more error testing
                        fileName = args[i + 1];
                        i++;
                        break;

                    case "--mem":
                        memSize = Convert.ToInt32(args[i + 1]);
                        i++;
                        break;

                    case "--test":
                        //run unit tests and quit
                        test = true;
                        break;

                    default:
                        Console.WriteLine("You have entered an invalid option \n Valid options are: \n --test: run unit tests and quit \n --mem: specify simluated RAM size \n --load: specify ELF file to open \n");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public int GetMemSize() 
        {
            return memSize;
        }

        public string GetFileName() 
        {
            return fileName;
        }

        public bool GetTest() 
        {
            return test;
        }

        public void SetFileName(string toFileName)
        {
            fileName = toFileName;
        }
    }

    class RAMsim
    {
        private byte[] ram;

        public RAMsim(int memsize) 
        {
            ram = new byte[memsize];
        }

        public void PopulateRam(byte[] toRam, uint loc)
        {
            for (int i = 0; i < toRam.Length; i++)
            {
                ram[loc] = toRam[i];
                loc++;
            }
        }

        public uint ReadWord(uint addr)
        {
            return BitConverter.ToUInt32(ram, (int)addr);   
        }

        public void WriteWord(uint addr, uint toRam)
        {
            int counter = 0;
            byte[] toRamBA = BitConverter.GetBytes(toRam);
            foreach (byte x in toRamBA)
            {
                ram[addr + counter] = x;
                counter++;
            }            
        }

        public uint ReadHalfWord(uint addr)
        {
            return BitConverter.ToUInt16(ram, (int)addr);
        }

        public void WriteHalfWord(uint addr, short toRam)
        {
            int counter = 0;
            byte[] toRamBA = BitConverter.GetBytes(toRam);
            foreach (byte x in toRamBA)
            {
                ram[addr + counter] = x;
                counter++;
            }
        }

        public byte ReadByte(uint addr)
        {
            return ram[addr];
        }

        public void WriteByte(uint addr, byte toRam)
        {
            ram[addr] = toRam;
        }

        public string getMDF()
        {
            byte[] mdf = new MD5CryptoServiceProvider().ComputeHash(ram);
            StringBuilder toString = new StringBuilder();
            for(int i = 0; i < mdf.Length; i++)
            {
                toString.Append(mdf[i].ToString("x2"));
            }
            //return hex string
            return toString.ToString();
        }

        //needs testing
        public bool TestFlag(uint addr, int bit) 
        {
            uint word = ReadWord(addr);
            word = word >> bit;
            return (word & 1)==1 ? true : false;
        }
        //needs testing
        public void SetFlag(uint addr, int bit, bool flag)
        {
            uint bitLoc = 1;
            uint word = ReadWord(addr);
            bitLoc = bitLoc << bit;

            if (flag)
            {
                //logic for setting to 1
                word = word ^ bitLoc;
            }
            else
            {
                //logic for setting to 0
                bitLoc = ~(bitLoc);
                word = word & bitLoc;
            }

            WriteWord(addr, word);
        }
    }

    class Loader
    {
        private Options myOptions;
        private RAMsim myRam;
        List<ELFTWO> programHeaders = new List<ELFTWO>();

        public Loader(Options toMyOptions, RAMsim toMyRam)
        {
            myOptions = toMyOptions;
            myRam = toMyRam;
        }

        public void Load()
        {
            //COMPLIMENT OF J
            //opens given filename and identifies key elements 
            string elfFilename = myOptions.GetFileName();
            using (FileStream strm = new FileStream(elfFilename, FileMode.Open))
            {
                ELF elfHeader = new ELF();
                byte[] data = new byte[Marshal.SizeOf(elfHeader)];

                // Read ELF header data
                strm.Read(data, 0, data.Length);
                // Convert to struct
                elfHeader = ByteArrayToStructure<ELF>(data);

                Console.WriteLine("Entry point: " + elfHeader.e_entry.ToString("X4"));
                Console.WriteLine("Number of program header entries: " + elfHeader.e_phnum);


                // Read program header entries into programHeaders
                strm.Seek(elfHeader.e_phoff, SeekOrigin.Begin);
                for (int i = 0; i < elfHeader.e_phnum; i++)
                {
                    data = new byte[elfHeader.e_phentsize];
                    strm.Read(data, 0, (int)elfHeader.e_phentsize);
                    programHeaders.Add(ByteArrayToStructure<ELFTWO>(data));
                }

                //read memory from program header and send to 
                foreach (ELFTWO toRam in programHeaders)
                {
                    data = new byte[toRam.p_memsz];
                    strm.Seek(toRam.p_offset, SeekOrigin.Begin);
                    strm.Read(data, 0, (int)toRam.p_memsz);
                    myRam.PopulateRam(data, toRam.p_vaddr);
                }

                //use for testing MD5 HASH
                //Debug.WriteLine(myRam.getMDF());

            }
        }

        //COMPLEMENTS OF J
        // Converts a byte array to a struct
        static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(),
                typeof(T));
            handle.Free();
            return stuff;
        }
    }

    //BEGIN UNIT TESTING
    class TestLoader
    {
        public static void RunTests(Options myOptions) 
        {
            myOptions.SetFileName("test1.exe");
            RAMsim myRam = new RAMsim(myOptions.GetMemSize());
            Loader myLoader = new Loader(myOptions, myRam);
            Debug.WriteLine("testing Loader...");
            myLoader.Load();
            Debug.Write("verifying MD5 hash...");
            Debug.Assert(myRam.getMDF() == "3500a8bef72dfed358b25b61b7602cf1");
            Debug.WriteLine("success!");
        }

    }

    class TestRAM
    {
        public static void RunTests()
        {
            Debug.WriteLine("testing RAM...");
           
            RAMsim ram = new RAMsim(7);
            
            //test populate method
            Debug.Write("verifying Populate...");
            ram.PopulateRam(new byte[] { 0xFF, 0xAA, 0xAA, 0xFF, 0xFF, 0xFF, 0xFF }, 0);
            Debug.Assert(ram.ReadHalfWord(1) == 0xAAAA);
            Debug.WriteLine("success!");

            //test set/test flag methods
            Debug.Write("verifying SetFlag/TestFlag...");
            ram.SetFlag(1, 6, true);
            Debug.Assert(ram.TestFlag(1, 6));
            ram.SetFlag(1, 6, false);
            Debug.Assert(!(ram.TestFlag(1, 6)));
            Debug.WriteLine("success!");

            //test read/write methods
            Debug.Write("verifying Read/Write Word...");
            ram.WriteWord(0, 0xFFFFFFFF);
            Debug.Assert(ram.ReadWord(0) == 0xFFFFFFFF);
            Debug.WriteLine("success!");

            Debug.Write("verifying Read/Write HalfWord...");
            ram.WriteWord(0, 0xCCCC);
            Debug.Assert(ram.ReadWord(0) == 0xCCCC);
            Debug.WriteLine("success!");

            Debug.Write("verifying Read/Write Byte...");
            ram.WriteWord(0, 0xAA);
            Debug.Assert(ram.ReadWord(0) == 0xAA);
            Debug.WriteLine("success!");
         }

    }
}
