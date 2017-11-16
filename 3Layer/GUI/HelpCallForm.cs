using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _3Layer.BLL;
using _3Layer.DTO;

namespace _3Layer.GUI
{
    public partial class HelpCallForm : Form
    {
        SoundBLL soundBLL = new SoundBLL();
        Question question;

        public HelpCallForm(Question question)
        {
            InitializeComponent();
            this.question = question;
        }

        private void HelpCallForm_Load(object sender, EventArgs e)
        {
            soundBLL.SoundHelpCall();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            DialogResult ds = MessageBox.Show("Đáp án " + question.Correct, "Trợ giúp của người thân", MessageBoxButtons.OK);
            if(ds == DialogResult.OK){
                Close();
            }
        }
    }
}
