using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSim
{
    class In_DataProcessing : Instructions
    {
        //stores informatino about all data processing instructions
        uint operand2, opcode, rn, rd, condition, instruction;
        Registers myRegister;
        Memory myMemory;
        public static string opTwoDissasembly;

        
        public In_DataProcessing(Registers toRegister, Memory toMemory, uint toInstruction)
        {
            myRegister = toRegister;
            myMemory = toMemory;
            instruction = toInstruction;
        }

        public override void decode()
        {
            condition = getSectionValue(31, 28, instruction);
            opcode = getSectionValue(24, 21, instruction);
            rn = getSectionValue(19, 16, instruction);
            rd = getSectionValue(15, 12, instruction);
            OperandTwo myOp2 = new OperandTwo(myRegister, instruction);
            operand2 = myOp2.getValue();
        }

        public override void execute()
        {
            //switch statement to determine WHICH execute command to do.
            switch (opcode)
            {
                //cases 1-12 here
                case 0:
                    //to string
                    executeAND();
                    break;

                case 1:
                    //to string
                    executeEOR();
                    break;

                case 2:
                    //to string
                    executeSUB();
                    break;

                case 3:
                    //to string
                    executeRSB();
                    break;

                case 4:
                    //to string
                    executeADD();
                    break;

                case 12:
                    //to string
                    executeORR();
                    break;

                case 13:
                    //to string
                    disassembly = "mov " + 'r' + rd + ", " + opTwoDissasembly;
                    executeMOV();
                    break;

                case 14:
                    //to string
                    executeBIC();
                    break;

                case 15:
                    //to string
                    executeMVN();
                    break;

                default:
                    break;
            }
        }

        public void executeMOV()
        {
            myRegister.WriteWord(rd, operand2);
        }

        public void executeMVN()
        {
            myRegister.WriteWord(rd, ~operand2);
        }

        public void executeADD()
        {
            myRegister.WriteWord(rd, (rn + operand2));
        }

        public void executeSUB()
        {
            myRegister.WriteWord(rd, (rn - operand2));
        }

        public void executeRSB()
        {
            myRegister.WriteWord(rd, (operand2 - rn));
        }

        public void executeMUL()
        {

        }

        public void executeAND()
        {
            myRegister.WriteWord(rd, (rn & operand2));
        }

        public void executeORR()
        {
            myRegister.WriteWord(rd, (rn | operand2));
        }

        public void executeEOR()
        {
            myRegister.WriteWord(rd, (rn ^ operand2));
        }

        public void executeBIC()
        {
            myRegister.WriteWord(rd, (rn & (~operand2)));
        }
    }
}