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
    public partial class HelpAskForm : Form
    {

        SoundBLL sound = new SoundBLL();
        Question question;

        public HelpAskForm(Question question)
        {
            InitializeComponent();
            this.question = question;
        }

        private void HelpAskForm_Load(object sender, EventArgs e)
        {
            sound.SoundHelpAsk();
        }
    }
}
