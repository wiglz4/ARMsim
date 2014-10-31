using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSim
{
    class In_dp_OperandTwo
    {
        int type;
        uint instruction, rm, shiftAmt, shift;
        string strShiftAmt;
        Registers myRegisters;

        public In_dp_OperandTwo(Registers toRegisters, uint toInstruction)
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
                In_DataProcessing.opTwoDissasembly = '#' + Convert.ToString((Instructions.getSectionValue(7, 0, instruction) >> (int)(2 * Instructions.getSectionValue(11, 8, instruction))) | (Instructions.getSectionValue(7, 0, instruction) << (32 - (int)(2 * Instructions.getSectionValue(11, 8, instruction)))));
                return (Instructions.getSectionValue(7, 0, instruction) >> (int)(2 * Instructions.getSectionValue(11, 8, instruction))) | (Instructions.getSectionValue(7, 0, instruction) << (32 - (int)(2 * Instructions.getSectionValue(11, 8, instruction))));
            }

            rm = Instructions.getSectionValue(3, 0, instruction);
            shift = Instructions.getSectionValue(6, 5, instruction);


            //this instruction will be shifting rn by something
            if (rm == 15)
            {
                In_DataProcessing.opTwoDissasembly = "pc";
            }
            else
            {
                In_DataProcessing.opTwoDissasembly = 'r' + Convert.ToString(rm);
            }


            if (Instructions.getSectionValue(4, 4, instruction) == 1)
            {
                type = 2;
                shiftAmt = myRegisters.ReadWord(Instructions.getSectionValue(11, 8, instruction));
                strShiftAmt = 'r' + Convert.ToString(Instructions.getSectionValue(11, 8, instruction));
                return getShiftDone(); //calculate shift
            }
            else
            {
                type = 1;
                shiftAmt = Instructions.getSectionValue(11, 7, instruction);
                strShiftAmt = '#' + Convert.ToString(Instructions.getSectionValue(11, 7, instruction));
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
                    if (shiftAmt != 0) 
                    {
                        In_DataProcessing.opTwoDissasembly += ", lsl " + strShiftAmt;
                    }
                    return (rm << (int)shiftAmt);
                case 1:
                    //LSR
                    if (shiftAmt != 0)
                    {
                        In_DataProcessing.opTwoDissasembly += ", lsr " + strShiftAmt;
                    }
                    return (rm >> (int)shiftAmt);
                case 2:
                    //ASR
                    //CAST TO INT (logic shift becomes arithmaic)
                    if (shiftAmt != 0)
                    {
                        In_DataProcessing.opTwoDissasembly += ", asr " + strShiftAmt;
                    }
                    return (uint)((int)rm >> (int)shiftAmt);
                default:
                    //ROR
                    In_DataProcessing.opTwoDissasembly += ", ror " + strShiftAmt;
                    return (rm >> (int)shiftAmt) | (rm << (int)(32 - shiftAmt));
            }
        }
    }
}