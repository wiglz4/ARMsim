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
        public static string firstOpcode;
        public static bool dontDoItBro;
        public string instructionName;
        //stores generic information about all decoded arm instructions

        //decodes bits 27-26 and returns specified instruction type
        public static Instructions decode(uint instruction, Registers myRegisters, Memory myMemory, bool disassembling)
        {

            // check opcodes here....
            dontDoItBro = false;
            firstOpcode = passOpcode(getSectionValue(31, 28, instruction), myMemory);
            if (firstOpcode == null)
            {
                dontDoItBro = true;
                return null;
                //get outta here. this instructions a bad one
            }

            //handle specials here
            In_Special sInstruction = new In_Special(myRegisters, myMemory, instruction, disassembling);

            if (sInstruction.isSpecial())
            {
                return sInstruction;
            }

            if (getSectionValue(27, 4, instruction) == 0x120001)
            {
                return new In_Branch(myRegisters, myMemory, instruction, disassembling);
            }

            else
            {
                //previously checked only bits 27, - 26
                uint type = getSectionValue(27, 25, instruction);
                if (type == 0 || type == 1)
                {
                    return new In_DataProcessing(myRegisters, myMemory, instruction, disassembling);
                }
                if (type == 2 || type == 3 || type == 4)
                {
                    return new In_LoadStore(myRegisters, myMemory, instruction, disassembling);
                }
                else
                {
                    return new In_Branch(myRegisters, myMemory, instruction, disassembling);
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

        public static string passOpcode(uint opcode, Memory myMemory)
        {
            switch (opcode)
            {
                case 0:
                    // EQ
                    return myMemory.TestFlag(1) == 1 ? "EQ" : null;

                case 1:
                    // NE
                    return myMemory.TestFlag(1) == 0 ? "NE" : null;

                case 2:
                    // CS/HS
                    return myMemory.TestFlag(2) == 1 ? "CS" : null;

                case 3:
                    // CC/LO
                    return myMemory.TestFlag(2) == 0 ? "CC" : null;

                case 4:
                    // MI
                    return myMemory.TestFlag(0) == 1 ? "MI" : null;

                case 5:
                    //PL
                    return myMemory.TestFlag(0) == 0 ? "PL" : null;

                case 6:
                    // VS
                    return myMemory.TestFlag(3) == 1 ? "VS" : null;

                case 7:
                    // VC
                    return myMemory.TestFlag(3) == 0 ? "VC" : null;

                case 8:
                    // HI
                    return (myMemory.TestFlag(2) == 1) && (myMemory.TestFlag(1) == 0) ? "HI" : null;

                case 9:
                    // LS
                    return (myMemory.TestFlag(2) == 0) && (myMemory.TestFlag(1) == 1) ? "LS" : null;

                case 10:
                    // GE
                    return (myMemory.TestFlag(0) == myMemory.TestFlag(3)) ? "GE" : null;

                case 11:
                    // LT
                    return (myMemory.TestFlag(0) != myMemory.TestFlag(3)) ? "LT" : null;
                
                case 12:
                    // GT
                    return (myMemory.TestFlag(1) == 0) && (myMemory.TestFlag(0) == myMemory.TestFlag(3)) ? "GT" : null;

                case 13:
                    // LE
                    return (myMemory.TestFlag(1) == 1) || (myMemory.TestFlag(0) != myMemory.TestFlag(3)) ? "LE" : null;

                case 14:
                    // AL
                    return "";              

                default:
                    return null;
            }
        }
    }
}
