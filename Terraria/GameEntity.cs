using System;
using System.Data;
using SFML.Graphics;
using SFML.System;

namespace Arkanoid
{
    public abstract class GameEntity : SFML.Graphics.Drawable
    {
        public Shape shape;
        public int x1, y1, x2, y2, x, y;

        public Color color;
        public int backgroundColor;
        public bool visible;
        public bool isMoving;
        public Type type;

        public GameEntity()
        {
            
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            throw new System.NotImplementedException();
        }

        public abstract void Move();


        public void draw(RenderWindow window)
        {
            shape.Position = new Vector2f(x1, y1);
            window.Draw(shape);
        }

        public bool CheckCollisions(GameEntity obj)
        {
            if(y2 >= obj.y1 && y1 <= obj.y2)
                if (x2 >= obj.x1 && x1 <= obj.x2)
                {
                    return true;
                }
            return false;
        }

        public virtual void Serialize(GameEntity obj)
        {
            
        }

        public virtual void ChangeDirection(GameEntity obj)
        {
        }


    }
}