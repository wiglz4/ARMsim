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

    //Class:        Loader
    //Purpose:      Accepts and parses elf file
    class Loader
    {
        private Options myOptions;
        private Memory myRam;
        private uint programCounter;
        List<ELFTWO> programHeaders = new List<ELFTWO>();

        //Method:       Loader
        //Purpose:      constructs Loader class object and instantiates variables
        //Variables:    toMyOptions - Options object for reference
        //              toMyRam - Memory object for reference
        public Loader(Options toMyOptions, Memory toMyRam)
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

                    //this really should be changed to be in memory class
                    programCounter = elfHeader.e_entry;
                    myRam.SetFlagAddr(elfHeader.e_flags);
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
                    
                    //testing purposes
                    // Debug.WriteLine(myRam.getMDF());

                }
            }
            catch
            {
                Console.WriteLine("error loading file. please check your file/filename and try again");
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

        public uint getProgramCounter()
        {
            return programCounter;
        }
    }
}
