using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public bool abort = false;
        private static StreamWriter myTracer;
        private int stepNum;


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

        public void Run()
        {
            uint keepRunning = myCPU.Fetch();
            while (keepRunning != 0 && !abort)
            {
                stepNum++;
                myCPU.Decode();
                myCPU.Execute();

                if (trace)
                {
                    myTracer.WriteLine(String.Format("{0:D4}", stepNum) + " " + keepRunning + " " + myRam.getMDF() + " " + myCPU.getFlagN() + myCPU.getFlagZ() + myCPU.getFlagC() + myCPU.getFlagF() + " 0=" + String.Format("{0:X8}", myRegisters.ReadWord(0)) + " 1=" + String.Format("{0:X8}", myRegisters.ReadWord(1)) + " 2=" + String.Format("{0:X8}", myRegisters.ReadWord(2)) + " 3=" + String.Format("{0:X8}", myRegisters.ReadWord(3)));
                    myTracer.WriteLine("4=" + String.Format("{0:X8}", myRegisters.ReadWord(4)) + " 5=" + String.Format("{0:X8}", myRegisters.ReadWord(5)) + " 6=" + String.Format("{0:X8}", myRegisters.ReadWord(6)) + " 7=" + String.Format("{0:X8}", myRegisters.ReadWord(7)) + " 8=" + String.Format("{0:X8}", myRegisters.ReadWord(8)) + " 9=" + String.Format("{0:X8}", myRegisters.ReadWord(9)));
                    myTracer.WriteLine("10=" + String.Format("{0:X8}", myRegisters.ReadWord(10)) + " 11=" + String.Format("{0:X8}", myRegisters.ReadWord(11)) + " 12=" + String.Format("{0:X8}", myRegisters.ReadWord(12)) + " 13=" + String.Format("{0:X8}", myRegisters.ReadWord(13)) + " 14=" + String.Format("{0:X8}", myRegisters.ReadWord(14)));
                }

                keepRunning = myCPU.Fetch();
            }
            endRun(this, e);
        }

        public void step()
        {
            stepNum++;
            uint keepRunning = myCPU.Fetch();
            if (trace)
            {
                myTracer.WriteLine(String.Format("{0:D4}", stepNum) + " " + keepRunning + " " + myRam.getMDF() + " " + myCPU.getFlagN() + myCPU.getFlagZ() + myCPU.getFlagC() + myCPU.getFlagF() + " 0=" + String.Format("{0:X8}", myRegisters.ReadWord(0)) + " 1=" + String.Format("{0:X8}", myRegisters.ReadWord(1)) + " 2=" + String.Format("{0:X8}", myRegisters.ReadWord(2)) + " 3=" + String.Format("{0:X8}", myRegisters.ReadWord(3)));
                myTracer.WriteLine("4=" + String.Format("{0:X8}", myRegisters.ReadWord(4)) + " 5=" + String.Format("{0:X8}", myRegisters.ReadWord(5)) + " 6=" + String.Format("{0:X8}", myRegisters.ReadWord(6)) + " 7=" + String.Format("{0:X8}", myRegisters.ReadWord(7)) + " 8=" + String.Format("{0:X8}", myRegisters.ReadWord(8)) + " 9=" + String.Format("{0:X8}", myRegisters.ReadWord(9)));
                myTracer.WriteLine("10=" + String.Format("{0:X8}", myRegisters.ReadWord(10)) + " 11=" + String.Format("{0:X8}", myRegisters.ReadWord(11)) + " 12=" + String.Format("{0:X8}", myRegisters.ReadWord(12)) + " 13=" + String.Format("{0:X8}", myRegisters.ReadWord(13)) + " 14=" + String.Format("{0:X8}", myRegisters.ReadWord(14)));
            }
            if (keepRunning != 0)
            {
                myCPU.Decode();
                myCPU.Execute();
                endRun(this, e);
            }
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

        public void setTrace(bool value)
        {
            trace = value;
        }

        public void FileStreamClose()
        {
            myTracer.Close();
        }

        public void FileStreamOpen()
        {
            myTracer = File.AppendText("trace.log");
            myTracer.AutoFlush = true;
        }
    }
}
