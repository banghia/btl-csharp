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
using System.Threading;
using System.Media;

namespace _3Layer.GUI
{
    public partial class GameForm : Form
    {
        GameBLL game;
        SoundBLL sound;

        private Color COLOR_ANSWER = Color.FromArgb(4, 99, 128);
        private Color COLOR_ANSWER_SELECTED = Color.FromArgb(255, 152, 0);
        private Color COLOR_ANSWER_CORRECT = Color.FromArgb(133, 219, 24);

        public GameForm()
        {
            InitializeComponent();
            Player currentPlayer = new Player("Nghia");
            game = new GameBLL(currentPlayer);
            sound = new SoundBLL();
        }

        //Load câu hỏi đầu tiên
        private void GameForm_Load(object sender, EventArgs e)
        {
            game.OnNextQuestion += new EventHandler(OnNextQuestion);
            game.OnShowResult += new EventHandler(OnShowResult);
            game.OnEndGame += new EventHandler(OnEndGame);
        }

        private void OnEndGame(object sender, EventArgs e) {
            Thread.Sleep(500);
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        pnlSidebar.Visible = false;
                        pnlQuestion.Visible = false;
                        pnlResult.Visible = true;
                        sound.SoundThankYou();
                    }));
                }
                catch (Exception)
                {
                }
            }
            else {
                pnlSidebar.Visible = false;
                pnlQuestion.Visible = false;
                pnlResult.Visible = true;
                sound.SoundThankYou();
            }
        }

        private void OnShowResult(object sender, EventArgs e) {
            if (this.InvokeRequired) {
                try
                {
                    this.Invoke(new MethodInvoker(delegate() {
                        switch (game.CurrentQuestion.Correct)
                        {
                            case 'A': btnA.BackColor = COLOR_ANSWER_CORRECT;
                                break;
                            case 'B': btnB.BackColor = COLOR_ANSWER_CORRECT;
                                break;
                            case 'C': btnC.BackColor = COLOR_ANSWER_CORRECT;
                                break;
                            case 'D': btnD.BackColor = COLOR_ANSWER_CORRECT;
                                break;
                        }
                    }));
                }
                catch (Exception)
                {
                }
            }
        }

        private void OnNextQuestion(object sender, EventArgs e)
        {
            Question question = game.CurrentQuestion;
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        lblContent.Text = question.Content;
                        btnA.Text = "A. " + question.A;
                        btnB.Text = "B. " + question.B;
                        btnC.Text = "C. " + question.C;
                        btnD.Text = "D. " + question.D;
                        btnA.BackColor = COLOR_ANSWER;
                        btnB.BackColor = COLOR_ANSWER;
                        btnC.BackColor = COLOR_ANSWER;
                        btnD.BackColor = COLOR_ANSWER;
                        GetLabelQuestion(game.CurrentLevel).BackColor = Color.Yellow;
                    }));
                }
                catch (Exception)
                {
                }
            }
            else {
                lblContent.Text = question.Content;
                btnA.Text = "A. " + question.A;
                btnB.Text = "B. " + question.B;
                btnC.Text = "C. " + question.C;
                btnD.Text = "D. " + question.D;
                btnA.BackColor = COLOR_ANSWER;
                btnB.BackColor = COLOR_ANSWER;
                btnC.BackColor = COLOR_ANSWER;
                btnD.BackColor = COLOR_ANSWER;
                GetLabelQuestion(game.CurrentLevel).BackColor = Color.Yellow;
            }
        }


        //Lây ra label câu hỏi theo index
        private Label GetLabelQuestion(int index) {
            return (Label)pnlSidebar.Controls["lblQuestion"+index];
        }

        private void btn_ChoiceAnswer(object sender, EventArgs e) {
            Button btn = (Button)sender;
            if (btn.Text != "") {
                char answer = btn.Text[0];
                if (game.ChoiceAnswer(answer))
                {
                    btn.BackColor = COLOR_ANSWER_SELECTED;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStart.Visible = false;
            sound.SoundRule(delegate() {
                game.Play();
            });
        }

        private void tmrTimePlay_Tick(object sender, EventArgs e)
        {
            if (game.IsPlay())
            {
                game.CountDownTimePlay();
                lblCountDown.Text = game.CurrentTimePlay + "";
                lblCountDown.Visible = true;
            }
            else {
                lblCountDown.Visible = false;
            }
        }

        private void btn50_Click(object sender, EventArgs e)
        {
            if (game.IsPlay()) {
                btn50.Enabled = false;
                sound.SoundHelp50();
                switch (game.CurrentQuestion.Correct) {
                    case 'A': btnB.Text = "";
                        btnC.Text = "";
                        break;
                    case 'B': btnA.Text = "";
                        btnC.Text = "";
                        break;
                    case 'C': btnB.Text = "";
                        btnD.Text = "";
                        break;
                    case 'D': btnA.Text = "";
                        btnB.Text = "";
                        break;
                }
            }
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            if (game.IsPlay()) {
                btnCall.Enabled = false;
                game.CurrentTimePlay = 30;
                HelpCallForm help = new HelpCallForm(game.CurrentQuestion);
                help.Show();
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (game.IsPlay()) {
                btnHelp.Enabled = false;
                game.CurrentTimePlay = 30;
                HelpAskForm help = new HelpAskForm(game.CurrentQuestion);
                help.Show();
            }
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStart.Visible = false;
            pnlSidebar.Visible = true;
            sound.SoundRule(delegate()
            {
                try
                {
                    pnlQuestion.Invoke(new MethodInvoker(delegate() {
                        pnlQuestion.Visible = true;
                    }));
                }
                catch (Exception)
                {
                }
                game.Play();
            });
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn thoát!", "Xác nhận", MessageBoxButtons.OKCancel);
            if (rs != DialogResult.OK)
            {
                e.Cancel = true;
            }
            else {
                game.sound.Stop();
                sound.Stop();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (game.IsPlay()) {
                btnStop.Enabled = false;
                game.Stop();
            }
        }

        
    }
}
