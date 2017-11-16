using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading;

namespace _3Layer.BLL
{
    public delegate void Callback();

    class SoundBLL
    {
        SoundPlayer soundPlayer;
        Thread th = null;

        public SoundBLL() {
           soundPlayer = new SoundPlayer();
        }

        public void SoundRule(Callback onComplete) {
            th = new Thread(new ThreadStart(delegate() {
                soundPlayer.Stream = Properties.Resources.rule;
                soundPlayer.PlaySync();
                onComplete.Invoke();
            }));
            th.Start();
        }

        //Âm thanh khi chọn câu trả lời
        public void SoundChoiceAnswer(char answer, Callback onComplete) {
            switch (answer)
            {
                case 'A':
                    soundPlayer.Stream = Properties.Resources.chooseA;
                    break;
                case 'B':
                    soundPlayer.Stream = Properties.Resources.chooseB;
                    break;
                case 'C':
                    soundPlayer.Stream = Properties.Resources.chooseC;
                    break;
                case 'D':
                    soundPlayer.Stream = Properties.Resources.chooseD;
                    break;
            }
            th = new Thread(new ThreadStart(delegate()
            {
                soundPlayer.PlaySync();
                onComplete.Invoke();
            }));
            th.Start();
        }

        //Âm thanh khi trả lời sai
        public void SoundIncorrect(char correctAnswer) {
            switch (correctAnswer)
            {
                case 'A':
                    soundPlayer.Stream = Properties.Resources.wrongA;
                    break;
                case 'B':
                    soundPlayer.Stream = Properties.Resources.wrongB;
                    break;
                case 'C':
                    soundPlayer.Stream = Properties.Resources.wrongC;
                    break;
                case 'D':
                    soundPlayer.Stream = Properties.Resources.wrongD;
                    break;
            }
            soundPlayer.PlaySync();
        }

        //Âm thanh khi trả lời đúng
        public void SoundCorrect(char currentAnswer) {
            switch (currentAnswer)
            {
                case 'A':
                    soundPlayer.Stream = Properties.Resources.trueA;
                    break;
                case 'B':
                    soundPlayer.Stream = Properties.Resources.trueB;
                    break;
                case 'C':
                    soundPlayer.Stream = Properties.Resources.trueC;
                    break;
                case 'D':
                    soundPlayer.Stream = Properties.Resources.trueD;
                    break;
            }
            soundPlayer.PlaySync();
        }

        //Âm thanh khi chờ đợi kết quả
        public void SoundWaitResult(Callback onComplete)
        {
            soundPlayer.Stream = Properties.Resources.waitanswer;
            th = new Thread(new ThreadStart(delegate()
            {
                soundPlayer.PlaySync();
                onComplete.Invoke();
            }));
            th.Start();
        }

        public void SoundTimeup() {
            soundPlayer.Stream = Properties.Resources.timesup;
            soundPlayer.Play();
        }

        //Âm thanh chúc mừng
        public void SoundCongratulation()
        {
            soundPlayer.Stream = Properties.Resources.congratulation;
            soundPlayer.PlaySync();
        }

        //Âm thanh khi chọn sự trợ giúp 50/50
        public void SoundHelp50() {
            soundPlayer.Stream = Properties.Resources.help50;
            soundPlayer.PlaySync();
        }

        //Âm thanh khi chọn sự trợ giúp gọi điện thoại cho người thân
        public void SoundHelpCall()
        {
            soundPlayer.Stream = Properties.Resources.help_call;
            soundPlayer.Play();
        }

        //Âm thanh luật chơi
        public void SoundRule() {
            soundPlayer.Stream = Properties.Resources.rule;
            soundPlayer.Play();
        }

        //Âm thanh khi chọn sự trợ giúp hỏi ý kiến khán giả
        public void SoundHelpAsk()
        {
            soundPlayer.Stream = Properties.Resources.help_ask;
            soundPlayer.Play();
        }

        //Âm thanh cảm ơn khi kết thúc trò chơi
        public void SoundThankYou() {
            soundPlayer.Stream = Properties.Resources.thank_you;
            soundPlayer.Play();
        }

        //Âm thanh đếm ngược thời gian trả lời câu hỏi
        public void SoundCountDown() {
            soundPlayer.Stream = Properties.Resources.level1;
            soundPlayer.PlayLooping();
        }

        //Âm thanh khi bắt đầu câu hỏi
        public void SoundQuestion(int currentQuestion, Callback onComplete) {
            switch (currentQuestion) {
                case 1: soundPlayer.Stream = Properties.Resources.question1;
                    break;
                case 2: soundPlayer.Stream = Properties.Resources.question2;
                    break;
                case 3: soundPlayer.Stream = Properties.Resources.question3;
                    break;
                case 4: soundPlayer.Stream = Properties.Resources.question4;
                    break;
                case 5: soundPlayer.Stream = Properties.Resources.question5;
                    break;
                case 6: soundPlayer.Stream = Properties.Resources.question6;
                    break;
                case 7: soundPlayer.Stream = Properties.Resources.question7;
                    break;
                case 8: soundPlayer.Stream = Properties.Resources.question8;
                    break;
                case 9: soundPlayer.Stream = Properties.Resources.question9;
                    break;
                case 10: soundPlayer.Stream = Properties.Resources.question10;
                    break;
                case 11: soundPlayer.Stream = Properties.Resources.question11;
                    break;
                case 12: soundPlayer.Stream = Properties.Resources.question12;
                    break;
                case 13: soundPlayer.Stream = Properties.Resources.question13;
                    break;
                case 14: soundPlayer.Stream = Properties.Resources.question14;
                    break;
                case 15: soundPlayer.Stream = Properties.Resources.question15;
                    break;
            }
            th = new Thread(new ThreadStart(delegate()
            {
                soundPlayer.PlaySync();
                onComplete.Invoke();
            }));
            th.Start();
        }

        //Dừng âm thanh
        public void Stop() {
            if (th != null && th.IsAlive)
            {
                th.Abort();
            }
            soundPlayer.Stop();
        }
    }
}
