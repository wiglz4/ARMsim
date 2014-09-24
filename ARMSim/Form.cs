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


//YOU LEFT OFF HERE
//TRACE, UNIT TESTS, PROGRAM REPORT
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
            //these two lines used to be in run
            myComputer = new Computer(theseOptions);
            myComputer.endRun += new Computer.EventHandler(UpdateAllTheThings);
            //setup views
            for (int i = 0; i < 15; i++)
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
            //starts running on file that opened program
            this.RunButton_Click(this.RunButton, EventArgs.Empty);
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
            myThread.Start();
            this.LoadFileButton.Enabled = false;
            this.StepButton.Enabled = false;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            myThread.Abort();
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
            this.ResetButton_Click(this.ResetButton, EventArgs.Empty);
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
            this.myComputer.FileStreamClose();
            myComputer = new Computer(theseOptions);
            myComputer.endRun += new Computer.EventHandler(UpdateAllTheThings);
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            this.UpdateMemory();
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
            this.DisBox.Text = @".top
	            ; add num1 to num2
	            mov	di,num1+digits-1
	            mov	si,num2+digits-1
	            mov	cx,digits	; 
	            call	AddNumbers	; num2 += num1
	            mov	bp,num2		;
	            call	PrintLine	;
	            dec	dword [term]	; decrement loop counter
	            jz	.done		;";
        }

        private void ResetDisassembly()
        {
            this.DisBox.Text = "";
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
                this.MemGridView.Rows[i].Cells[1].Value = String.Format("{0:X}", myComputer.getMemory().ReadWord(Convert.ToUInt32(this.MemAddr.Text) + (uint)(counter+0)));
                this.MemGridView.Rows[i].Cells[2].Value = String.Format("{0:X}", myComputer.getMemory().ReadWord(Convert.ToUInt32(this.MemAddr.Text) + (uint)(counter+4)));
                this.MemGridView.Rows[i].Cells[3].Value = String.Format("{0:X}", myComputer.getMemory().ReadWord(Convert.ToUInt32(this.MemAddr.Text) + (uint)(counter+8)));
                this.MemGridView.Rows[i].Cells[4].Value = String.Format("{0:X}", myComputer.getMemory().ReadWord(Convert.ToUInt32(this.MemAddr.Text) + (uint)(counter+12)));
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
                this.RegisterGridView.Rows[i].Cells[1].Value = String.Format("{0:X}", myComputer.getRegisters().ReadWord((uint)i));
            }
        }

        private void ResetRegisters()
        {
            //updates registers
            for (int i = 0; i < 15; i++)
            {
                this.RegisterGridView.Rows[i].Cells[0].Value = i;
                this.RegisterGridView.Rows[i].Cells[1].Value = 0;
            }
        }

        private void UpdateFlags()
        {
            this.FlagGridView.Rows[0].Cells[1].Value = myComputer.getCPU().getFlagN();
            this.FlagGridView.Rows[0].Cells[0].Value = 'N';
            this.FlagGridView.Rows[1].Cells[1].Value = myComputer.getCPU().getFlagZ();
            this.FlagGridView.Rows[1].Cells[0].Value = 'Z';
            this.FlagGridView.Rows[2].Cells[1].Value = myComputer.getCPU().getFlagC();
            this.FlagGridView.Rows[2].Cells[0].Value = 'C';
            this.FlagGridView.Rows[3].Cells[1].Value = myComputer.getCPU().getFlagF();
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
            this.myComputer.FileStreamClose();
            this.TraceBox.Text = File.ReadAllText("trace.log");
            this.myComputer.FileStreamOpen();
        }
    }
}
