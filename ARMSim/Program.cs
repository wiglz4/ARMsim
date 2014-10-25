using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ARMSim
{
    //Class:        Program
    //Purpose:      Accepts user input then initiates classes and runs program.
    class Program
    {
        //Method:       Main
        //Purpose:      Accepts user input then initiates classes and runs program.
        //Variables:    args -   String array of command line arguments.
        [STAThread]
        static void Main(string[] args)
        {
            Options myOptions = new Options();
            myOptions.Parse(args);

            //if myoptions.getexec && myoptions.getloadfile
            //new computer
            //run computer
            //exit program

            if (myOptions.GetTest())
            {
                TestMemory.RunTests();
                TestLoader.RunTests(myOptions);
                TestComputer.RunTests(myOptions);
                TestCPU.RunTests(myOptions);
                TestRegisters.RunTests(myOptions);
                Environment.Exit(0);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ARMSimForm(myOptions));
        }
    }
}