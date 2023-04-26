using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arkanoid
{
    [Serializable]
    public class Platform: GameEntity
    {
        
        public int speed;
        public Platform(int x1, int y1,  int x2, int y2, Color col, int sp, bool mov)
        {
            type = typeof(Platform);
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
                    x1 = (x1 - speed);
                    x2 = (x2 - speed);
                }
                else if (moveRight)
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
        public List<Platform> platforms;

        public Platforms()
        {
            platforms = new List<Platform>();
            platforms.Add(new Platform(350, 570, 470, 590, Color.Green, 6, true ));
        }
        
    }
}