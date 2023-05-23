using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;

namespace Arkanoid
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class Ball : GameEntity 
    {
        
        [JsonProperty] public int radius;
        [JsonProperty] public int speed;
        [JsonProperty] public double direction;

        public Ball(int x, int y, int rad, int sp, double dir, Color col)
        {
            type = "Ball";
            radius = rad;
            x1 = (x-rad);
            x2 = (x+rad);
            y1 = (y-rad);
            y2 = (y+rad);
            shape = new CircleShape(rad);
            color = col;
            shape.FillColor = col;
            speed = sp;
            direction = dir;
            isMoving = true;
            visible = true;
            width = x2 - x1;
            height = y2 - y1;
            x = x1 + width / 2;
            y = y1 + height / 2;
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
            x = x1 + width / 2;
            y = y1 + height / 2;
            type = "Ball";
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
                            GameField.messages.mLostGame.ShowMessage();
                            return;
                        }
                        else
                        {
                            GameField.messages.mLostLives.ShowMessage();
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
            x = x1 + width / 2;
            y = y1 + height / 2;
            shape.Position = new Vector2f(x1, y1);
        }

        public override void ChangeDirectionX(GameEntity obj)
        {
            direction = (float)(Math.PI) - direction;
            if (obj is Brick brick)
            {
                Game.player.stat.score++;
                brick.decreaseStrength();
            }
        }

        public override void ChangeDirectionY(GameEntity obj)
        {
            direction = - direction;
            if (obj is Brick brick)
            {
                Game.player.stat.score++;
                brick.decreaseStrength();
            }
        }

        public override void ChangeDirection(GameEntity obj)
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
    }

    public class Balls
    {
        public List<Ball> balls = new List<Ball>();
        public Balls()
        { 
            balls = new List<Ball>();
            balls.Add(new Ball(315,315,15,6,(float)(Math.PI/2*0.5),Color.Cyan));
        }
    }
}

