using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;


namespace ARMSim
{
    public partial class ARMSimForm : Form
    {
        Options theseOptions;
        Computer myComputer;
        Thread myThread;

        public ARMSimForm(Options myOptions)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(ARMSimForm_KeyDown);
            theseOptions = myOptions;
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
            //setup views
            for (int i = 0; i < 16; i++)
            {
                this.RegisterGridView.Rows.Add();
            }
            for (int i = 0; i < 4; i++)
            {
                this.FlagGridView.Rows.Add();
            }
            for (int i = 0; i < 16; i++)
            {
                this.MemGridView.Rows.Add();
            }
            for (int i = 0; i < 10; i++)
            {
                this.StackGridView.Rows.Add();
            }
            for (int i = 0; i < 9; i++)
            {
                this.disassemblyView.Rows.Add();
            }


            //allow to open without any cmd line options
            if (theseOptions.GetFileName() == "")
            {
                this.RunButton.Enabled = false;
                this.StepButton.Enabled = false;
                this.StopButton.Enabled = false;
                this.ResetButton.Enabled = false;
                this.LoadFileButton.Enabled = true;
            }
            else
            {
                //these two lines used to be in run
                myComputer = new Computer(theseOptions);
                myComputer.endRun += new Computer.EventHandler(UpdateAllTheThings);
                //starts running on file that opened program
                this.RunButton_Click(this.RunButton, EventArgs.Empty);
            }
        }

