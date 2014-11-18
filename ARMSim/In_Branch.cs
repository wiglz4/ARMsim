using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSim
{
    class In_Branch : Instructions
    {

        Registers myRegister;
        Memory myMemory;
        bool disassembling;
        uint targetAddr, opcode, instruction, condition;


        public In_Branch(Registers toRegister, Memory toMemory, uint toInstruction, bool toDisassembling)
        {
            myRegister = toRegister;
            myMemory = toMemory;
            disassembling = toDisassembling;
            instruction = toInstruction;
        }

        public override void decode()
        {
            condition = getSectionValue(31, 28, instruction);
            opcode = getSectionValue(27, 24, instruction);
            targetAddr = calcTargetAddr();
            //sign extend 
            
        }

        public override void execute()
        {
            //switch statement to determine WHICH execute command to do.
            switch (opcode)
            {
                case 1:
                    //to string
                    disassembly = "bx"  + Instructions.firstOpcode + (myRegister.ReadWord(getSectionValue(3, 0, instruction)));
                    if (!disassembling) { executeBX(); }
                    break;

                case 10:
                    //to string
                    disassembly = "b" + Instructions.firstOpcode + " " + (myRegister.ReadWord(15) + targetAddr);
                    if (!disassembling) { executeB(); }
                    break;

                case 11:
                    //to string
                    disassembly = "bl" + Instructions.firstOpcode + " " + (myRegister.ReadWord(15) + targetAddr);
                    if (!disassembling) { executeBL(); }
                    break;

                default:
                    break;
            }
        }

        public void executeB()
        {
            Computer.storedBranchPC = (int)myRegister.ReadWord(15) - 8;
            myRegister.WriteWord(15, myRegister.ReadWord(15) + 4 + targetAddr);
        }

        public void executeBL()
        {
            Computer.storedBranchPC = (int)myRegister.ReadWord(15) - 8;
            myRegister.WriteWord(14, myRegister.ReadWord(15) - 4);
            myRegister.WriteWord(15, myRegister.ReadWord(15) + 4 + targetAddr);
        }

        public void executeBX()
        {
            Computer.storedBranchPC = (int)myRegister.ReadWord(15) - 8;
            myRegister.WriteWord(15, myRegister.ReadWord(getSectionValue(3, 0, instruction)) + 4);
        }

        public uint calcTargetAddr()
        {
            int toReturn = Convert.ToInt32(getSectionValue(23, 0, instruction));
            if (getSectionValue(23, 23, instruction) == 1)
            {
                toReturn = toReturn | 0x3F000000;
            }
            toReturn = toReturn << 2;
            return (uint)toReturn;
        }
    }
}
