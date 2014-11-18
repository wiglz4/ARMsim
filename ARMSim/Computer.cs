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
        public bool disassembling = false;
        private static StreamWriter myTracer;
        private int stepNum;
        public static int storedBranchPC;

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
            stepNum = 0;
            storedBranchPC = 0;
        }

        //Method:       Run
        //Purpose:      Runs through entire simulated program using CPU fetch-decode-execute.
        public void Run()
        {
            uint curCommand = myCPU.Fetch();
            while (curCommand != 0 && !abort)
            {
                storedBranchPC = 0;
                stepNum++;
                if (myCPU.Decode(curCommand))
                {
                    if (Instructions.dontDoItBro == false)
                    {
                        myCPU.Execute();
                    }
                    else
                    {
                        myRegisters.IncrCounter();
                    }
                    curCommand = myCPU.Fetch();
                    if (trace) {
                        if (storedBranchPC == 0)
                        {
                            WriteTrace((int)myRegisters.ReadWord(15) - 12);
                        }
                        else
                        {
                            WriteTrace(storedBranchPC);
                        }
                    }
                }
                else
                {
                    if (trace) { WriteTrace((int)myRegisters.ReadWord(15) - 8); }
                    curCommand = 0;
                }
            }
            endRun(this, e);
        }

        //Method:       Step
        //Purpose:      Steps through one fetch-decode-execute cycle.
        public void step()
        {
            //when im disassembling i will be running through here and simply not doing the final sub-execute hence the if !disassembly
            storedBranchPC = 0;
            uint curCommand = myCPU.Fetch();
            if (curCommand != 0)
            {
                if (!disassembling) { stepNum++; }
                if (myCPU.Decode(curCommand))
                {
                    if (Instructions.dontDoItBro == false)
                    {
                        myCPU.Execute();
                    }
                    else
                    {
                        myRegisters.IncrCounter();
                    }
                    if (trace && !disassembling) 
                    {
                        if (storedBranchPC == 0)
                        {
                            WriteTrace((int)myRegisters.ReadWord(15) - 12);
                        }
                        else
                        {
                            WriteTrace(storedBranchPC);
                        }
                    }
                }
                else
                {
                    if (trace && !disassembling) { WriteTrace((int)myRegisters.ReadWord(15) - 8); }
                }
                if (!disassembling) { endRun(this, e); }
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

        public void WriteTrace(int PC)
        {
            FileStreamOpen();
            myTracer.WriteLine(String.Format("{0:D6}", stepNum) + " " + String.Format("{0:X8}", PC) + " [sys] " + getMemory().TestFlag(0) + getMemory().TestFlag(1) + getMemory().TestFlag(2) + getMemory().TestFlag(3) + " 0=" + String.Format("{0:X8}", myRegisters.ReadWord(0)) + " 1=" + String.Format("{0:X8}", myRegisters.ReadWord(1)) + " 2=" + String.Format("{0:X8}", myRegisters.ReadWord(2)) + " 3=" + String.Format("{0:X8}", myRegisters.ReadWord(3)));
            myTracer.WriteLine("        4=" + String.Format("{0:X8}", myRegisters.ReadWord(4)) + " 5=" + String.Format("{0:X8}", myRegisters.ReadWord(5)) + " 6=" + String.Format("{0:X8}", myRegisters.ReadWord(6)) + " 7=" + String.Format("{0:X8}", myRegisters.ReadWord(7)) + " 8=" + String.Format("{0:X8}", myRegisters.ReadWord(8)) + " 9=" + String.Format("{0:X8}", myRegisters.ReadWord(9)));
            myTracer.WriteLine("       10=" + String.Format("{0:X8}", myRegisters.ReadWord(10)) + " 11=" + String.Format("{0:X8}", myRegisters.ReadWord(11)) + " 12=" + String.Format("{0:X8}", myRegisters.ReadWord(12)) + " 13=" + String.Format("{0:X8}", myRegisters.ReadWord(13)) + " 14=" + String.Format("{0:X8}", myRegisters.ReadWord(14)));
            FileStreamClose();
        }
    }
}
