using System;
using SFML.Graphics;
using SFML.Window;

namespace Arkanoid
{
    public class Game
    {
        private Players players;
        private GameField gameField;
        public RenderWindow Window { get; set; }

        public Game()
        {
            gameField = new GameField();
        }

        public void Start()
        {
            Window = new RenderWindow(new VideoMode(800, 600), "Arkanoid");
            Window.SetFramerateLimit(60);
            Window.Clear(Color.White);
            Window.Closed += (sender, args) => Window.Close();
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear(Color.White);
                gameField.allObjects.moveObjects();
                gameField.allObjects.CheckCollisions();
                gameField.allObjects.drawObjects(Window);
                Window.Display();
            }
        }

        public void Pause()
        {
            
        }

        public void Save()
        {
            
        }

        public void Continue()
        {
            
        }

        public void Load()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}