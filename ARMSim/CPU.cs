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
        int N, C, Z, F;

        public CPU(Memory toMemory, Registers toRegisters, uint programCounter)
        {
            myMemory = toMemory;
            myRegisters = toRegisters;
            myMemory.WriteWord(myRegisters.ReadWord(15), programCounter);
            N = 0;
            C = 0;
            Z = 0;
            F = 0;
        }

        public uint Fetch()
        {
            uint counter = myRegisters.GetMemAtLocReg(myMemory, 15);
            myRegisters.IncrCounter(myMemory);
            return myMemory.ReadWord(counter);
            //read word from RAM address specified by program counter register 
            //(program counter register is 15)
            //increments counter by 4
        }

        public void Decode()
        {
            //later
        }

        public void Execute()
        {
            Thread.Sleep(250);
            //pause for 1/4th a second (250)
        }

        public int getFlagN()
        {
            return N;
        }
        public int getFlagZ()
        {
            return Z;
        }
        public int getFlagC()
        {
            return C;
        }
        public int getFlagF()
        {
            return F;
        }
    }
}
