using System;
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
        
        
        public static event EventHandler<EventArgs> GamePause;
        public static event EventHandler<EventArgs> GameContinue;
       // public event EventHandler<EventArgs> GameSave;
        public static event EventHandler<EventArgs> GameNew;
        /*public event EventHandler<EventArgs> GameLoad;
        public event EventHandler<EventArgs> GameSettings;
        public event EventHandler<EventArgs> GameExit;*/
        
        

        public Game()
        {
            gameField = new GameField();
            GameContinue += Continue;
            GameNew += NewGame;
            /*GameSave += Save;
            GameLoad += Load;
            GameSettings += ShowSettings;
            GameExit += Exit;*/
            GamePause += Pause;
            gameField.displayObjects.Add(menu);
            settings.AddToList(gameField.displayObjects);
            menu.ShowMenu();
        }

        public void Start()
        {
            window = new RenderWindow(new VideoMode(800, 600), "Arkanoid", Styles.None);
            window.Position = new Vector2i(0, 0);
            settings.size = new Vector2i(1366, 728);
            ChangeWindowSize(1366,728,0,0);
            window.SetFramerateLimit(60);                                                                         GameNew?.Invoke(null,null);   GamePause?.Invoke(null,null);
            Clock clock = new Clock();
            Time timeSinceLastUpdate = Time.Zero;
            Time timePerFrame = Time.FromSeconds(1 / 144f);
            window.SetKeyRepeatEnabled(false);
            window.Clear(Color.White);
            window.Closed += (sender, args) => window.Close();
            while (window.IsOpen)
            {
                Time dt = clock.Restart();
                timeSinceLastUpdate += dt;
                while (timeSinceLastUpdate >= timePerFrame)
                {
                    timeSinceLastUpdate -= timePerFrame;
                    window.DispatchEvents();
                    window.Clear(Color.White);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                    {
                        GamePause?.Invoke(null,null);
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

        public static void NewGame(object sender, EventArgs e)
        {
            gameField.displayObjects.Clear();
            GameContinue?.Invoke(null,null);
            gameField.NewGame();
            player.stat.lives = 3;
            player.stat.score = 0;
            gameField.displayObjects.Add(menu);
            settings.AddToList(gameField.displayObjects);
        }

        public static void Pause(object sender, EventArgs e)
        {
            pause = true;
            menu.ShowMenu();
        }

        public static void Save(object sender, EventArgs e)
        {
            serialize.SerializeToText(gameField.displayObjects,player,settings);                                                                             serialize.SerializeText(gameField.displayObjects, player, settings);
            serialize.SerializeJson(gameField.displayObjects, player, settings);
        }

        public static void Continue(object sender, EventArgs e)
        {
            pause = false;
            menu.HideMenu();
        }
        public static void LoadJson(object sender, EventArgs e)
        {
            gameField.displayObjects.Clear(); 
            serialize.DeserializeJsonFile(gameField.displayObjects, player, settings);      
            gameField.Update();
            GameContinue?.Invoke(null,null);
        }
        public static void Load(object sender, EventArgs e)
        {
            gameField.displayObjects.Clear(); 
            serialize.DeserializeJsonFile(gameField.displayObjects, player, settings);
            //serialize.DeserializeText(gameField.displayObjects, player, settings);
            gameField.Update();
            GameContinue?.Invoke(null,null);
        }

        public static void ShowSettings(object sender, EventArgs e)
        {
            menu.visible = false;
            settings.SettingsVisible(true);
        }

        public static void UpdateDifficulty()
        {
            gameField.displayObjects.Clear();
            GameContinue?.Invoke(null,null);
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

        public static void Exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}