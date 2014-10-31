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
    //Class:        Program
    //Purpose:      Accepts user input then initiates classes and runs program.
    class Memory
    {
        protected byte[] ram;
        private uint myFlags;


        //Method:       Memory
        //Purpose:      Constructs new Memory object and byte array of ram
        //Variables:    memsize - size of memory to instantiate bytearray to
        public Memory(int memsize)
        {
            ram = new byte[memsize];
        }

        //Method:       PopulateRam
        //Purpose:      Populates ram with values from toRam
        //Variables:    toRam - bytearray to populate ram with
        //              loc - uint of location in ram to insert toRam
        public void PopulateRam(byte[] toRam, uint loc)
        {
            Debug.WriteLine("Memory.PopulateRam: PoplulatingRam at location: " + loc + " in byte[] ram");
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
            return toString.ToString().ToUpper();
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

        public void SetFlagAddr(uint toFlags)
        {
            myFlags = toFlags;
        }

        public uint GetFlagAddr()
        {
            return myFlags;
        }
    }
}
