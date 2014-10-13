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
        Registers myRegister;
        Memory myMemory;


        public In_LoadStore(Registers toRegister, Memory toMemory)
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

        public void executeLDR()
        {

        }

        public void executeSTR()
        {

        }
    }
}
