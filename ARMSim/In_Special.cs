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
        uint instruction, rd, rs, rm;


        public In_Special(Registers toRegister, Memory toMemory, uint toInstruction)
        {
            myRegister = toRegister;
            myMemory = toMemory;
            instruction = toInstruction;
        }

        public override void decode()
        {
            switch (instructionName)
            {

                case "swi":
                    
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
                    //to string
                    executeSWI();
                    break;

                case "mul":
                    //to string
                    executeMUL();
                    break;

                default:
                    break;
            }
            //switch to figure out which execute method to call
        }

        public void executeSWI()
        {
            //blow up here.
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
