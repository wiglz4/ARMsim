using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//YOU LEFT OFF HERE
//need to link up all the buttons and forms with actual stuffs. only need it to update beginning and end of running. not during
//when rest button is clicked need to delete then recreate computer
//also need to create trace log file
namespace ARMSim
{
    public partial class ARMSimForm : Form
    {
        public ARMSimForm(Options myOptions)
        {
            InitializeComponent();
            Computer myComputer = new Computer(myOptions);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void StopButton_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LoadFileButton_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //dont forget to creeate new thread every time i call run and/or step
    }
}
