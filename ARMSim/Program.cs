﻿using System;
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

    //Class:        Program
    //Purpose:      Accepts user input then initiates classes and runs program.
    class Program
    {
        //Method:       Main
        //Purpose:      Accepts user input then initiates classes and runs program.
        //Variables:    args -   String array of command line arguments.
        static void Main(string[] args)
        {
            Options myOptions = new Options();
            myOptions.Parse(args);

            if (myOptions.GetTest())
            {
                TestRAM.RunTests();
                TestLoader.RunTests(myOptions);
                Environment.Exit(0);
            }

            RAMsim myRam = new RAMsim(myOptions.GetMemSize());
            Loader myLoader = new Loader(myOptions, myRam);
            myLoader.Load();
        }
    }

    //Class:        Options
    //Purpose:      Parses user input and stores relevant information in an Option object.
    class Options
    {
        private int memSize = 32768;
        private string fileName;
        private bool test;

        //Method:       Parse
        //Purpose:      Parses user input and stores relevant information in an Option object.
        //Variables:    args -   String array of command line arguments.
        public void Parse(string[] args)
        {
            //iterate through command line arguments and fill out variables
            Debug.WriteLine("Options.Parse: parsing command line options");

            if (args.Length < 1)
            {
                Console.WriteLine("You have entered an invalid option \n Valid options are: \n --test: run unit tests and quit \n --mem: specify simluated RAM size \n --load: specify ELF file to open \n");
                Environment.Exit(0);
            }

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--load":
                        try
                        {
                            fileName = args[i + 1];
                        }
                        catch
                        {
                            Console.WriteLine("please supply a file to load");
                            Environment.Exit(0);
                        }
                        i++;
                        Debug.WriteLine("Options.Parse: --load and file accepted.");
                        break;

                    case "--mem":
                        try
                        {
                            memSize = Convert.ToInt32(args[i + 1]);
                            if (memSize > 10000000) { throw new Exception(); }
                        }
                        catch
                        {
                            Console.WriteLine("incorrect ram formatting (10MB max) quitting");
                            Environment.Exit(0);
                        }
                        i++;
                        Debug.WriteLine("Options.Parse: --mem accepted.");
                        break;

                    case "--test":
                        //run unit tests and quit
                        test = true;
                        Debug.WriteLine("Options.Parse: --test accepted. Unit tests will run.");
                        break;

                    default:
                        Console.WriteLine("You have entered an invalid option \n Valid options are: \n --test: run unit tests and quit \n --mem: specify simluated RAM size \n --load: specify ELF file to open \n");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        //Method:       GetMemSize
        //Purpose:      Returns memSize.
        public int GetMemSize()
        {
            return memSize;
        }

        //Method:       GetFileName
        //Purpose:      Returns fileName.
        public string GetFileName()
        {
            return fileName;
        }

        //Method:       GetTest
        //Purpose:      Returns test.
        public bool GetTest()
        {
            return test;
        }

        //Method:       SetFileName
        //Purpose:      Sets the file name to a new variable.
        //Variables:    toFileName - string of new filename
        public void SetFileName(string toFileName)
        {
            fileName = toFileName;
        }
    }


    //Class:        Program
    //Purpose:      Accepts user input then initiates classes and runs program.
    class RAMsim
    {
        private byte[] ram;

        //Method:       RAMsim
        //Purpose:      Constructs new RAMsim object and byte array of ram
        //Variables:    memsize - size of memory to instantiate bytearray to
        public RAMsim(int memsize)
        {
            ram = new byte[memsize];
        }

        //Method:       PopulateRam
        //Purpose:      Populates ram with values from toRam
        //Variables:    toRam - bytearray to populate ram with
        //              loc - uint of location in ram to insert toRam
        public void PopulateRam(byte[] toRam, uint loc)
        {
            Debug.WriteLine("RAMsim.PopulateRam: PoplulatingRam at location: " + loc + " in byte[] ram");
            for (int i = 0; i < toRam.Length; i++)
            {
                ram[loc] = toRam[i];
                loc++;
            }
        }

        //Method:       ReadWord
        //Purpose:      returns a word of information from ram
        //Variables:    addr - uint of addr to read a word from
        public uint ReadWord(uint addr)
        {
            return BitConverter.ToUInt32(ram, (int)addr);
        }


        //Method:       WriteWord
        //Purpose:      writes a word of information to ram
        //Variables:    addr - uint of addr to write a word into
        //              toRam - uint to populate ram with
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

        //Method:       ReadHalfWord
        //Purpose:      returns a half word of information from ram
        //Variables:    addr - uint of addr to read a half word from
        public uint ReadHalfWord(uint addr)
        {
            return BitConverter.ToUInt16(ram, (int)addr);
        }

        //Method:       WriteHalfWord
        //Purpose:      writes a halfword of information to ram
        //Variables:    addr - uint of addr to write a halfword into
        //              toRam - uint to populate ram with
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

        //Method:       ReadByte
        //Purpose:      returns a byte of information from ram
        //Variables:    addr - uint of addr to read a byte from
        public byte ReadByte(uint addr)
        {
            return ram[addr];
        }

        //Method:       WriteByte
        //Purpose:      writes a byte of information to ram
        //Variables:    addr - uint of addr to write a byte into
        //              toRam - uint to populate ram with
        public void WriteByte(uint addr, byte toRam)
        {
            ram[addr] = toRam;
        }

        //Method:       getMDF
        //Purpose:      returns MD5 hash dump as a string
        public string getMDF()
        {
            byte[] mdf = new MD5CryptoServiceProvider().ComputeHash(ram);
            StringBuilder toString = new StringBuilder();
            for (int i = 0; i < mdf.Length; i++)
            {
                toString.Append(mdf[i].ToString("x2"));
            }
            //return hex string
            return toString.ToString();
        }

        //Method:       TestFlag
        //Purpose:      tests a specific bit and returns the corresponding bool
        //Variables:    addr - uint of addr to read a word from
        //              bit - int of location in addr where the bit to check is stored
        public bool TestFlag(uint addr, int bit)
        {
            uint word = ReadWord(addr);
            word = word >> bit;
            return (word & 1) == 1 ? true : false;
        }

        //Method:       SetFlag
        //Purpose:      sets a specific bit in ram
        //Variables:    addr - uint of addr to read a word from
        //              bit - int of location in addr where the bit to change is stored
        //              bool - value of bit to be set
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

    //Class:        Loader
    //Purpose:      Accepts and parses elf file
    class Loader
    {
        private Options myOptions;
        private RAMsim myRam;
        List<ELFTWO> programHeaders = new List<ELFTWO>();

        //Method:       Loader
        //Purpose:      constructs Loader class object and instantiates variables
        //Variables:    toMyOptions - Options object for reference
        //              toMyRam - RAMsim object for reference
        public Loader(Options toMyOptions, RAMsim toMyRam)
        {
            myOptions = toMyOptions;
            myRam = toMyRam;
        }

        //Method:       Load
        //Purpose:      extracts ram and program headers from the elf file and calls to instantiate ram
        public void Load()
        {
            //COMPLIMENT OF J
            //opens given filename and identifies key elements 
            string elfFilename = myOptions.GetFileName();
            try
            {
                Debug.WriteLine("Loader.Load: Opening " + elfFilename + "...");
                using (FileStream strm = new FileStream(elfFilename, FileMode.Open))
                {
                    ELF elfHeader = new ELF();
                    byte[] data = new byte[Marshal.SizeOf(elfHeader)];

                    Debug.WriteLine("Loader.Load: Reading " + elfFilename + "...");
                    // Read ELF header data
                    strm.Read(data, 0, data.Length);

                    Debug.WriteLine("Loader.Load: Converting to struct");
                    // Convert to struct
                    elfHeader = ByteArrayToStructure<ELF>(data);

                    Debug.WriteLine("Loader.Load: Entry point: " + elfHeader.e_entry.ToString("X4"));
                    Debug.WriteLine("Loader.Load: Number of program header entries: " + elfHeader.e_phnum);
                    Debug.WriteLine("Loader.Load: Reading program header entries...");

                    // Read program header entries into programHeaders
                    strm.Seek(elfHeader.e_phoff, SeekOrigin.Begin);
                    for (int i = 0; i < elfHeader.e_phnum; i++)
                    {
                        data = new byte[elfHeader.e_phentsize];
                        strm.Read(data, 0, (int)elfHeader.e_phentsize);
                        programHeaders.Add(ByteArrayToStructure<ELFTWO>(data));
                    }

                    Debug.WriteLine("Loader.Load: Reading memory from program headers...");
                    //read memory from program header and send to 
                    foreach (ELFTWO toRam in programHeaders)
                    {
                        data = new byte[toRam.p_memsz];
                        strm.Seek(toRam.p_offset, SeekOrigin.Begin);
                        strm.Read(data, 0, (int)toRam.p_memsz);
                        myRam.PopulateRam(data, toRam.p_vaddr);
                    }

                    Console.WriteLine(myRam.getMDF());

                }
            }
            catch
            {
                Console.WriteLine("error loading file. please check your file/filename and try again");
                Environment.Exit(1);
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
    //Class:        TestLoader
    //Purpose:      Unit tests for the loader class
    class TestLoader
    {
        //Method:       RunTests
        //Purpose:      tests every method in the Loader class
        //Variables:    myOptions - Options handle to options to class
        public static void RunTests(Options myOptions)
        {
            myOptions.SetFileName("test1.exe");
            RAMsim myRam = new RAMsim(myOptions.GetMemSize());
            Loader myLoader = new Loader(myOptions, myRam);
            Console.WriteLine("testing Loader...");
            myLoader.Load();
            Console.Write("verifying MD5 hash...");
            Debug.Assert(myRam.getMDF() == "3500a8bef72dfed358b25b61b7602cf1");
            Console.WriteLine("success!");
        }

    }

    //BEGIN UNIT TESTING
    //Class:        TestRAM
    //Purpose:      Unit tests for the ram class
    class TestRAM
    {
        //Method:       RunTests
        //Purpose:      tests every method in the RAMsim class
        public static void RunTests()
        {
            Console.WriteLine("testing RAM...");

            RAMsim ram = new RAMsim(7);

            //test populate method
            Console.Write("verifying Populate...");
            ram.PopulateRam(new byte[] { 0xFF, 0xAA, 0xAA, 0xFF, 0xFF, 0xFF, 0xFF }, 0);
            Debug.Assert(ram.ReadHalfWord(1) == 0xAAAA);
            Console.WriteLine("success!");

            //test set/test flag methods
            Console.Write("verifying SetFlag/TestFlag...");
            ram.SetFlag(1, 6, true);
            Debug.Assert(ram.TestFlag(1, 6));
            ram.SetFlag(1, 6, false);
            Debug.Assert(!(ram.TestFlag(1, 6)));
            Console.WriteLine("success!");

            //test read/write methods
            Console.Write("verifying Read/Write Word...");
            ram.WriteWord(0, 0xFFFFFFFF);
            Debug.Assert(ram.ReadWord(0) == 0xFFFFFFFF);
            Console.WriteLine("success!");

            Console.Write("verifying Read/Write HalfWord...");
            ram.WriteWord(0, 0xCCCC);
            Debug.Assert(ram.ReadWord(0) == 0xCCCC);
            Console.WriteLine("success!");

            Console.Write("verifying Read/Write Byte...");
            ram.WriteWord(0, 0xAA);
            Debug.Assert(ram.ReadWord(0) == 0xAA);
            Console.WriteLine("success!");
        }

    }
}
