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
//need to link up all the buttons and forms with actual stuffs. only need it to update beginning and end of running. not during
//when rest button is clicked need to delete then recreate computer
//also need to create trace log file
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
            myComputer = new Computer(theseOptions);
            //starts running on file that opened program
            this.RunButton_Click(this, new System.EventArgs());
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.RunButton.Enabled = false;
                this.StepButton.Enabled = false;
                this.StopButton.Enabled = true;
                this.ResetButton.Enabled = false;
                myThread = new Thread(myComputer.Run);
                myThread.Start();
            }
            catch
            {
                this.RunButton.Enabled = false;
                this.StepButton.Enabled = false;
                this.StopButton.Enabled = false;
                this.ResetButton.Enabled = false;
            }
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            myComputer.step();
            //update values
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            myComputer.setStop(true);
            this.StepButton.Enabled = true;
            this.ResetButton.Enabled = true;
            //update values
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
            myBox.ShowDialog();
            theseOptions.SetFileName(myBox.FileName);
            myComputer = new Computer(theseOptions);
            this.RunButton.Enabled = true;
            this.StepButton.Enabled = true;
            this.StopButton.Enabled = false;
            this.ResetButton.Enabled = true;
            //update text box
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
            this.RunButton.Enabled = true;
            this.StepButton.Enabled = true;
            this.StopButton.Enabled = false;
            this.ResetButton.Enabled = true;
        }

        //dont forget to creeate new thread every time i call Run and/or step
    }
}
