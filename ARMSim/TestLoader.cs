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
    //Class:        TestLoader
    //Purpose:      Unit tests for the loader class
    class TestLoader
    {
        //Method:       RunTests
        //Purpose:      tests every method in the Loader class
        //Variables:    myOptions - Options handle to options to class
        public static void RunTests(Options myOptions)
        {
            myOptions.SetFileName("test1.exe");
            Memory myRam = new Memory(myOptions.GetMemSize());
            Loader myLoader = new Loader(myOptions, myRam);
            Console.WriteLine("testing Loader...");
            myLoader.Load();
            Console.Write("verifying MD5 hash...");
            Debug.Assert(myRam.getMDF() == "3500A8BEF72DFED358B25B61B7602CF1");
            Console.WriteLine("success!");
        }
    }
}
