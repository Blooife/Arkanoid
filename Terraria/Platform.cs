using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameEngine
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class Platform: GameEntity
    {
        
        [JsonProperty]public int speed;
        public Platform(int x1, int y1,  int x2, int y2, Color col, int sp)
        {
            type = "Platform";
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            shape = new RectangleShape(new Vector2f(x2-x1,y2-y1));
            shape.Position = new Vector2f(x1,y1);
            color = col;
            shape.FillColor = col;
            shape.OutlineColor= Color.Black;
            shape.OutlineThickness = 1;
            speed = sp;
            isMoving = true;
            visible = true;
            width = x2 - x1;
            height = y2 - y1;
        }

        public void ChangeWidth(int w)
        {
            if (w > 0)
            {
                if (width < 140)
                {
                    width += 20;
                    x2 += 20;  
                }
            }
            else
            {
                if (width > 100)
                {
                    width -= 20;
                    x2 -= 20;
                }
            }
            shape = new RectangleShape(new Vector2f(x2-x1,y2-y1));
            shape.Position = new Vector2f(x1,y1);
            shape.FillColor = color;
            shape.OutlineColor= Color.Black;
            shape.OutlineThickness = 1;
        }

        public void UpdateSpeed(int sp)
        {
            speed = sp;
        }
        
        public override void SerializeToText(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename,true))
            {
                writer.WriteLine("Platform");
                writer.WriteLine($"{x1} {y1} {x2} {y2} {color.R} {color.G} {color.B} {speed}");
                writer.Flush();
            }
        }

        public Platform(string[] prop)
        {
            x1 = Int32.Parse(prop[0]);
            y1 = Int32.Parse(prop[1]);
            x2 = Int32.Parse(prop[2]);
            y2 = Int32.Parse(prop[3]);
            shape = new RectangleShape(new Vector2f(x2-x1,y2-y1));
            color = new Color(Byte.Parse(prop[4]),Byte.Parse(prop[5]),Byte.Parse(prop[6]));
            shape.FillColor = color;
            shape.OutlineColor= Color.Black;
            shape.OutlineThickness = 1;
            speed = Int32.Parse(prop[7]);
            isMoving = true;
            visible = true;
            type = "Platform";
            width = x2 - x1;
            height = y2 - y1;
        }

        public override void Move()
        { 
            bool moveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            bool moveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
            if (moveLeft || moveRight)
            {
                if (moveLeft)
                {
                    x1 = (x1 - speed);
                    x2 = (x2 - speed);
                }
                if (moveRight)
                {
                    x1 = (x1 + speed);
                    x2 = (x2 + speed);
                }
                
                if (x1 < 0)
                {
                    x2 = (x2 - x1);
                    x1 = 0;
                }
                else if (x2 > 800)
                {
                    x1 = 800 - (x2 - x1);
                    x2 = 800;
                }
            }
        }
    }

    public class Platforms
    {
        public List<Platform> platforms = new List<Platform>();

        public Platforms()
        {
            platforms.Add(new Platform(350, 570, 470, 590, Color.Green, 6));
        }
        
        public Platforms(int n)
        {
            
        }
        
    }
}