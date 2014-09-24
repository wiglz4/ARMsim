using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace ARMSim
{
    //BEGIN UNIT TESTING
    //Class:        TestCPU
    //Purpose:      Unit tests for the CPU class
    class TestCPU
    {
        //Method:       RunTests
        //Purpose:      tests key methods in the CPU class
        //Variables:    myOptions - Options handle to options to class
        public static void RunTests(Options myOptions)
        {
            myOptions.SetFileName("test1.exe");
            Console.WriteLine("testing CPU...");
            Computer myTestComp = new Computer(myOptions);
            myTestComp.endRun += new Computer.EventHandler(delegate { });
            Console.WriteLine("testing Fetch, Decode, and Execute methods...");
            myTestComp.step();
            Debug.Assert(myTestComp.getStepNum() == 1);
            Console.WriteLine("success!");
            myTestComp.FileStreamClose();
        }
    }
}
