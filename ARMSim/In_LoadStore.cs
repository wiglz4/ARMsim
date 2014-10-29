using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSim
{
    class In_LoadStore : Instructions
    {
        //stores informatino about all Load and Store instructions
        uint operand2, p, u, bs, w, l, rn, rd, condition, instruction, type, addr;
        Registers myRegister;
        Memory myMemory;
        public static string opTwoDissasembly;


        public In_LoadStore(Registers toRegister, Memory toMemory, uint toInstruction)
        {
            myRegister = toRegister;
            myMemory = toMemory;
            instruction = toInstruction;
        }

        public override void decode()
        {
            condition = getSectionValue(31, 28, instruction);
            type = getSectionValue(27, 25, instruction);
            p = getSectionValue(24, 24, instruction);
            u = getSectionValue(23, 23, instruction);
            bs = getSectionValue(22, 22, instruction);
            w = getSectionValue(21, 21, instruction);
            l = getSectionValue(20, 20, instruction);
            rn = getSectionValue(19, 16, instruction);
            rd = getSectionValue(15, 12, instruction);
            In_ls_OperandTwo myOp2 = new In_ls_OperandTwo(myRegister, instruction);
            operand2 = myOp2.getValue();
        }

        public override void execute()
        {
            if (l == 1)
            {
                //its a load
                switch (type)
                {
                    case 4:
                        executeLDM();
                        break;
                    default:
                        executeLDR();
                        break;
                }
            }
            else
            {
                //its a store
                switch (type)
                {
                    case 4:
                        executeSTM();
                        break;
                    default:
                        executeSTR();
                        break;
                }
            }
        }

        public void executeLDR()
        {
            int operand2int = Convert.ToInt32(operand2);
            if (u == 0) { operand2int *= -1; }
            //gettin rn
            if (p == 1) { addr = (uint)(myRegister.ReadWord(rn) + operand2int); }
            else if (p == 0) { addr = myRegister.ReadWord(rn); }
            //performing the load
            if (bs == 0) { myRegister.WriteWord(rd, myMemory.ReadWord(addr)); }
            else if (bs == 1) { myRegister.WriteWord(rd, myMemory.ReadByte(addr)); }
            //writeback?
            if (p == 1 && w == 1) { myRegister.WriteWord(rn, addr); }
        }

        public void executeSTR()
        {

        }
        public void executeLDM()
        {

        }

        public void executeSTM()
        {

        }
    }
}
