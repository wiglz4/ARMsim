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
            List<ELFTWO> programHeaders = new List<ELFTWO>();

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

            //initiate ram and size
            RAMsim myRAM = new RAMsim(myOptions.getMemSize());
            
            //COMPLIMENT OF J
            //opens given filename and identifies key elements 
            string elfFilename = myOptions.getfileName();
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
                    myRAM.PopulateRam(data, toRam.p_vaddr);
                }

                Debug.WriteLine(myRAM.getMDF());
          
                // Now, do something with it ... see cppreadelf for a hint
                //TO HERE
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

        public int getMemSize() 
        {
            return memSize;
        }

        public string getfileName() 
        {
            return fileName;
        }

        public bool getTest() 
        {
            return test;
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

        public uint ReadWord(int addr)
        {
            //take some data, put into uint. flip bits around using int.
            return 5;
        }

        public void WriteWord(uint word, int addr)
        {
            
        }

        public void ReadHalfWord()
        {

        }

        public void WriteHalfWord()
        {

        }

        public void ReadByte()
        {

        }

        public void WriteByte()
        {
            //a change
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
        public bool TestFlag(int addr, int bit) 
        {
            uint word = ReadWord(addr);
            word = word >> bit;
            return (word & 1)==1 ? true : false;
        }
        //needs testing
        public void SetFlag(int addr, int bit, bool flag)
        {
            //logic for setting to 1
            uint bitLoc = 1;
            uint word = ReadWord(addr);
            bitLoc = bitLoc << bit;
            word = word ^ bitLoc;
            WriteWord(word, addr);

            //need logic for setting it to 0
        }
    }
}
