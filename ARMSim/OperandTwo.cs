using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSim
{
    class OperandTwo
    {
        int type;
        uint instruction, rm, shiftAmt, shift;
        Registers myRegisters;

        public OperandTwo(Registers toRegisters, uint toInstruction)
        {
            instruction = toInstruction;
            myRegisters = toRegisters;
        }

        public uint getValue()
        {
            if (Instructions.getSectionValue(25, 25, instruction) == 1)
            {
                if (Instructions.getSectionValue(11, 8, instruction) == 0)
                {
                    In_DataProcessing.opTwoDissasembly = '#' + Convert.ToString(Instructions.getSectionValue(7, 0, instruction));
                }
                type = 3;
                return (Instructions.getSectionValue(7, 0, instruction) >> (int)(2 * Instructions.getSectionValue(11, 8, instruction))) | (Instructions.getSectionValue(7, 0, instruction) << (32 - (int)(2 * Instructions.getSectionValue(11, 8, instruction))));
            }
            
            rm = Instructions.getSectionValue(3, 0, instruction);
            shift = Instructions.getSectionValue(6, 5, instruction);

            if (Instructions.getSectionValue(4, 4, instruction) == 1)
            {
                type = 2;
                shiftAmt = myRegisters.ReadWord(Instructions.getSectionValue(11, 8, instruction));
                return getShiftDone(); //calculate shift
            }
            else
            {
                type = 1;
                shiftAmt = Instructions.getSectionValue(11, 7, instruction);
                return getShiftDone(); //calculate shift
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
                    //ASR
                    //CAST TO INT (logic shift becomes arithmaic)
                    return (uint)((int)rm >> (int)shiftAmt);
                case 2:
                    //LSR
                    return (rm >> (int)shiftAmt);
                default:
                    //ROR
                    return (rm >> (int)shiftAmt) | (rm << (int)(32 - shiftAmt));
            }
        }
    }
}