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
    //Class:        TestRegisters
    //Purpose:      Unit tests for the Registers class
    class TestRegisters
    {
        //Method:       RunTests
        //Purpose:      tests key methods in the Registers class
        //Variables:    myOptions - Options handle to options class
        public static void RunTests(Options myOptions)
        {
            myOptions.SetFileName("test1.exe");
            Console.WriteLine("testing Registers...");
            Computer myTestComp = new Computer(myOptions);
            myTestComp.endRun += new Computer.EventHandler(delegate { });
            Console.WriteLine("testing Read Word...");
            Debug.Assert(myTestComp.getRegisters().ReadWord(15) == 320);
            Console.WriteLine("success!");
            myTestComp.FileStreamClose();
        }
    }
}
