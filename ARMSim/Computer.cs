using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARMSim
{
    //Class:        Computer
    //Purpose:      Represents simulated computer.
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
        public bool abort = false;
        private static StreamWriter myTracer;
        private int stepNum;

        //Method:       Constructor
        //Purpose:      Sets Computer up for use.
        //Variables:    toOptions -   Options parsed from command line input.
        public Computer(Options toOptions)
        {
            myOptions = toOptions;
            myRam = new Memory(myOptions.GetMemSize());
            myLoader = new Loader(myOptions, myRam);
            myLoader.Load();
            myRegisters = new Registers();
            myCPU = new CPU(myRam, myRegisters, myLoader.getProgramCounter());
            trace = true;
            FileStream myFileStream = new FileStream("trace.log", FileMode.Create);
            myFileStream.Close();
            myTracer = File.AppendText("trace.log");
            myTracer.AutoFlush = true;
            stepNum = 0;
        }

        //Method:       Run
        //Purpose:      Runs through entire simulated program using CPU fetch-decode-execute.
        public void Run()
        {
            uint curCommand = myCPU.Fetch();
            while (curCommand != 0 && !abort)
            {
                stepNum++;
                if (myCPU.Decode(curCommand))
                {
                    myCPU.Execute();
                    curCommand = myCPU.Fetch();
                    if (trace)
                    {
                        WriteTrace(12);
                    }
                }
                else
                {
                    if (trace) { WriteTrace(8); }
                    curCommand = 0;
                }
            }
            endRun(this, e);
        }

        //Method:       Step
        //Purpose:      Steps through one fetch-decode-execute cycle.
        public void step()
        {
            stepNum++;
            uint curCommand = myCPU.Fetch();
            if (curCommand != 0)
            {
                if (myCPU.Decode(curCommand))
                {
                    myCPU.Execute();
                    if (trace) { WriteTrace(12); }
                }
                else
                {
                    if (trace) { WriteTrace(8); }
                }
                endRun(this, e);
            }
        }

        //Method:       FileStreamClose
        //Purpose:      Closes the trace file.
        public void FileStreamClose()
        {
            myTracer.Close();
        }

        //Method:       FileStreamOpen
        //Purpose:      Opens and flushes the trace file.
        public void FileStreamOpen()
        {
            myTracer = File.AppendText("trace.log");
            myTracer.AutoFlush = true;
        }

        //Method:       getCPU
        //Purpose:      Returns myCPU
        public CPU getCPU()
        {
            return myCPU;
        }

        //Method:       getRegisters
        //Purpose:      Returns myRegisters
        public Registers getRegisters()
        {
            return myRegisters;
        }

        //Method:       getMemory
        //Purpose:      Returns myRam
        public Memory getMemory()
        {
            return myRam;
        }

        //Method:       setTrace
        //Purpose:      toogles trace value
        //Variables:    value - Bool to set trace to
        public void setTrace(bool value)
        {
            trace = value;
        }

        public int getStepNum()
        {
            return stepNum;
        }

        public void WriteTrace(int subPCamt)
        {
            myTracer.WriteLine(String.Format("{0:D6}", stepNum) + " " + String.Format("{0:X8}", myRegisters.ReadWord(15) - subPCamt) + " " + myRam.getMDF() + " " + myCPU.getFlagN() + myCPU.getFlagZ() + myCPU.getFlagC() + myCPU.getFlagF() + " 0=" + String.Format("{0:X8}", myRegisters.ReadWord(0)) + " 1=" + String.Format("{0:X8}", myRegisters.ReadWord(1)) + " 2=" + String.Format("{0:X8}", myRegisters.ReadWord(2)) + " 3=" + String.Format("{0:X8}", myRegisters.ReadWord(3)));
            myTracer.WriteLine("        4=" + String.Format("{0:X8}", myRegisters.ReadWord(4)) + " 5=" + String.Format("{0:X8}", myRegisters.ReadWord(5)) + " 6=" + String.Format("{0:X8}", myRegisters.ReadWord(6)) + " 7=" + String.Format("{0:X8}", myRegisters.ReadWord(7)) + " 8=" + String.Format("{0:X8}", myRegisters.ReadWord(8)) + " 9=" + String.Format("{0:X8}", myRegisters.ReadWord(9)));
            myTracer.WriteLine("       10=" + String.Format("{0:X8}", myRegisters.ReadWord(10)) + " 11=" + String.Format("{0:X8}", myRegisters.ReadWord(11)) + " 12=" + String.Format("{0:X8}", myRegisters.ReadWord(12)) + " 13=" + String.Format("{0:X8}", myRegisters.ReadWord(13)) + " 14=" + String.Format("{0:X8}", myRegisters.ReadWord(14)));
                    
        }
    }
}
