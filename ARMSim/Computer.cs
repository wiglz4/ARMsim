using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSim
{
    class Computer
    {
        private Options myOptions;
        private Memory myRam;
        private Loader myLoader;
        private Registers myRegisters;
        private CPU myCPU;
        private bool trace;

        public Computer(Options toOptions)
        {
            myOptions = toOptions;
            myRam = new Memory(myOptions.GetMemSize());
            myLoader = new Loader(myOptions, myRam);
            myLoader.Load();
            myRegisters = new Registers();
            myCPU = new CPU(myRam, myRegisters);
            trace = true;
        }

        public void run()
        {
            uint keepRunning = myCPU.Fetch();
            while (keepRunning != 0)
            {
                myCPU.Decode();
                myCPU.Execute();
                keepRunning = myCPU.Fetch();
            } 
        }

        public void step()
        {
            myCPU.Fetch();
            myCPU.Decode();
            myCPU.Execute();
        }
    }
}
