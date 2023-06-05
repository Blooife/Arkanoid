using System;
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
        public  static Bonuses bonuses;
        readonly StatusBar statusBar = new StatusBar(); 
        public static readonly Messages Messages = new Messages();
        public static event EventHandler<CollisionEventArgs> OnCollision ;

        public GameField()
        {
            Game.ev.OnCollision += CollisionEvent;
            OnCollision += CollisionEvent;
        }
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
            bonuses.SetBrick(bricks);
            foreach (var b in bonuses.bonuses)
            {
                displayObjects.Add(b);
            }
            
            displayObjects.Add(statusBar);
            displayObjects.Add(Messages.mLostGame);
            displayObjects.Add(Messages.mWinGame);
        }

        public void Update()
        {
            balls = new Balls(1);
            bricks = new Bricks(1);
            platforms = new Platforms(1);
            bonuses = new Bonuses(1);
            foreach (var o in displayObjects)
            {
                if (o is Ball b)
                {
                    balls.balls.Add(b);
                } else if (o is Brick br)
                {
                    bricks.bricks.Add(br);
                } else if (o is Bonus bns)
                {
                    bonuses.bonuses.Add(bns);
                } else if (o is Platform pl)
                {
                    platforms.platforms.Add(pl);
                }
            }
            bonuses.SetBrick(bricks);
            foreach (var b in bonuses.bonuses)
            {
                if (b.Btype == BonusType.Score)
                {
                    displayObjects.Add(b);
                }
            }
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

        public void CollisionEvent(object sender, CollisionEventArgs ev)
        {
            GameEntity o = (GameEntity)sender;
            o.OnCollision(ev.withWho);
        }

        public void CheckGameState()
        {
            bool st = false;
            foreach (var br in bricks.bricks)
            {
                if (br.visible && br.strength != 15)
                {
                    st = true;
                }
            }
            if (!st)
            {
                Messages.mWinGame.ShowMessage();
                foreach (var b in balls.balls)
                {
                    b.visible = false;
                }
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
                           OnCollision?.Invoke(obj, new CollisionEventArgs(obj2));
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