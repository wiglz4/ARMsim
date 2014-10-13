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
        Instructions curInstruction;
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
            //retrive command pointed to by addr stored in register 15
            uint command = myRegisters.GetMemAtLocReg(myMemory, 15);
            myRegisters.IncrCounter(myMemory);
            return myMemory.ReadWord(command);
            //read word from RAM address specified by program counter register 
            //(program counter register is 15)
            //increments counter by 4
        }

        //Method:       Decode
        //Purpose:      Decodes thisCommand into a specific instruction. Handles it as generic.
        //Variables:    thisCommand - uint containing undecoded command
        public void Decode(uint thisCommand)
        {
            curInstruction = Instructions.decode(thisCommand, myRegisters, myMemory);
            curInstruction.decode();
        }

        //Method:       Execute
        //Purpose:      executes specific instruction
        public void Execute()
        {
            curInstruction.execute();
            //Thread.Sleep(250);
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
