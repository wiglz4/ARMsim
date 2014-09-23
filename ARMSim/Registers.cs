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
    class Registers : Memory
    {
        public Registers() : base(64) { ; }

        new public uint ReadWord(uint addr)
        {
            return BitConverter.ToUInt32(ram, (int)addr*4);
        }

        new public void WriteWord(uint addr, uint toRam)
        {
            int counter = 0;
            byte[] toRamBA = BitConverter.GetBytes(toRam);
            foreach (byte x in toRamBA)
            {
                ram[addr*4 + counter] = x;
                counter++;
            }
        }
        
        public uint GetRegister(Memory myMemory, uint regNum)
        {
            return myMemory.ReadWord(BitConverter.ToUInt32(ram, (int)regNum * 4));
        }

        public void IncrCounter(Memory myMemory)
        {
            uint newCounter = GetRegister(myMemory, 15);
            newCounter += 4;
            myMemory.WriteWord(this.ReadWord(15), newCounter);
        }
    }
}
