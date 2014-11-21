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
    //Class:        TestComputer
    //Purpose:      Unit tests for the computer class
    class TestComputer
    {
        //Method:       RunTests
        //Purpose:      tests every method in the Computer class
        //Variables:    myOptions - Options handle to options to class
        public static void RunTests(Options myOptions)
        {
            myOptions.SetFileName("ctest.exe");
            Console.WriteLine("testing Computer setup...");
            Computer myTestComp = new Computer(myOptions);
            myTestComp.endRun += new Computer.EventHandler(delegate { });
            myTestComp.putChar += new Computer.EventHandler(delegate { });
            Console.WriteLine("testing Computer Running...");
            myTestComp.step();
            Debug.Assert(myTestComp.getStepNum() == 1);
            myTestComp.Run();
            Debug.Assert(myTestComp.getStepNum() == 20);
            Console.WriteLine("success!");
            myTestComp.FileStreamClose();
        }
    }
}
