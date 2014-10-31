using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSim
{
    class In_Special : Instructions
    {
        Registers myRegister;
        Memory myMemory;
        bool disassembling;
        uint instruction, rd, rs, rm;
        public static string opTwoDissasembly;


        public In_Special(Registers toRegister, Memory toMemory, uint toInstruction, bool toDisassembling)
        {
            myRegister = toRegister;
            myMemory = toMemory;
            disassembling = toDisassembling;
            instruction = toInstruction;
        }

        public override void decode()
        {
            switch (instructionName)
            {

                case "swi":
                    disassembly = "swi " + Instructions.getSectionValue(23, 0, instruction);
                    break;

                case "mul":
                    rd = getSectionValue(19, 16, instruction);
                    rs = getSectionValue(11, 8, instruction);
                    rm = getSectionValue(3, 0, instruction);
                    break;

                default:
                    break;
            }
        }

        public override void execute()
        {
            switch (instructionName)
            {

                case "swi":
                    executeSWI();
                    break;

                case "mul":
                    //to string
                    disassembly = "mul " + 'r' + rd + ", " + 'r' + rm + ", " + 'r' + rs;
                    if (!disassembling) { executeMUL(); }
                    break;

                default:
                    break;
            }
            //switch to figure out which execute method to call
        }

        public void executeSWI()
        {
            //never happens
        }

        public void executeMUL()
        {
            myRegister.WriteWord(rd, (myRegister.ReadWord(rs) * myRegister.ReadWord(rm)));
        }

        public bool isSpecial()
        {
            if (getSectionValue(27, 24, instruction) == 15)
            {
                instructionName = "swi";
                return true;
            } if ((getSectionValue(7, 7, instruction) == 1) && (getSectionValue(4, 4, instruction) == 1) && (getSectionValue(27, 21, instruction) == 0) && (getSectionValue(15, 12, instruction) == 0))
            {
                instructionName = "mul";
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
