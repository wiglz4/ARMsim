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
    //Class:        Registers inherits from Memory
    //Purpose:      Creates and holds byte array to store registers in.
    class Registers : Memory
    {
        public Registers() : base(64) { ; }

        //Method:       ReadWord
        //Purpose:      Returns value stored in specified register.
        //Variables:    addr -   Uint specifying register.
        new public uint ReadWord(uint addr)
        {
            return BitConverter.ToUInt32(ram, (int)addr * 4);
        }

        //Method:       WriteWord
        //Purpose:      Writes value to specified register.
        //Variables:    addr -  Uint specifying register.
        //              toRam - Uint specifying value.
        new public void WriteWord(uint addr, uint toRam)
        {
            int counter = 0;
            byte[] toRamBA = BitConverter.GetBytes(toRam);
            foreach (byte x in toRamBA)
            {
                ram[addr * 4 + counter] = x;
                counter++;
            }
        }

        //Method:       GetMemAtLocReg
        //Purpose:      Returns Memory stored in the address location specified by regNum
        //Variables:    myMemory -  Memory handle to ram.
        //              regNum -    Register storing address.   
        public uint GetMemAtLocReg(Memory myMemory, uint regNum)
        {
            return myMemory.ReadWord(BitConverter.ToUInt32(ram, (int)regNum * 4));
        }

        //Method:       IncrCounter
        //Purpose:      Increments Program Counter
        //Variables:    myMemory -  Memory handle to ram.
        public void IncrCounter(Memory myMemory)
        {
            uint newCounter = GetMemAtLocReg(myMemory, 15);
            myMemory.WriteWord(this.ReadWord(15), newCounter + 4);
        }
    }
}