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
        string uSign = "";
        string strShiftAmt = "";

        Registers myRegisters;

        public In_ls_OperandTwo(Registers toRegisters, uint toInstruction)
        {
            instruction = toInstruction;
            myRegisters = toRegisters;
            if (Instructions.getSectionValue(23, 23, instruction) == 0)
            {
                uSign = "-";
            }
        }

        public uint getValue()
        {
            type = Instructions.getSectionValue(27, 25, instruction);
            if (type == 2)
            {
                //immediate
                if (Instructions.getSectionValue(11, 0, instruction) != 0)
                {
                    In_LoadStore.opTwoDissasembly = ", #" + uSign + Convert.ToString(Instructions.getSectionValue(11, 0, instruction));
                }
                else
                {
                    In_LoadStore.opTwoDissasembly = "";
                }
                return Instructions.getSectionValue(11, 0, instruction);
            }
            else if (type == 3)
            {
                //shift
                
                rm = Instructions.getSectionValue(3, 0, instruction);
                shift = Instructions.getSectionValue(6, 5, instruction);
                shiftAmt = Instructions.getSectionValue(11, 7, instruction);

                strShiftAmt = '#' + Convert.ToString(Instructions.getSectionValue(11, 7, instruction));
                In_LoadStore.opTwoDissasembly = ", " + uSign + "r" + Convert.ToString(rm);

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
                    if (shiftAmt != 0)
                    {
                        In_LoadStore.opTwoDissasembly += ", lsl " + strShiftAmt;
                    }
                    return (rm << (int)shiftAmt);
                case 1:
                    //LSR
                    if (shiftAmt != 0)
                    {
                        In_LoadStore.opTwoDissasembly += ", lsr " + strShiftAmt;
                    }
                    return (rm >> (int)shiftAmt);
                case 2:
                    //ASR
                    //CAST TO INT (logic shift becomes arithmaic)
                    if (shiftAmt != 0)
                    {
                        In_LoadStore.opTwoDissasembly += ", asr " + strShiftAmt;
                    }
                    return (uint)((int)rm >> (int)shiftAmt);
                default:
                    //ROR
                    In_LoadStore.opTwoDissasembly += ", ror " + strShiftAmt;
                    return (rm >> (int)shiftAmt) | (rm << (int)(32 - shiftAmt));
            }
        }
    }
}
