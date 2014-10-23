﻿using System;
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

            //test MOV register
            myTestComp.getRegisters().WriteWord(5, 0);
            myTestComp.getRegisters().WriteWord(3, 4);
            myTestComp.getCPU().Decode((uint)0xE1A05003); //mov r5, r3
            myTestComp.getCPU().Execute(); //mov r5, r3
            Debug.Assert(myTestComp.getRegisters().ReadWord(5) == 4);

            //test MOV immediate
            myTestComp.getCPU().Decode((uint)0xe3a02030); //mov r2, 48
            myTestComp.getCPU().Execute(); //mov r2, 48
            Debug.Assert(myTestComp.getRegisters().ReadWord(2) == 48);
            Debug.Assert(myTestComp.getCPU().disassembly == "mov r2, #48");

            //test MUL
            myTestComp.getRegisters().WriteWord(4, 2);
            myTestComp.getRegisters().WriteWord(2, 8);
            myTestComp.getCPU().Decode((uint)0xE0020294); //mul r2, r4, r2
            myTestComp.getCPU().Execute();  //mul r2, r4, r2
            Debug.Assert(myTestComp.getRegisters().ReadWord(2) == 16);


            Console.WriteLine("success!");
            myTestComp.FileStreamClose();
        }
    }
}
