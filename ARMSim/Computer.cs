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
        private bool stop;

        public Computer(Options toOptions)
        {
            myOptions = toOptions;
            myRam = new Memory(myOptions.GetMemSize());
            myLoader = new Loader(myOptions, myRam);
            myLoader.Load();
            myRegisters = new Registers();
            myCPU = new CPU(myRam, myRegisters);
            trace = true;
            stop = false;
            //delete whatever trace file exists
            //create new trace file 
        }

        public void Run()
        {
            uint keepRunning = myCPU.Fetch();
            while (keepRunning != 0 && stop != true)
            {
                myCPU.Decode();
                myCPU.Execute();
                keepRunning = myCPU.Fetch();
                //write to trace file IF trace bool is true
            } 
        }

        public void step()
        {
            myCPU.Fetch();
            myCPU.Decode();
            myCPU.Execute();
        }

        public void setStop(bool val)
        {
            stop = val;
        }

        public bool getStop()
        {
            return stop;
        }
        public CPU getCPU()
        {
            return myCPU;
        }
    }
}
