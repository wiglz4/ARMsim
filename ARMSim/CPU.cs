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
    //Class:        CPU
    //Purpose:      Represents simulated CPU.
    class CPU
    {
        Memory myMemory;
        Registers myRegisters;
        int N, C, Z, F;

        //Method:       Constructor
        //Purpose:      Sets CPU up for use.
        //Variables:    toMemory - Memory object that Computer setup.
        //              toRegisters - Registers object that Computer setup.
        //              programCounter - uint signifying where to start fetch at.
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

        //Method:       Fetch
        //Purpose:      Reads next word to execute, increments program counter.
        public uint Fetch()
        {
            uint counter = myRegisters.GetMemAtLocReg(myMemory, 15);
            myRegisters.IncrCounter(myMemory);
            return myMemory.ReadWord(counter);
            //read word from RAM address specified by program counter register 
            //(program counter register is 15)
            //increments counter by 4
        }

        //Method:       Fetch
        //Purpose:      later.
        public void Decode()
        {
            //WILL CREATE GENERIC INSTRUCTION AND CALL DECODE ON IT AND RETURN TO SENDER SPECIFIC INSTRUCTION
        }

        //Method:       Fetch
        //Purpose:      later/pauses for 1/4 a second
        public void Execute()
        {
            //WILL ACCEPT SPECIFIC INSTRUCTION AS PARAMETER AND CALL EXECUTE ON SPECIFIC INSTRUCTION
            Thread.Sleep(250);
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
