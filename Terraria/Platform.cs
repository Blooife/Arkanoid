using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arkanoid
{
    public class Platform: GameEntity
    {
        
        private int speed;
        public Platform(int x1, int y1,  int x2, int y2, Color col, string t, int sp, bool mov)
        {
            setx1(x1);
            setx2(x2);
            sety1(y1);
            sety2(y2);
            shape = new RectangleShape(new Vector2f(x2-x1,y2-y1));
            shape.Position = new Vector2f(x1,y1);
            color = col;
            shape.FillColor = col;
            shape.OutlineColor= Color.Black;
            shape.OutlineThickness = 1;
            speed = sp;
            type = t;
            isMoving = mov;
            visible = true;
        }

        public void ChangeWidth(int w)
        {
            
        }

        public override void Move()
        { 
            bool moveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            bool moveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
            Keyboard.Key k;
            if (moveLeft || moveRight)
            {
                if (moveLeft)
                {
                    setx1(getx1() - speed);
                    setx2(getx2() - speed);
                }
                else if (moveRight)
                {
                    setx1(getx1() + speed);
                    setx2(getx2() + speed);
                }
                
                if (getx1() < 0)
                {
                    setx2(getx2() - getx1());
                    setx1(0);
                }
                else if (getx2() > 800)
                {
                    setx1(800 - (getx2() - getx1()));
                    setx2(800);
                }
            }
        }

        public override bool CheckCollisions(GameEntity obj)
        {
            return false;
        }

        public override void decreaseStrength()
        {
            
        }
    }

    public class Platforms
    {
        public List<Platform> platforms;

        public Platforms()
        {
            platforms = new List<Platform>();
            platforms.Add(new Platform(350, 570, 470, 590, Color.Green,"platform", 6, true ));
        }
        
    }
}