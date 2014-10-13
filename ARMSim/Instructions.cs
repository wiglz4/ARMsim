using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSim
{
   
    abstract class Instructions
    {
        public static string disassembly;
        //stores generic information about all decoded arm instructions

        //decodes bits 27-26 and returns specified instruction type
        public static Instructions decode(uint instruction, Registers myRegisters, Memory myMemory)
        {
            uint type = getSectionValue(27, 26, instruction);

            //handle specials before this
            if (type == 0)
            {
                return new In_DataProcessing(myRegisters, myMemory, instruction);
            }
            if (type == 1)
            {
                return new In_LoadStore(myRegisters, myMemory);
            }
            else
            {
                return new In_Branch(myRegisters, myMemory);
            }
        }

        public abstract void decode();
        public abstract void execute();


        //returns uint value of a specific selection of bits
        public static uint getSectionValue(int upper, int lower, uint instruction)
        {
            return (instruction << (31 - upper)) >> (31 + lower - upper);
        }
    }
}
