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
            myThread = new Thread(myComputer.Run);
            myThread.Start();
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            myThread = new Thread(myComputer.step);
            myThread.Start();
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
            //NEED TO ADD filter .exe 
            myBox.ShowDialog();
            theseOptions.SetFileName(myBox.FileName);
            myComputer = new Computer(theseOptions);
            myComputer.endRun += new Computer.EventHandler(UpdateAllTheThings);
            this.RunButton.Enabled = true;
            this.StepButton.Enabled = true;
            this.StopButton.Enabled = false;
            this.ResetButton.Enabled = true;
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
            myComputer = new Computer(theseOptions);
            myComputer.endRun += new Computer.EventHandler(UpdateAllTheThings);
            this.RunButton.Enabled = true;
            this.StepButton.Enabled = true;
            this.StopButton.Enabled = false;
            this.ResetButton.Enabled = true;
            this.LoadFileButton.Enabled = true;
        }

        private void UpdateAllTheThings(Computer c, EventArgs e)
        {
            this.richTextBox1.AppendText("updated things ");
            this.RunButton.Enabled = true;
            this.StepButton.Enabled = true;
            this.StopButton.Enabled = false;
            this.ResetButton.Enabled = true;
            this.LoadFileButton.Enabled = true;
            //update everything.
        }
    }
}
