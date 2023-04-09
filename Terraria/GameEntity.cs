using System.Data;
using SFML.Graphics;
using SFML.System;

namespace Arkanoid
{
    public abstract class GameEntity : SFML.Graphics.Drawable
    {
        public Shape shape;
        int x1, y1, x2, y2, x, y;

        public Color color;
        public int backgroundColor;
        public string type;
        public bool visible;
        public bool isMoving;

        public GameEntity()
        {
            
        }


        public int getx1() { return x1; }
        public int getx2() { return x2; }
        public int gety1() { return y1; }
        public int gety2() { return y2; }

        public void setx1(int coor) { x1 = coor;}
        public void sety1(int coor) { y1 = coor;}
        public void setx2(int coor) { x2 = coor;}
        public void sety2(int coor) { y2 = coor;}
        
        public void Draw(RenderTarget target, RenderStates states)
        {
            throw new System.NotImplementedException();
        }

        public abstract void Move();


        public void draw(RenderWindow window)
        {
            shape.Position = new Vector2f(getx1(), gety1());
            window.Draw(shape);
        }

        public string getType()
        {
            return type;
        }
        
        public void setType(string t)
        {
            type = t;
        }

      public abstract bool CheckCollisions(GameEntity obj);
      public abstract void decreaseStrength();


    }
}