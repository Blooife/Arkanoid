using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;


namespace Arkanoid
{
    [Serializable]
    public class Brick: GameEntity
    {
        public int strength;
        public Bonuses bonuses;

        public Brick(int x1, int y1,  int x2, int y2, int str, Color col, bool mov)
        {
            type = typeof(Brick);
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
            isMoving = mov;
            visible = true;
        }

        public override void Move()
        {
            
        }

        public void decreaseStrength()
        {
            strength -= 1;
            if (strength == 1)
            {
                color = Color.Blue;
                shape.FillColor = Color.Blue;
            }
            else if(strength == 0)
            {
                visible = false;
            }
        }
        
    }

    public class Bricks
    {
        public List<Brick> bricks = new List<Brick>();


        public Bricks()
        {
            bricks = new List<Brick>();
          //  bricks.Add(new Brick(50,100,100,120,1,Color.Blue,false));
            for (int i = 1; i < 10; i++)
            {
                bricks.Add(new Brick(i*70, 60,i*70+70, 80, 1, Color.Blue, false));
                bricks.Add(new Brick(i*70, 80,i*70+70, 100, 2, Color.Red, false));
                bricks.Add(new Brick(i*70, 100,i*70+70, 120, 1, Color.Blue,  false));
                bricks.Add(new Brick(i*70, 120,i*70+70, 140, 2, Color.Red,  false));
            }
               
        }
    }
}