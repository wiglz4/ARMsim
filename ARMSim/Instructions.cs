using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSim
{
    //YOU LEFT OFF HERE:
    //1. TEST CASES
    //2. REPORT

    //TO FIX FROM BEFORE--
    //-setflag

    abstract class Instructions
    {
        public static string disassembly;
        public string instructionName;
        //stores generic information about all decoded arm instructions

        //decodes bits 27-26 and returns specified instruction type
        public static Instructions decode(uint instruction, Registers myRegisters, Memory myMemory, bool disassembling)
        {
            //handle specials here
            In_Special sInstruction = new In_Special(myRegisters, myMemory, instruction, disassembling);

            if (sInstruction.isSpecial())
            {
                return sInstruction;
            }
            else
            {
                uint type = getSectionValue(27, 26, instruction);
                if (type == 0)
                {
                    return new In_DataProcessing(myRegisters, myMemory, instruction, disassembling);
                }
                if (type == 1 || type == 2)
                {
                    return new In_LoadStore(myRegisters, myMemory, instruction, disassembling);
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
