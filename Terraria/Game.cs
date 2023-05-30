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
        public static RenderWindow Window;
        public static bool pause;
        public static Random rndm = new Random();

        public Game()
        {
            gameField = new GameField();
            gameField.displayObjects.Add(menu);
            settings.AddToList(gameField.displayObjects);
            menu.ShowMenu();
        }

        public void Start()
        {
            Window = new RenderWindow(new VideoMode(800, 600), "Arkanoid", Styles.None);
            Window.Position = new Vector2i(0, 0);
            settings.size = new Vector2i(1366, 728);
            ChangeWindowSize(1366,728,0,0);
            Window.SetFramerateLimit(60);
            NewGame();
            Pause();
            Window.SetKeyRepeatEnabled(false);
            Window.Clear(Color.White);
            Window.Closed += (sender, args) => Window.Close();
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear(Color.White);
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    Pause();
                }
                if (!pause)
                {
                    gameField.MoveObjects();
                    gameField.CheckCollisions();
                }
                gameField.DrawObjects(Window);
                Window.Display();
            }
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

        public static void Save()
        {
            serialize.SerializeToText(gameField.displayObjects,player,settings);                                                                             serialize.SerializeText(gameField.displayObjects, player, settings);
            serialize.SerializeJson(gameField.displayObjects, player, settings);
        }

        public static void Continue()
        {
            pause = false;
            menu.HideMenu();
        }
        public static void LoadJson()
        {
            gameField.displayObjects.Clear(); 
            serialize.DeserializeJsonFile(gameField.displayObjects, player, settings);      
            gameField.Update();
            Continue();
        }
        public static void Load()
        {
            gameField.displayObjects.Clear(); 
            serialize.DeserializeJsonFile(gameField.displayObjects, player, settings);
            //serialize.DeserializeText(gameField.displayObjects, player, settings);
            gameField.Update();
            Continue();
        }

        public static void ShowSettings()
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
            Window.Size = new Vector2u(x, y);
            pause = false;
            Window.Position = new Vector2i(px,py);
            foreach (var o in gameField.displayObjects)
            {
                o.UpdateSize();
            }
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}