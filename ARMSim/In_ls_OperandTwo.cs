using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ARMSim
{
    class In_ls_OperandTwo
    {
        uint type;
        uint instruction, rm, shiftAmt, shift;

        Registers myRegisters;

        public In_ls_OperandTwo(Registers toRegisters, uint toInstruction)
        {
            instruction = toInstruction;
            myRegisters = toRegisters;
        }

        public uint getValue()
        {
            type = Instructions.getSectionValue(27, 25, instruction);
            if (type == 2)
            {
                //immediate
                return Instructions.getSectionValue(11, 0, instruction);
            }
            else if (type == 3)
            {
                //shift
                rm = Instructions.getSectionValue(3, 0, instruction);
                shift = Instructions.getSectionValue(6, 5, instruction);
                shiftAmt = Instructions.getSectionValue(11, 7, instruction);
                return getShiftDone();
            }
            else
            {
                //register list
                uint numOfSetBits = 0;
                foreach (bool x in new BitArray(BitConverter.GetBytes(Instructions.getSectionValue(15, 0, instruction))))
                {
                    if (x)
                        numOfSetBits++;
                }
                return numOfSetBits;
            }
        }

        public uint getShiftDone()
        {
            rm = myRegisters.ReadWord(rm);
            switch (shift)
            {
                case 0:
                    //LSL
                    return (rm << (int)shiftAmt);
                case 1:
                    //LSR
                    return (rm >> (int)shiftAmt);
                case 2:
                    //ASR
                    //CAST TO INT (logic shift becomes arithmaic)
                    return (uint)((int)rm >> (int)shiftAmt);
                default:
                    //ROR
                    return (rm >> (int)shiftAmt) | (rm << (int)(32 - shiftAmt));
            }
        }
    }
}
