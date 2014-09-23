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
        public event EventHandler endRun;
        public EventArgs e = null;
        public delegate void EventHandler(Computer c, EventArgs e);
        private bool trace;

        public Computer(Options toOptions)
        {
            myOptions = toOptions;
            myRam = new Memory(myOptions.GetMemSize());
            myLoader = new Loader(myOptions, myRam);
            myLoader.Load();
            myRegisters = new Registers();
            myCPU = new CPU(myRam, myRegisters, myLoader.getProgramCounter());
            trace = true;
            //delete whatever trace file exists
            //create new trace file 
        }

        public void Run()
        {
            uint keepRunning = myCPU.Fetch();
            while (keepRunning != 0)
            {
                myCPU.Decode();
                myCPU.Execute();
                keepRunning = myCPU.Fetch();
                //write to trace file IF trace bool is true
            }
            endRun(this, e);
        }

        public void step()
        {
            myCPU.Fetch();
            myCPU.Decode();
            myCPU.Execute();
            endRun(this, e);
        }

        public CPU getCPU()
        {
            return myCPU;
        }

        public Registers getRegisters()
        {
            return myRegisters;
        }

        public Memory getMemory()
        {
            return myRam;
        }
    }
}
