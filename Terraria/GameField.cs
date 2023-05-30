using System.Collections.Generic;
using SFML.Graphics;

namespace GameEngine
{
    public class GameField
    {
        public readonly List<GameEntity> displayObjects = new List<GameEntity>();
        Balls balls;
        Bricks bricks;
        Platforms platforms;
        public Bonuses bonuses;
        readonly StatusBar statusBar = new StatusBar(); 
        public static readonly Messages Messages = new Messages();

        public void NewGame()
        {
            balls = new Balls();
            bricks = new Bricks();
            platforms = new Platforms();
            bonuses = new Bonuses();
            foreach (var b in balls.balls)
            {
                displayObjects.Add(b);
            }
            foreach (var b in bricks.bricks)
            {
                displayObjects.Add(b);
            }
            foreach (var b in platforms.platforms)
            {
                displayObjects.Add(b);
            }
            foreach (var b in bonuses.bonuses)
            {
                displayObjects.Add(b);
            }
            bonuses.SetBrick(bricks);
            displayObjects.Add(statusBar);
            displayObjects.Add(Messages.mLostGame);
            displayObjects.Add(Messages.mWinGame);
        }

        public void Update()
        {
            displayObjects.Add(statusBar);
            displayObjects.Add(Messages.mLostGame); 
            displayObjects.Add(Messages.mWinGame);
            displayObjects.Add(Game.menu);
            Game.settings.AddToList(displayObjects);
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
                        if (obj is Bonus bonus)
                        {
                            string t;
                        }
                        if (obj.CheckCollisions(obj2))
                        {
                            
                            obj.OnCollision(obj2);
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