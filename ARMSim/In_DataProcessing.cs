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

        
        public In_DataProcessing(Registers toRegister, Memory toMemory, uint toInstruction)
        {
            myRegister = toRegister;
            myMemory = toMemory;
            instruction = toInstruction;
        }

        new public void decode()
        {
            condition = getSectionValue(31, 28, instruction);
            opcode = getSectionValue(24, 21, instruction);
            rn = getSectionValue(19, 16, instruction);
            rd = getSectionValue(15, 12, instruction);
            rd = getSectionValue(15, 12, instruction);
            OperandTwo myOp2 = new OperandTwo(myRegister, instruction);
            operand2 = myOp2.getValue();
        }

        new public void execute()
        {
            switch (opcode)
            {
                //cases 1-12 here
                case 13:
                    executeMOV();
                    break;
                default:
                    break;
            }
            //switch statement to determine WHICH execute command to do.
        }

        public void executeMOV()
        {
            myRegister.WriteWord(rd, operand2);
        }

        public void executeMVN()
        {

        }

        public void executeADD()
        {

        }

        public void executeSUB()
        {

        }

        public void executeRSB()
        {

        }

        public void executeMUL()
        {

        }

        public void executeAND()
        {

        }

        public void executeORR()
        {

        }

        public void executeEOR()
        {

        }

        public void executeBIC()
        {

        }
    }
}
