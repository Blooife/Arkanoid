using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SFML.Graphics;
using SFML.System;

namespace Arkanoid
{
    [Serializable]
    public class Ball : GameEntity
    {
        private int radius;
        private int speed;
        public float direction;

        public Ball(int x, int y, int rad, int sp, float dir, Color col, bool mov)
        {
            type = typeof(Ball);
            x1 = (x-rad);
            x2 = (x+rad);
            y1 = (y-rad);
            y2 = (y+rad);
            shape = new CircleShape(rad);
            shape.Position = new Vector2f();
            color = col;
            shape.FillColor = col;
            speed = sp;
            direction = dir;
            isMoving = mov;
            visible = true;
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
                    if (y1<=0)
                    {
                        direction = - direction;
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
                brick.decreaseStrength();
            }
        }
    }

    public class Balls
    {
        public List<Ball> balls = new List<Ball>();
      // public Ball[] balls;
        public Balls()
        {
            balls = new List<Ball>();
          //  balls.Add(new Ball(380,530,410,600,5,(float)(Math.PI*0.1),Color.Cyan, "ball",true));
          //balls.Add(new Ball(300,300,330,330,6,(float)(Math.PI/2*0.5),Color.Cyan, "ball",true));
          balls.Add(new Ball(315,315,15,6,(float)(Math.PI/2*0.5),Color.Cyan, true));
        }
        
    }
}





/*public Ball(int x1, int y1,  int x2, int y2, int sp, float dir, Color col,string t, bool mov)
{
    setx1(x1);
    setx2(x2);
    sety1(y1);
    sety2(y2);
    rad = (x2 - x1) / 2;
    shape = new CircleShape(rad);
    shape.Position = new Vector2f();
    color = col;
    shape.FillColor = col;
    type = t;
    speed = sp;
    direction = dir;
    isMoving = mov;
}*/