using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ARMSim
{
    class CPU
    {
        Memory myMemory;
        Registers myRegisters;

        public CPU(Memory toMemory, Registers toRegisters)
        {
            myMemory = toMemory;
            myRegisters = toRegisters;
        }

        public uint Fetch()
        {
            return myMemory.ReadWord(myRegisters.ReadWord(15));
            //read word from RAM address specified by program counter register 
            //(program counter register is 15)
        }

        public void Decode()
        {
            //later
        }

        public void Execute()
        {
            Thread.Sleep(3000);
            //pause for 1/4th a second (250)
        }
    }
}
