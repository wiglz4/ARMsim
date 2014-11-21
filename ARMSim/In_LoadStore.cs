using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ARMSim
{
    class In_LoadStore : Instructions
    {
        //stores informatino about all Load and Store instructions
        uint operand2, p, u, bs, w, l, rn, rd, condition, instruction, type, addr;
        Registers myRegister;
        Memory myMemory;
        bool disassembling;
        public static string opTwoDissasembly;


        public In_LoadStore(Registers toRegister, Memory toMemory, uint toInstruction, bool toDisassembling)
        {
            myRegister = toRegister;
            myMemory = toMemory;
            disassembling = toDisassembling;
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
                        //to string
                        
                        executeLDM();
                        break;

                    default:
                        //to string
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
                        //to string
                        executeSTM();
                        break;

                    default:
                        //to string
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
            if (bs == 0)
            {
                if (!disassembling) { myRegister.WriteWord(rd, myMemory.ReadWord(addr)); }
                disassembly = "ldr" + Instructions.firstOpcode + " ";
            }
            else if (bs == 1) 
            {
                if (!disassembling) { myRegister.WriteWord(rd, myMemory.ReadByte(addr)); }
                disassembly = "ldrb" + Instructions.firstOpcode + " ";
            }

            disassembly += "r" + Convert.ToString(rd);

            //writeback?
            if (p == 1 && w == 1)
            {
                if (!disassembling) { myRegister.WriteWord(rn, addr); }
                disassembly += "!";
            }

            disassembly += ", [r" + Convert.ToString(rn) + opTwoDissasembly + "]";
        }

        public void executeSTR()
        {
            int operand2int = Convert.ToInt32(operand2);
            if (u == 0) { operand2int *= -1; }
            //gettin rn
            if (p == 1) { addr = (uint)(myRegister.ReadWord(rn) + operand2int); }
            else if (p == 0) { addr = myRegister.ReadWord(rn); }
            //performing the store
            if (bs == 0)
            {
                if (!disassembling) { myMemory.WriteWord(addr, myRegister.ReadWord(rd)); }
                disassembly = "str" + Instructions.firstOpcode + " ";
            }
            else if (bs == 1)
            {
                if (!disassembling) { myMemory.WriteByte(addr, myRegister.ReadByte(rd)); }
                disassembly = "strb" + Instructions.firstOpcode + " ";
            }

            disassembly += "r" + Convert.ToString(rd);

            //writeback?
            if (p == 1 && w == 1)
            {
                if (!disassembling) { myRegister.WriteWord(rn, addr); }
                disassembly += "!";
            }

            disassembly += ", [r" + Convert.ToString(rn) + opTwoDissasembly + "]";
        }

        public void executeLDM()
        {
            //rn is a pointer in memory
            //get num of bits set (operand2 = num of bits set)
            //if increment after, start at address in memory rn. 
            uint memAddr = myRegister.ReadWord(rn);
            bool incrAfter = false;
            bool decrBefore = false;


            //calculate p/u flags
            if (p == 0 && u == 1)
            {
                incrAfter = true;
                disassembly = "ldmfd" + Instructions.firstOpcode + " ";
            }
            else if (p == 1 && u == 0)
            {
                decrBefore = true;
                disassembly = "ldmea" + Instructions.firstOpcode + " ";
            }

            string addresses = "{";

            //do the load
            BitArray myBA = new BitArray(BitConverter.GetBytes(Instructions.getSectionValue(15, 0, instruction)));
            for (uint i = 0; i < 16; i++)
            {
                if (myBA.Get((int)i))
                {
                    addresses += "r" + i + ", ";
                    if (incrAfter && !disassembling)
                    {
                        myRegister.WriteWord(i, myMemory.ReadWord(memAddr));
                        memAddr += 4;
                    }
                    else if (decrBefore && !disassembling)
                    {
                        myRegister.WriteWord(i, myMemory.ReadWord(memAddr - (operand2 * 4)));
                        memAddr += 4;
                    }
                }
            }
            addresses = addresses.Remove(addresses.Length - 2);
            addresses += "}";
            if (rn == 13)
            {
                disassembly += "sp";
            }
            else
            {
                disassembly += "r" + rn;
            }
            //do the writeBack
            if (decrBefore && w == 1)
            {
                if (!disassembling) { myRegister.WriteWord(rn, myRegister.ReadWord(rn) - (operand2 * 4)); }
                disassembly += "!";
            }
            if (incrAfter && w == 1)
            {
                if (!disassembling) { myRegister.WriteWord(rn, myRegister.ReadWord(rn) + (operand2 * 4)); }
                disassembly += "!";
            }
            disassembly += ", " + addresses;
        }

        public void executeSTM()
        {
            //rn is a pointer in memory
            //get num of bits set (operand2 = num of bits set)
            //if increment after, start at address in memory rn. 
            uint memAddr = myRegister.ReadWord(rn);
            bool incrAfter = false;
            bool decrBefore = false;

            //calculate p/u flags
            if (p == 0 && u == 1)
            {
                incrAfter = true;
                disassembly = "stmia" + Instructions.firstOpcode + " ";
            }
            else if (p == 1 && u == 0)
            {
                decrBefore = true;
                disassembly = "stmfd" + Instructions.firstOpcode + " ";
            }

            string addresses = "{";

            //do the load
            BitArray myBA = new BitArray(BitConverter.GetBytes(Instructions.getSectionValue(15, 0, instruction)));
            for (uint i = 0; i < 16; i++)
            {
                if (myBA.Get((int)i))
                {
                    addresses += "r" + i + ", ";
                    if (incrAfter && !disassembling)
                    {
                        myMemory.WriteWord(memAddr, myRegister.ReadWord(i));
                        memAddr += 4;
                    }
                    else if (decrBefore && !disassembling)
                    {
                        myMemory.WriteWord(memAddr - (operand2 * 4), myRegister.ReadWord(i));
                        memAddr += 4;
                    }
                }
            }
            addresses = addresses.Remove(addresses.Length - 2);
            addresses += "}";
            if (rn == 13)
            {
                disassembly += "sp";
            }
            else
            {
                disassembly += "r" + rn;
            }
            
            //do the writeBack
            if (decrBefore && w == 1)
            {
                if (!disassembling) { myRegister.WriteWord(rn, myRegister.ReadWord(rn) - (operand2 * 4)); }
                disassembly += "!";
            }
            if (incrAfter && w == 1)
            {
                if (!disassembling) { myRegister.WriteWord(rn, myRegister.ReadWord(rn) + (operand2 * 4)); }
                disassembly += "!";
            }

            disassembly += ", " + addresses;
        }
    }
}
