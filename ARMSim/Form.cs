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
//also need to create trace log file
//need to actually spit stuff on screen
namespace ARMSim
{

    public partial class ARMSimForm : Form
    {
        Options theseOptions;
        Computer myComputer;
        Thread myThread;
        
        public ARMSimForm(Options myOptions)
        {
            theseOptions = myOptions;
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
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
            //starts running on file that opened program
            this.RunButton_Click(this.RunButton, EventArgs.Empty);
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            this.richTextBox1.AppendText("test");
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

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

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
            myComputer = new Computer(theseOptions);
            myComputer.endRun += new Computer.EventHandler(UpdateAllTheThings);
        }

        private void UpdateAllTheThings(Computer c, EventArgs e)
        {
            this.richTextBox1.AppendText("updated things ");
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

            //spit random junk in the text box

            //update stack panel
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
            this.ConsoleBox.Text = File.ReadAllText("Console.txt");
            this.theseOptions.FileStreamOpen();
        }

        private void ResetConsoleBox()
        {
            this.ConsoleBox.Text = "";
            this.theseOptions.FileStreamClose();
            this.theseOptions.FileStreamCreate();
            this.theseOptions.FileStreamOpen();
        }

        private void Console_Click(object sender, EventArgs e)
        {

        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            this.UpdateMemory();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
