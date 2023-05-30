using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;


namespace GameEngine
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class Brick: GameEntity
    {
        [JsonProperty]public int strength;
        public Bonus bonus;

        public Brick(int x1, int y1,  int x2, int y2, int str, Color col, bool v)
        {
            type = "Brick";
            width = x2 - x1;
            height = y2 - y1;
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
            strength = str;
            isMoving = false;
            visible = v;
            //bonus = new Bonus(x1,y1,"1");
        }
        public override void SerializeToText(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                writer.WriteLine("Brick");
                writer.WriteLine($"{x1} {y1} {x2} {y2} {color.R} {color.G} {color.B} {strength}");
                writer.Flush();
            }
        }

        public Brick(string[] prop)
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
            strength = Int32.Parse(prop[7]);
            isMoving = false;
            visible = true;
            type = "Brick";
            width = x2 - x1;
            height = y2 - y1;
            //bonus = new Bonus(x1,y1, "1");
        }
        public override void Move()
        {
        }

        public override void draw(RenderWindow window)
        {
            window.Draw(shape);
        }
        

        public void decreaseStrength()
        {
            if (strength != 15)
            {
                strength -= 1;
                if (strength == 1)
                {
                    color = Color.Blue;
                    shape.FillColor = color;
                }
                else if(strength == 0)
                {
                    visible = false;
                    bonus.visible = true;
                    Game.gameField.CheckGameState();
                }
            }
        }
    }

    public class Bricks
    {
        public List<Brick> bricks = new List<Brick>();


        public Bricks()
        {
            bricks = new List<Brick>();
            for (int i = 1; i < 10; i++)
            {
                bricks.Add(new Brick(i*70+5, 100,i*70+70, 120, 1, Color.Blue, true));
                bricks.Add(new Brick(i*70+5, 120,i*70+70, 140, 2, Color.Red, true));
                bricks.Add(new Brick(i*70+5, 140,i*70+70, 160, 1, Color.Blue, true));
                bricks.Add(new Brick(i*70+5, 160,i*70+70, 180, 2, Color.Red, true));
            }
        }
        
        public Bricks(int n)
        {
            
        }
    }
}