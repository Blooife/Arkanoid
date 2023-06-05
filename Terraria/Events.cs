using System;
using System.Collections.Generic;

namespace GameEngine
{
    public delegate bool EventGameOverHandler();
    public delegate void EventOnStart();
    public delegate void EventOnPause();
    public delegate void EventOnContinue();
    public delegate void EventOnNewGame();
    public delegate void EventOnExit();
    public delegate void EventTimer();
    public delegate void EventOnChangeSize(uint x, uint y, int px, int py);
    public class Events
    {
        
        public event EventOnStart StartHandler;
        public event EventOnPause PauseHandler;
        public event EventOnNewGame NewGameHandler;
        public event EventOnContinue ContinueHandler;
        public event EventOnChangeSize ChangeSizeHandler;
        public event EventOnExit ExitHandler;
        public event EventHandler<CollisionEventArgs> OnCollision;
        public event EventGameOverHandler GameOverHandler;
        public event EventTimer TimerHandler;
        
        public void OnGamePause()
        {
            try
            {
                PauseHandler();
            }
            catch(NullReferenceException ex)
            {
                
            }
            
        }

        public void OnTimer()
        {
            TimerHandler();
        }
        
        public void OnGameContinue()
        {
            ContinueHandler();
        }
        
        public void OnExit()
        {
            ExitHandler();
        }
        
        public void OnChangeSize(uint x, uint y, int px, int py)
        {
            ChangeSizeHandler(x, y, px, py);
        }
        public void OnNewGame()
        {
            NewGameHandler();
        }

        public void OnGameOver()
        {
            GameOverHandler();
        }
        
        public void OnGameStart()
        {
            StartHandler();
        }
    }
}