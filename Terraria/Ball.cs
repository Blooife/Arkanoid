using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Arkanoid
{
    public class Ball : GameEntity
    {
        private int radius;
        private int speed;
        private float direction;
        public int rad;

        public Ball(int x, int y, int rad, int sp, float dir, Color col,string t, bool mov)
        {
            setx1(x-rad);
            setx2(x+rad);
            sety1(y-rad);
            sety2(y+rad);
            shape = new CircleShape(rad);
            shape.Position = new Vector2f();
            color = col;
            shape.FillColor = col;
            type = t;
            speed = sp;
            direction = dir;
            isMoving = mov;
            visible = true;
        }

        public float getDirection()
        {
            return direction;
        }

        public void setDirection(float dir)
        {
            direction = dir;
        }

        public override void Move()
        {
            if (getx1()<=0)
            {
                setDirection((float)(Math.PI) - getDirection());
            }else{
                if (getx2()>=800)
                {
                    setDirection((float)(Math.PI) - getDirection());
                }
                else {
                    if (gety1()<=0)
                    {
                        setDirection( - getDirection());
                    }
                }
            }
            float dx = (float) Math.Cos(direction) * speed;
            float dy = (float) Math.Sin(direction) * speed;

            setx1((int)(getx1() + dx));
            setx2((int)(getx2() + dx));
            sety1((int)(gety1() + dy));
            sety2((int)(gety2() + dy));
            shape.Position = new Vector2f(getx1(), gety1());
        }
        public override bool CheckCollisions(GameEntity obj)
        {
            bool res = false;
            if(gety2() >= obj.gety1() && gety1() <= obj.gety2())
                if (getx2() >= obj.getx1() && getx1() <= obj.getx2())
                {
                   // if (getx1() >= obj.getx2() || getx2() <= obj.getx1())
                    if(gety1() >= obj.gety2() || gety2() <= obj.gety1())
                    {
                        setDirection( - getDirection());
                    }
                    else 
                    {
                        setDirection((float)(Math.PI) - getDirection());
                    }
                    res = true;
                    if (obj is Brick brick)
                    {
                        brick.decreaseStrength();
                    }
                }
            return res;
        }
        
        public override void decreaseStrength()
        {
            
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
          balls.Add(new Ball(315,315,15,6,(float)(Math.PI/2*0.5),Color.Cyan, "ball",true));
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