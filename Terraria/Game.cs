using System;
using System.Timers;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace GameEngine
{
    public class Game
    {
        public static Player player = new Player();
        public static Settings settings = new Settings(1, 5, 6, 800,600);
        public static GameField gameField;
        public static Menu menu=new Menu();
        public static Proxy serialize = new Proxy();
        public static RenderWindow window;
        public static bool pause;
        public static Random rndm = new Random();
        public static Timer tmr = new Timer();

        public static Events ev = new Events();

        public Game()
        {
            gameField = new GameField();
            ev.StartHandler += (Start);
            ev.PauseHandler += (Pause);
            ev.ContinueHandler += (Continue);
            ev.NewGameHandler += (NewGame);
            ev.ChangeSizeHandler += (ChangeWindowSize);
            ev.GameOverHandler += (GameOver);
            ev.TimerHandler += (GameEvent);
            gameField.displayObjects.Add(menu);
            settings.AddToList(gameField.displayObjects);
            menu.ShowMenu();
            tmr.Interval = 10;
            tmr.Elapsed += OnTimerEvent;
            tmr.AutoReset = true;
        }
        
        public void Start()
        {
            window = new RenderWindow(new VideoMode(800, 600), "Arkanoid", Styles.None);
            window.Position = new Vector2i(0, 0);
            settings.size = new Vector2i(1366, 728);
            ChangeWindowSize(1366,728,0,0);
            window.SetFramerateLimit(60);                                                                         NewGame();   Pause();
            window.SetKeyRepeatEnabled(false);
            window.Clear(Color.White);
            window.Closed += (sender, args) => window.Close();
            tmr.Start();
        }

        /*public void Start()
        {
            window = new RenderWindow(new VideoMode(800, 600), "Arkanoid", Styles.None);
            window.Position = new Vector2i(0, 0);
            settings.size = new Vector2i(1366, 728);
            ChangeWindowSize(1366, 728, 0, 0);
            window.SetFramerateLimit(60);
            NewGame();
            Pause();
            window.SetKeyRepeatEnabled(false);
            window.Clear(Color.White);
            window.Closed += (sender, args) => window.Close();
            tmr.Start();
            /*Clock cl = new Clock();
            Time elapsed;
            while (window.IsOpen)
            {
                window.DispatchEvents();
                elapsed = cl.Restart();
                if (elapsed >= Time.FromMilliseconds(16))
                {
                    ev.OnTimer();
                }
            }#1#
        }*/
        
        /*static void Sleep(int milliseconds)
        {
            Clock sleepClock = new Clock();
            while (sleepClock.ElapsedTime.AsMilliseconds() < milliseconds);
        }*/
        
        public static void OnTimerEvent(object sender, ElapsedEventArgs e)
        {
            ev.OnTimer();
        }


        public void GameEvent()
        {
            window.DispatchEvents();
            window.Clear(Color.White);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                    ev.OnGamePause();
            }
            if (!pause)
            {
                    gameField.MoveObjects();
                    gameField.CheckCollisions();
            }
            gameField.DrawObjects(window);
            window.Display();
            
        }
        public static void NewGame()
        {
            gameField.displayObjects.Clear();
            Continue();
            gameField.NewGame();
            player.stat.lives = 3;
            player.stat.score = 0;
            gameField.displayObjects.Add(menu);
            settings.AddToList(gameField.displayObjects);
        }

        public static void Pause()
        {
            pause = true;
            menu.ShowMenu();
        }

        public static void Save(object sender, EventArgs e)
        {
            serialize.SerializeToText(gameField.displayObjects,player,settings);                                                                             serialize.SerializeText(gameField.displayObjects, player, settings);
            serialize.SerializeJson(gameField.displayObjects, player, settings);
        }

        public static void Continue()
        {
            pause = false;
            menu.HideMenu();
        }
        public static void LoadJson(object sender, EventArgs e)
        {
            gameField.displayObjects.Clear(); 
            serialize.DeserializeJsonFile(gameField.displayObjects, player, settings);      
            gameField.Update();
            Continue();
        }
        public static void Load(object sender, EventArgs e)
        {
            gameField.displayObjects.Clear(); 
            serialize.DeserializeJsonFile(gameField.displayObjects, player, settings);
            gameField.Update();
            Continue();
        }

        public static void ShowSettings(object sender, EventArgs e)
        {
            menu.visible = false;
            settings.SettingsVisible(true);
        }

        public static void UpdateDifficulty()
        {
            gameField.displayObjects.Clear();
            Continue();
            gameField.NewGame();
            player.stat.lives = 3;
            player.stat.score = 0;
            gameField.displayObjects.Add(menu);
            settings.AddToList(gameField.displayObjects);
            gameField.UpdateDifficulty(settings.difficulty);
            pause = false;
        }

        public static void ChangeWindowSize(uint x, uint y, int px, int py)
        {
            
            window.Size = new Vector2u(x, y);
            pause = false;
            window.Position = new Vector2i(px,py);
            foreach (var o in gameField.displayObjects)
            {
                o.UpdateSize();
            }
        }

        public bool GameOver()
        {
            GameField.Messages.mLostGame.ShowMessage();
            return true;
        }

        public static void Exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        public void StartGame()
        {
            window = new RenderWindow(new VideoMode(800, 600), "Arkanoid", Styles.None);
            window.Position = new Vector2i(0, 0);
            settings.size = new Vector2i(1366, 728);
            ChangeWindowSize(1366,728,0,0);
            window.SetFramerateLimit(60);        
            window.SetKeyRepeatEnabled(false);
            window.Clear(Color.White);
            NewGame();
            Pause();
            window.Closed += (sender, args) => window.Close();
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.White);
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    ev.OnGamePause();
                }
                if (!pause)
                {
                    gameField.MoveObjects();
                    gameField.CheckCollisions();
                }
                gameField.DrawObjects(window);
                window.Display();
            }
        }
    }
}