using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnTest.Text = "konstruktor";
            btnTest.MouseLeave += BtnTest_MouseLeave;

        }

        private void BtnTest_MouseLeave(object sender, EventArgs e)
        {
            btnTest.Text = "ok";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            btnTest.Text = "Przycisk";
        }

        private void EnterTest(object sender, EventArgs e)
        {
            btnTest.Text = "enter";
        }
    }
}
