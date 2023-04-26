using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

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
            //Serialize(gameField.allObjects.displayObjects);
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
        
        /*public void Serialize(List<GameEntity> displayObjects)
        {
            System.Type[] typeArr = new[] { typeof(Ball), typeof(Platform), typeof(Brick) };
            XmlSerializer xs = new XmlSerializer(typeArr);
            using (FileStream fs = new FileStream("serialize.xml", FileMode.Open))
            {
                xs.Serialize(fs, displayObjects);
            }
        }*/
    }
}