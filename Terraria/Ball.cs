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
    public class Ball : GameEntity 
    {
        
        [JsonProperty] public float radius;
        [JsonProperty] public int speed;
        [JsonProperty] public double direction;

        public Ball(int x, int y, float rad, int sp, double dir, Color col)
        {
            type = "Ball";
            radius = rad;
            x1 = (x-(int)rad);
            x2 = (x+rad);
            y1 = (y-rad);
            y2 = (y+rad);
            shape = new CircleShape(rad);
           // shape.Scale = new Vector2f(1, 1);
           // shape.Origin = new Vector2f(x1 + rad, y1+rad);
            color = col;
            shape.FillColor = col;
            speed = sp;
            direction = dir;
            isMoving = true;
            visible = true;
            width = x2 - x1;
            height = y2 - y1;
            UpdateSize();
        }

        public override void UpdateSize()
        {
            if (Game.Window.Size.X >= 1000)
            {
                float scaleX =(float) Game.Window.Size.X / 800;
                float scaleY = (float)Game.Window.Size.Y / 600;
                //float scale = Math.Min(scaleX, scaleY);
                if(Game.Window.Size.X >=1200)
                    shape = new CircleShape(10);
                shape.FillColor = color;
                shape.Scale = new Vector2f(scaleY, scaleX); 
            }
            else
            {
               // float scaleX =(float) Game.Window.Size.X / 800;
                //float scaleY = (float)Game.Window.Size.Y / 600;
               // float scale = Math.Min(scaleX, scaleY);
                shape = new CircleShape(15);
                shape.FillColor = color;
                shape.Scale = new Vector2f(1, 1);
            }
            
        }

        public override void SerializeToText(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                writer.WriteLine("Ball");
                writer.WriteLine($"{x1} {y1} {x2} {y2} {color.R} {color.G} {color.B} {speed} {direction}");
                writer.Flush();
            }
        }

        public Ball(string[] prop)
        {
            x1 = Int32.Parse(prop[0]);
            y1 = Int32.Parse(prop[1]);
            x2 = Int32.Parse(prop[2]);
            y2 = Int32.Parse(prop[3]);
            shape = new CircleShape((x2 - x1)/2);
            color = new Color(Byte.Parse(prop[4]),Byte.Parse(prop[5]),Byte.Parse(prop[6]));
            shape.FillColor = color;
            speed = Int32.Parse(prop[7]);
            direction = Double.Parse(prop[8]);
            isMoving = true;
            visible = true;
            width = x2 - x1;
            height = y2 - y1;
            type = "Ball";
            UpdateSize();
        }

        public void UpdateSpeed(int sp)
        {
            speed = sp;
        }

        public override void Move()
        {
            if (x1<=0)
            {
                direction = (float)(Math.PI) - direction;
            }else{
                if (x2>=800)
                {
                    direction = (float)(Math.PI) - direction;
                }
                else {
                    if (y1<=60)
                    {
                        direction = - direction;
                    }else if (y1 >= 600)
                    {
                        if (Game.player.stat.lives <= 1)
                        {
                            GameField.Messages.mLostGame.ShowMessage();
                            return;
                        }
                        else
                        {
                            Game.player.stat.lives--;
                            x1 = 350;
                            x2 = 380;
                            y1 = 240;
                            y2 = 270;
                            direction = (float)(Math.PI / 2 * 0.5);
                        }
                    }
                }
            }
            float dx = (float) Math.Cos(direction) * speed;
            float dy = (float) Math.Sin(direction) * speed;

            x1 = (int)(x1 + dx);
            x2 = (int)(x2 + dx);
            y1 = (int)(y1 + dy);
            y2 = (int)(y2 + dy);
            shape.Position = new Vector2f(x1, y1);
        }

        public override void OnCollision(GameEntity obj)
        {
            if(!(obj is Bonus))
            {
                if (y1 >= obj.y2 || y2 <= obj.y1)
                {
                    direction = - direction;
                }
                else
                {
                    direction = (float)(Math.PI) - direction;
                }
                if (obj is Brick brick)
                {
                    Game.player.stat.score++;
                    brick.decreaseStrength();
                } 
            }
            
            
            /*if (leftY >= obj.leftY && rightY <= obj.rightY )
            {
                if (leftX <= obj.leftX)
                {
                    rightX = obj.leftX;
                    leftX = rightX - 2 * (int)radius;
                    refX = rightX - (int)radius;
                }
                else
                {
                    leftX = obj.rightX;
                    rightX = leftX + 2 * (int)radius;
                    refX = rightX - (int)radius;
                }
                setDirection((float)(Math.PI) - getDirection());
            }
            else
            {
                if (leftY <= obj.leftY)
                {
                    rightY = obj.leftY;
                    leftY = rightY - 2 * (int)radius;
                    refY = leftY + (int)radius;
                }
                else
                {
                    leftY = obj.rightY;
                    rightY = leftY + 2 * (int)radius;
                    refY = leftY + (int)radius;
                }
                setDirection(2 * (float)Math.PI-getDirection());
            }*/
        }
    }

    public class Balls
    {
        public List<Ball> balls;
        public Balls()
        { 
            balls = new List<Ball>();
            balls.Add(new Ball(315,315,15,6,(float)(Math.PI/2*0.5),Color.Cyan));
        }
    }
}

