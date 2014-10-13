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


        public In_Branch(Registers toRegister, Memory toMemory)
        {
            myRegister = toRegister;
            myMemory = toMemory;
        }

        public override void decode()
        {
            ;
        }

        public override void execute()
        {
            //executes decoded instruction
        }
    }
}
