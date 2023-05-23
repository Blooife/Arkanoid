using System.Collections.Generic;
using GameEngine;
using SFML.Graphics;
using SFML.Window;

namespace Arkanoid
{
    public class GameField
    {
        private int X, Y, X1, Y1;
        public List<GameEntity> displayObjects = new List<GameEntity>();
        Balls balls;
        Bricks bricks;
        Platforms platforms; 
        StatusBar statusBar = new StatusBar(); 
        public static Messages messages = new Messages();
        public GameField()
        {
        }

        public void NewGame()
        {
            balls = new Balls();
            bricks = new Bricks();
            platforms = new Platforms();
            AddObjects(balls.balls.ToArray());
            AddObjects(bricks.bricks.ToArray());
            AddObjects(platforms.platforms.ToArray());
            displayObjects.Add(statusBar);
            displayObjects.Add(messages.mLostLives);
            displayObjects.Add(messages.mLostGame);
            displayObjects.Add(messages.mWinGame);
        }

        public void Update()
        {
            displayObjects.Add(statusBar);
            displayObjects.Add(messages.mLostLives);
            displayObjects.Add(messages.mLostGame); 
            displayObjects.Add(messages.mWinGame);
            displayObjects.Add(Game.menu);
            AddObjects(Game.settings.listBtn.ToArray());
        }
        
        public void AddObjects(GameEntity[] objects)
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
                            obj.ChangeDirection(obj2);
                            break;
                        }
                    }
            }
        }

        public void MoveObjects()
        {
            foreach (var obj in displayObjects)
            {
                if(obj.isMoving && obj.visible)
                    obj.Move();
            }
        }

        public void UpdateDifficulty(int d)
        {
            if (balls != null && platforms != null)
            {
                foreach (var b in balls.balls)
                {
                    b.speed = d;
                }
                foreach (var p in platforms.platforms)
                {
                    p.speed = d;
                }  
            }
        }
        
        public void DrawObjects(RenderWindow window)
        {
            foreach (var obj in displayObjects.ToArray())
            {
                if(obj.visible)
                    obj.draw(window);
            }
        }
    }
}