﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSim
{
    //YOU LEFT OFF HERE:
    //1. IMPLEMENT SWI ON LINE 33
    //2. IMPLEMENT LDR, STR, STM/LDM (FD)
    //UNIT TESTS

    //TO FIX FROM BEFORE--
    //-setflag
    //-disassemblyPanel
    //-multiThreading
   
    abstract class Instructions
    {
        public static string disassembly;
        public string instructionName;
        //stores generic information about all decoded arm instructions

        //decodes bits 27-26 and returns specified instruction type
        public static Instructions decode(uint instruction, Registers myRegisters, Memory myMemory)
        {
            //handle specials here
            In_Special sInstruction = new In_Special(myRegisters, myMemory, instruction);

            if (sInstruction.isSpecial())
            {
                return sInstruction;
            }
            else
            {
                uint type = getSectionValue(27, 26, instruction);
                if (type == 0)
                {
                    return new In_DataProcessing(myRegisters, myMemory, instruction);
                }
                if (type == 1)
                {
                    return new In_LoadStore(myRegisters, myMemory);
                }
                else
                {
                    return new In_Branch(myRegisters, myMemory);
                }
            }
        }

        public abstract void decode();
        public abstract void execute();


        //returns uint value of a specific selection of bits
        public static uint getSectionValue(int upper, int lower, uint instruction)
        {
            return (instruction << (31 - upper)) >> (31 + lower - upper);
        }
    }
}
