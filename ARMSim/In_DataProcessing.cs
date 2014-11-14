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
        bool disassembling;
        public static string opTwoDissasembly;


        public In_DataProcessing(Registers toRegister, Memory toMemory, uint toInstruction, bool toDisassembling)
        {
            myRegister = toRegister;
            myMemory = toMemory;
            disassembling = toDisassembling;
            instruction = toInstruction;
        }

        public override void decode()
        {
            condition = getSectionValue(31, 28, instruction);
            opcode = getSectionValue(24, 21, instruction);
            rn = getSectionValue(19, 16, instruction);
            rd = getSectionValue(15, 12, instruction);
            In_dp_OperandTwo myOp2 = new In_dp_OperandTwo(myRegister, instruction);
            operand2 = myOp2.getValue();
        }

        public override void execute()
        {
            //switch statement to determine WHICH execute command to do.
            switch (opcode)
            {
                case 0:
                    //to string
                    disassembly = "and " + 'r' + rd + ", " + 'r' + rn + ", " + opTwoDissasembly;
                    if (!disassembling) { executeAND(); }
                    break;

                case 1:
                    disassembly = "eor " + 'r' + rd + ", " + 'r' + rn + ", " + opTwoDissasembly;
                    if (!disassembling) { executeEOR(); }
                    break;

                case 2:
                    //to string
                    disassembly = "sub " + 'r' + rd + ", " + 'r' + rn + ", " + opTwoDissasembly;
                    if (!disassembling) { executeSUB(); }
                    break;

                case 3:
                    //to string
                    disassembly = "rsb " + 'r' + rd + ", " + 'r' + rn + ", " + opTwoDissasembly;
                    if (!disassembling) { executeRSB(); }
                    break;

                case 4:
                    //to string
                    disassembly = "add " + 'r' + rd + ", " + 'r' + rn + ", " + opTwoDissasembly;
                    if (!disassembling) { executeADD(); }
                    break;

                case 10:
                    //to string
                    disassembly = "cmp r" + rn + ", " + opTwoDissasembly;
                    if (!disassembling) { executeCMP(); }
                    break;

                case 12:
                    //to string
                    disassembly = "orr " + 'r' + rd + ", " + 'r' + rn + ", " + opTwoDissasembly;
                    if (!disassembling) { executeORR(); }
                    break;

                case 13:
                    //to string
                    disassembly = "mov " + 'r' + rd + ", " + opTwoDissasembly;
                    if (!disassembling) { executeMOV(); }
                    break;

                case 14:
                    //to string
                    disassembly = "bic " + 'r' + rd + ", " + 'r' + rn + ", " + opTwoDissasembly;
                    if (!disassembling) { executeBIC(); }
                    break;

                case 15:
                    //to string
                    disassembly = "mvn " + 'r' + rd + ", " + opTwoDissasembly;
                    if (!disassembling) { executeMVN(); }
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
            myRegister.WriteWord(rd, (myRegister.ReadWord(rn) + operand2));
        }

        public void executeSUB()
        {
            myRegister.WriteWord(rd, (myRegister.ReadWord(rn) - operand2));
        }

        public void executeRSB()
        {
            myRegister.WriteWord(rd, (operand2 - myRegister.ReadWord(rn)));
        }

        public void executeAND()
        {
            myRegister.WriteWord(rd, (myRegister.ReadWord(rn) & operand2));
        }

        public void executeORR()
        {
            myRegister.WriteWord(rd, (myRegister.ReadWord(rn) | operand2));
        }

        public void executeEOR()
        {
            myRegister.WriteWord(rd, (myRegister.ReadWord(rn) ^ operand2));
        }

        public void executeBIC()
        {
            myRegister.WriteWord(rd, (myRegister.ReadWord(rn) & (~operand2)));
        }

        public void executeCMP()
        {
            uint meinKmp = myRegister.ReadWord(rn) - operand2;

            //set N flag
            myMemory.SetFlag(0, (Instructions.getSectionValue(31, 31, meinKmp) == 0 ? false : true));
            
            //set Z flag
            myMemory.SetFlag(1, (meinKmp == 0 ? true : false));
            
            //set C flag
            myMemory.SetFlag(2, (operand2 > myRegister.ReadWord(rn) ? false : true));

            //set F flag
            try
            {
                myMemory.SetFlag(3, false);
                int tempRN = (int)myRegister.ReadWord(rn);
                int iop2 = (int)operand2;
                int f = checked(tempRN - iop2);
            }
            catch (OverflowException e)
            {
                myMemory.SetFlag(3, true);
            }
        }
    }
}