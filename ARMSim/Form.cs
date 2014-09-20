using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        //dont forget to creeate new thread every time i call run and/or step
    }
}
