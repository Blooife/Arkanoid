using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;


namespace Arkanoid
{
    public class Brick: GameEntity
    {
        public int strength;
        public Bonuses bonuses;

        public Brick(int x1, int y1,  int x2, int y2, int str, Color col, string t, bool mov)
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
            strength = str;
            type = t;
            isMoving = mov;
            visible = true;
        }

        public override void Move()
        {
            throw new System.NotImplementedException();
        }

        public override bool CheckCollisions(GameEntity obj)
        {
            return false;
        }
        
        public override void decreaseStrength()
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
                bricks.Add(new Brick(i*70, 60,i*70+70, 80, 1, Color.Blue, "brick", false));
                bricks.Add(new Brick(i*70, 80,i*70+70, 100, 2, Color.Red, "brick", false));
                bricks.Add(new Brick(i*70, 100,i*70+70, 120, 1, Color.Blue, "brick", false));
                bricks.Add(new Brick(i*70, 120,i*70+70, 140, 2, Color.Red, "brick", false));
            }
               
        }
    }
}