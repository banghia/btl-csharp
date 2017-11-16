using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _3Layer.GUI;
using System.Threading;

namespace _3Layer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(delegate() { 
                GameForm gameForm = new GameForm();
                Application.Run(gameForm);
            }));
            th.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuestionsManagerForm qs = new QuestionsManagerForm();
            qs.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