        public void ARMSimForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.O && e.Control)
            {
                if (this.LoadFileButton.Enabled) { this.LoadFileButton_Click(this.LoadFileButton, EventArgs.Empty); }
            }
            if (e.KeyCode == Keys.F5)
            {
                if (this.RunButton.Enabled) { this.RunButton_Click(this.RunButton, EventArgs.Empty); }
            }
            if (e.KeyCode == Keys.F10)
            {
                if (this.StepButton.Enabled) { this.StepButton_Click(this.StepButton, EventArgs.Empty); }
            }
            if (e.KeyCode == Keys.B && e.Control)
            {
                if (this.StopButton.Enabled) { this.StopButton_Click(this.StopButton, EventArgs.Empty); }
            }
            if (e.KeyCode == Keys.T && e.Control)
            {
                if (!this.checkBox1.Checked)
                {
                    this.checkBox1.Checked = true;
                    myComputer.setTrace(true);
                }
                else
                {
                    this.checkBox1.Checked = false;
                    myComputer.setTrace(false);
                }
            }
            if (e.KeyCode == Keys.R && e.Control)
            {
                if (this.ResetButton.Enabled) { this.ResetButton_Click(this.ResetButton, EventArgs.Empty); }
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            //there's a reason i used to create and run computer here instead of in ARMSIMform
            //but i can't remember why...
            this.OpenedFile.Hide();
            myComputer.abort = false;
            this.RunButton.Enabled = false;
            this.StepButton.Enabled = false;
            this.StopButton.Enabled = true;
            this.ResetButton.Enabled = false;
            this.LoadFileButton.Enabled = false;
            myThread = new Thread(myComputer.Run);
            myThread.Start();
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            myThread = new Thread(myComputer.step);
            this.LoadFileButton.Enabled = false;
            this.StepButton.Enabled = false;
            myThread.Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            myComputer.abort = true;
            this.StepButton.Enabled = true;
            this.ResetButton.Enabled = true;
            UpdateAllTheThings(myComputer, EventArgs.Empty);
        }

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog myBox = new OpenFileDialog();
            myBox.InitialDirectory = "c:\\";
            myBox.RestoreDirectory = true;
            myBox.Filter = "Applications (*.exe)|*.exe|All files (*.*)|*.*";
            myBox.ShowDialog();
            theseOptions.SetFileName(myBox.FileName);
            if (theseOptions.GetFileName() != "") 
            {
                myComputer = new Computer(theseOptions);
                myComputer.endRun += new Computer.EventHandler(UpdateAllTheThings);
                this.OpenedFile.Text = theseOptions.GetFileName();
                this.OpenedFile.Show();
                this.ResetButton_Click(this.ResetButton, EventArgs.Empty);
            }
            else
            {
                this.RunButton.Enabled = false;
                this.StepButton.Enabled = false;
                this.StopButton.Enabled = false;
                this.ResetButton.Enabled = false;
                this.LoadFileButton.Enabled = true;
                this.OpenedFile.Text = ".Please select a file to load";
                this.OpenedFile.Show();
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.RunButton.Enabled = true;
            this.StepButton.Enabled = true;
            this.StopButton.Enabled = false;
            this.ResetButton.Enabled = true;
            this.LoadFileButton.Enabled = true;
            this.ResetRegisters();
            this.ResetFlags();
            this.ResetConsoleBox();
            this.ResetMemory();
            this.ResetStack();
            this.ResetDisassembly();
            this.ResetTracer();
            myComputer = new Computer(theseOptions);
            myComputer.endRun += new Computer.EventHandler(UpdateAllTheThings);
        }

        private void UpdateAllTheThings(Computer c, EventArgs e)
        {
            //update buttons
            this.RunButton.Enabled = true;
            this.StepButton.Enabled = true;
            this.StopButton.Enabled = false;
            this.ResetButton.Enabled = true;
            this.LoadFileButton.Enabled = true;

            //update panels
            this.UpdateRegisters();
            this.UpdateFlags();
            this.UpdateConsoleBox();
            this.UpdateMemory();
            this.UpdateStack();
            this.UpdateDisassembly();
            this.UpdateTracer();

            //spit random junk in the text box
        }

        private void UpdateDisassembly()
        {
            this.ResetDisassembly();
            uint savePC = myComputer.getRegisters().ReadWord(15);
            int i = 0;
            //save register 15 in TEMPVAR
            if (myComputer.getStepNum() > 4)
            {
                myComputer.getRegisters().WriteWord(15, savePC - 16);
            }
            else
            {
                myComputer.getRegisters().WriteWord(15, (uint)(savePC - (myComputer.getStepNum() * 4)));
                i = 4 - myComputer.getStepNum();
            }
            //set register 15 4 commands back. (sub 16).
            myComputer.disassembling = true;
            myComputer.getCPU().disassembling = true;
            //set computer.disassembling to true
            //set cpu.disassembling to true

            for (; i < 9; i++)
            {
                myComputer.getCPU().disassembly = "";
                myComputer.step();
                this.disassemblyView.Rows[i].Cells[0].Value = String.Format("{0:X8}", myComputer.getRegisters().ReadWord(15) - 12);
                this.disassemblyView.Rows[i].Cells[1].Value = String.Format("{0:X8}", myComputer.getCPU().unDecodedInstruction);
                this.disassemblyView.Rows[i].Cells[2].Value = myComputer.getCPU().disassembly;

                //stop stepping if swi
                if (myComputer.getCPU().disassembly.Length >= 3 && myComputer.getCPU().disassembly.Substring(0, 3) == "swi")
                {
                    this.disassemblyView.Rows[i].Cells[0].Value = String.Format("{0:X8}", myComputer.getRegisters().ReadWord(15) - 8);
                    break;
                }
            }
            //step through 9 times. 
                //grab computer.getregister.readword(15)
                //grab computer.getpcu.undecoded instruction
                //grab computer.getcpu.disassembly after each step.

            this.disassemblyView.Rows[4].DefaultCellStyle.BackColor = Color.LightGray;
            //highlight the 5th row 

            myComputer.getRegisters().WriteWord(15, savePC);
            //set register 15 = to TEMPVAR
            myComputer.disassembling = false;
            myComputer.getCPU().disassembling = false;
            //set computer.disassembling to false
        }

        private void ResetDisassembly()
        {
            for (int i = 0; i < 9; i++)
            {
                this.disassemblyView.Rows[i].Cells[0].Value = "";
                this.disassemblyView.Rows[i].Cells[1].Value = "";
                this.disassemblyView.Rows[i].Cells[2].Value = "";
            }
            this.disassemblyView.Rows[4].DefaultCellStyle.BackColor = Color.White;
        }

        private void UpdateStack()
        {
            uint mySP = myComputer.getRegisters().ReadByte(13);
            for (int i = 0; i < 10; i++)
            {
                this.StackGridView.Rows[i].Cells[0].Value = i;
                this.StackGridView.Rows[i].Cells[1].Value = String.Format("{0:X}", myComputer.getMemory().ReadWord(mySP + (uint)i));
            }
        }

        private void ResetStack()
        {
            uint mySP = myComputer.getRegisters().ReadByte(13);
            for (int i = 0; i < 10; i++)
            {
                this.StackGridView.Rows[i].Cells[0].Value = i;
                this.StackGridView.Rows[i].Cells[1].Value = 0;
            }
        }

        private void UpdateMemory()
        {
            int counter = 0;
            for (int i = 0; i < 16; i++)
            {
                this.MemGridView.Rows[i].Cells[0].Value = Convert.ToUInt32(this.MemAddr.Text) + (uint)counter;
                this.MemGridView.Rows[i].Cells[1].Value = String.Format("{0:X}", myComputer.getMemory().ReadWord(Convert.ToUInt32(this.MemAddr.Text) + (uint)(counter + 0)));
                this.MemGridView.Rows[i].Cells[2].Value = String.Format("{0:X}", myComputer.getMemory().ReadWord(Convert.ToUInt32(this.MemAddr.Text) + (uint)(counter + 4)));
                this.MemGridView.Rows[i].Cells[3].Value = String.Format("{0:X}", myComputer.getMemory().ReadWord(Convert.ToUInt32(this.MemAddr.Text) + (uint)(counter + 8)));
                this.MemGridView.Rows[i].Cells[4].Value = String.Format("{0:X}", myComputer.getMemory().ReadWord(Convert.ToUInt32(this.MemAddr.Text) + (uint)(counter + 12)));
                counter += 16;
            }
        }

        private void ResetMemory()
        {
            int counter = 0;
            for (int i = 0; i < 16; i++)
            {
                this.MemGridView.Rows[i].Cells[0].Value = "null";
                this.MemGridView.Rows[i].Cells[1].Value = 0;
                this.MemGridView.Rows[i].Cells[2].Value = 0;
                this.MemGridView.Rows[i].Cells[3].Value = 0;
                this.MemGridView.Rows[i].Cells[4].Value = 0;
                counter += 16;
            }
        }

        private void UpdateRegisters()
        {
            //updates registers
            for (int i = 0; i < 15; i++)
            {
                this.RegisterGridView.Rows[i].Cells[0].Value = i;
                this.RegisterGridView.Rows[i].Cells[1].Value = String.Format("{0:X8}", myComputer.getRegisters().ReadWord((uint)i));
            }
            this.RegisterGridView.Rows[15].Cells[0].Value = 15;
            this.RegisterGridView.Rows[15].Cells[1].Value = String.Format("{0:X8}", myComputer.getRegisters().ReadWord((uint)15) - 8);
        }

        private void ResetRegisters()
        {
            //updates registers
            for (int i = 0; i < 16; i++)
            {
                this.RegisterGridView.Rows[i].Cells[0].Value = i;
                this.RegisterGridView.Rows[i].Cells[1].Value = 0;
            }
        }

        private void UpdateFlags()
        {
            //fix to correspond w/ memory flags then delete this stuff outta CPU
            this.FlagGridView.Rows[0].Cells[1].Value = myComputer.getMemory().TestFlag(0);
            this.FlagGridView.Rows[0].Cells[0].Value = 'N';
            this.FlagGridView.Rows[1].Cells[1].Value = myComputer.getMemory().TestFlag(1);
            this.FlagGridView.Rows[1].Cells[0].Value = 'Z';
            this.FlagGridView.Rows[2].Cells[1].Value = myComputer.getMemory().TestFlag(2);
            this.FlagGridView.Rows[2].Cells[0].Value = 'C';
            this.FlagGridView.Rows[3].Cells[1].Value = myComputer.getMemory().TestFlag(3);
            this.FlagGridView.Rows[3].Cells[0].Value = 'F';
        }

        private void ResetFlags()
        {
            for (int i = 0; i < 4; i++)
            {
                this.FlagGridView.Rows[i].Cells[1].Value = false;
            }
        }

        private void UpdateConsoleBox()
        {
            this.theseOptions.FileStreamClose();
            this.myConsole.Text = File.ReadAllText("Console.txt");
            this.theseOptions.FileStreamOpen();
        }

        private void ResetConsoleBox()
        {
            this.myConsole.Text = "";
            this.theseOptions.FileStreamClose();
            this.theseOptions.FileStreamCreate();
            this.theseOptions.FileStreamOpen();
        }

        private void UpdateTracer()
        {
            //this.myComputer.FileStreamClose();
            this.TraceBox.Text = File.ReadAllText("trace.log");
        }

        private void ResetTracer()
        {
            //this.myComputer.FileStreamClose();
            this.TraceBox.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.checkBox1.Checked)
            {
                myComputer.setTrace(false);
            }
            else
            {
                myComputer.setTrace(true);
            }
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            this.UpdateMemory();
        }
    }
}
