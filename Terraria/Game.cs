using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Xml.Serialization;
using GameEngine;
using SFML.System;

namespace Arkanoid
{
    public class Game
    {
        public static Player player = new Player();
        public static Settings settings = new Settings(1, 5, 6, 800,600);
        public static GameField gameField;
        public static Menu menu=new Menu();
        public static Proxy serialize = new Proxy();
        public static RenderWindow Window { get; set; }
        public static bool pause = false;

        public Game()
        {
            gameField = new GameField();
            gameField.displayObjects.Add(menu);
            gameField.AddObjects(settings.listBtn.ToArray());
            menu.ShowMenu();
        }

        public void Start()
        {
            Window = new RenderWindow(new VideoMode(800, 600), "Arkanoid", Styles.None);
            Window.Position = new Vector2i(0, 0);
            Window.SetFramerateLimit(60);
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
            gameField.AddObjects(settings.listBtn.ToArray());
        }

        public static void Pause()
        {
            pause = true;
            menu.ShowMenu();
        }

        public static void Save()
        {
            serialize.SerializeText(gameField.displayObjects, player, settings);
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
            serialize.DeserializeText(gameField.displayObjects, player, settings);
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
            gameField.AddObjects(settings.listBtn.ToArray());
            gameField.UpdateDifficulty(settings.difficulty);
            pause = false;
        }

        public static void ChangeWindowSize(uint x, uint y, int px, int py)
        {
            VideoMode desktopMode = VideoMode.DesktopMode;
            //View view = new View(new FloatRect(0, 0, 800, 600));
            Window.Size = new Vector2u(x, y);
            Vector2i windowPosition = new Vector2i((int)
                (desktopMode.Width - (int)x) / 2,(int)
                (desktopMode.Height - (int)y) / 2
            );
            pause = false;
            Window.Position = new Vector2i(px,py);
            float scaleX =(float) Window.Size.X / 800;
            float scaleY = (float)Window.Size.Y / 600;
            float scale = Math.Min(scaleX, scaleY);
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