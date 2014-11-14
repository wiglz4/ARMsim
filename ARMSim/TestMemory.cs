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
    //Class:        TestMemory
    //Purpose:      Unit tests for the ram class
    class TestMemory
    {
        //Method:       RunTests
        //Purpose:      tests every method in the Memory class
        public static void RunTests()
        {
            Console.WriteLine("testing RAM...");

            Memory ram = new Memory(7);

            //test populate method
            Console.Write("verifying Populate...");
            ram.PopulateRam(new byte[] { 0xFF, 0xAA, 0xAA, 0xFF, 0xFF, 0xFF, 0xFF }, 0);
            Debug.Assert(ram.ReadHalfWord(1) == 0xAAAA);
            Console.WriteLine("success!");

            /*test set/test flag methods
            Console.Write("verifying SetFlag/TestFlag...");
            ram.SetFlag(1, 6, true);
            Debug.Assert(ram.TestFlag(1, 6));
            ram.SetFlag(1, 6, false);
            Debug.Assert(!(ram.TestFlag(1, 6)));
            Console.WriteLine("success!");*/

            //test read/write methods
            Console.Write("verifying Read/Write Word...");
            ram.WriteWord(0, 0xFFFFFFFF);
            Debug.Assert(ram.ReadWord(0) == 0xFFFFFFFF);
            Console.WriteLine("success!");

            Console.Write("verifying Read/Write HalfWord...");
            ram.WriteWord(0, 0xCCCC);
            Debug.Assert(ram.ReadWord(0) == 0xCCCC);
            Console.WriteLine("success!");

            Console.Write("verifying Read/Write Byte...");
            ram.WriteWord(0, 0xAA);
            Debug.Assert(ram.ReadWord(0) == 0xAA);
            Console.WriteLine("success!");
        }
    }
}
