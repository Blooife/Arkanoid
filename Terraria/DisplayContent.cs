using System.Collections.Generic;
using SFML.Graphics;

namespace Arkanoid
{
    public class DisplayObjects
    {
       // private GameEntity[] displayContent;
        private List<GameEntity> displayObjects = new List<GameEntity>();

        public DisplayObjects()
        {
            Balls balls = new Balls();
            Bricks bricks = new Bricks();
            Platforms platforms = new Platforms();
            addObjects(balls.balls.ToArray());
            addObjects(bricks.bricks.ToArray());
            addObjects(platforms.platforms.ToArray());
        }

        public void addObjects(GameEntity[] objects)
        {
            foreach (var obj in objects)
            {
                displayObjects.Add(obj);
            }
        }

        public void CheckCollisions()
        {
            foreach (var obj in displayObjects)
            {
                if(obj.isMoving && obj.visible)
                    foreach (var obj2 in displayObjects)
                    {
                        if(obj.Equals(obj2) || !obj2.visible) continue;
                        if (obj.CheckCollisions(obj2))
                        {
                            break;
                        }
                    }
            }
        }

        public void moveObjects()
        {
            foreach (var obj in displayObjects)
            {
                if(obj.isMoving && obj.visible)
                    obj.Move();
            }
        }

        public void drawObjects(RenderWindow window)
        {
            foreach (var obj in displayObjects)
            {
                if(obj.visible)
                    obj.draw(window);
            }
        }
    }
}