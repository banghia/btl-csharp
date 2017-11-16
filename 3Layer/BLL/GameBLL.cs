using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _3Layer.DTO;

namespace _3Layer.BLL
{
    class GameBLL
    {
        public SoundBLL sound = new SoundBLL();

        private QuestionBLL questionBLL = new QuestionBLL();
        public enum Status { START, PAUSE, STOP , PLAY}
        public enum Answers { A = 'A', B = 'B', C = 'C', D = 'D', EMPTY = '\0' }

        private const int TIMEPLAY = 20;
        private const int TIMEHELP = 30;

        public Player CurrentPlayer { get; set;}
        private Question[] Questions;
        public Question CurrentQuestion { get; set; }
        public int CurrentTimePlay { get; set; }
        public int CurrentLevel { get; set; }
        public Answers CurrentAnswer { get; set; }
        public Status StatusGame { get; set; }

        public event EventHandler OnPlay;
        public event EventHandler OnNextQuestion;
        public event EventHandler OnTimeUp;
        public event EventHandler OnEndGame;
        public event EventHandler OnPause;
        public event EventHandler OnResume;
        public event EventHandler OnShowResult;

        public GameBLL(Player currentPlayer) {
            this.CurrentPlayer = currentPlayer;
            Questions = questionBLL.getAll().ToArray();
            CurrentLevel = 0;
            StatusGame = Status.START;
        }

        public void NextQuestion() {
            if (CurrentLevel >= 15) {
                Stop();
            }else
            if (StatusGame == Status.PLAY) {
                CurrentQuestion = Questions[CurrentLevel];
                CurrentLevel++;
                CurrentTimePlay = TIMEPLAY;
                CurrentAnswer = Answers.EMPTY;
                if (OnNextQuestion != null)
                {
                    Pause();
                    sound.SoundQuestion(CurrentLevel, delegate() {
                        Resume();
                    });
                    OnNextQuestion(this, EventArgs.Empty);
                }
            }
        }

        public bool ChoiceAnswer(char answer) {
            if (StatusGame == Status.PLAY && CurrentAnswer == Answers.EMPTY) {
                switch (answer) {
                    case 'A': CurrentAnswer = Answers.A;
                        break;
                    case 'B': CurrentAnswer = Answers.B;
                        break;
                    case 'C': CurrentAnswer = Answers.C;
                        break;
                    case 'D': CurrentAnswer = Answers.D;
                        break;
                }
                Pause();
                sound.SoundChoiceAnswer(answer, delegate() {
                    ShowResult();
                });
                return true;
            }
            return false;
        }

        public void ShowResult() {
            sound.SoundWaitResult(delegate()
            {
                if (CurrentQuestion.Correct == (char)CurrentAnswer)
                {
                    if (OnShowResult != null) {
                        OnShowResult(this, EventArgs.Empty);
                    }
                    sound.SoundCorrect(CurrentQuestion.Correct);
                    Resume();
                    NextQuestion();
                }
                else {
                    if (OnShowResult != null)
                    {
                        OnShowResult(this, EventArgs.Empty);
                    }
                    sound.SoundIncorrect(CurrentQuestion.Correct);
                    Stop();
                }
            });
        }

        public void CountDownTimePlay() {
            if (StatusGame == Status.PLAY && CurrentTimePlay > 0) {
                CurrentTimePlay--;
            }
            else if (CurrentTimePlay == 0) {
                sound.SoundTimeup();
                if (OnTimeUp != null) {
                    OnTimeUp(this, EventArgs.Empty);
                }
                Stop();
            }
        }

        public void Play()
        {
            if(StatusGame == Status.START){
                StatusGame = Status.PLAY;
                NextQuestion();
                if (OnPlay != null)
                {
                    OnPlay(this, EventArgs.Empty);
                }
            }
        }

        public bool IsPlay() {
            return StatusGame == Status.PLAY;
        }

        public void Pause() {
            if (StatusGame == Status.PLAY) {
                StatusGame = Status.PAUSE;
                if(OnPause != null){
                    OnPause(this, EventArgs.Empty);
                }
            }
        }

        public bool IsPause()
        {
            return StatusGame == Status.PAUSE;
        }

        public void Resume() {
            if (StatusGame == Status.PAUSE) {
                StatusGame = Status.PLAY;
                if (OnResume != null) {
                    OnResume(this, EventArgs.Empty);
                }
            }        
        }

        public void Stop() {
            StatusGame = Status.STOP;
            if (OnEndGame != null) {
                OnEndGame(this, EventArgs.Empty);
            }
        }

        public bool IsStop()
        {
            return StatusGame == Status.STOP;
        }

        public bool IsStart()
        {
            return StatusGame == Status.START;
        }

    }
}
