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
    //Class:        Options
    //Purpose:      Parses user input and stores relevant information in an Option object.
    public class Options
    {
        private int memSize = 32768;
        private string fileName;
        private bool test;

        //Method:       Parse
        //Purpose:      Parses user input and stores relevant information in an Option object.
        //Variables:    args -   String array of command line arguments.
        public void Parse(string[] args)
        {
            //iterate through command line arguments and fill out variables
            Debug.WriteLine("Options.Parse: parsing command line options");

            if (args.Length < 1)
            {
                Console.WriteLine("You have entered an invalid option \n Valid options are: \n --test: Run unit tests and quit \n --mem: specify simluated RAM size \n --load: specify ELF file to open \n");
                Environment.Exit(0);
            }

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--load":
                        try
                        {
                            fileName = args[i + 1];
                        }
                        catch
                        {
                            Console.WriteLine("please supply a file to load");
                            Environment.Exit(0);
                        }
                        i++;
                        Debug.WriteLine("Options.Parse: --load and file accepted.");
                        break;

                    case "--mem":
                        try
                        {
                            memSize = Convert.ToInt32(args[i + 1]);
                            if (memSize > 10000000) { throw new Exception(); }
                        }
                        catch
                        {
                            Console.WriteLine("incorrect ram formatting (10MB max) quitting");
                            Environment.Exit(0);
                        }
                        i++;
                        Debug.WriteLine("Options.Parse: --mem accepted.");
                        break;

                    case "--test":
                        //Run unit tests and quit
                        test = true;
                        Debug.WriteLine("Options.Parse: --test accepted. Unit tests will Run.");
                        break;

                    default:
                        Console.WriteLine("You have entered an invalid option \n Valid options are: \n --test: Run unit tests and quit \n --mem: specify simluated RAM size \n --load: specify ELF file to open \n");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        //Method:       GetMemSize
        //Purpose:      Returns memSize.
        public int GetMemSize()
        {
            return memSize;
        }

        //Method:       GetFileName
        //Purpose:      Returns fileName.
        public string GetFileName()
        {
            return fileName;
        }

        //Method:       GetTest
        //Purpose:      Returns test.
        public bool GetTest()
        {
            return test;
        }

        //Method:       SetFileName
        //Purpose:      Sets the file name to a new variable.
        //Variables:    toFileName - string of new filename
        public void SetFileName(string toFileName)
        {
            fileName = toFileName;
        }
    }
}
