using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    public class Bonus: Text
    {
        private Texture texture;
        public Sprite sprite;
        public BonusType type;
        public Bonus(string str):base(str)
        {
            text.CharacterSize = 8;
            visible = false; //////////////
            isMoving = true;
            RandBonus();
        }

        public void RandBonus()
        {
            int t = Game.rndm.Next(0, 4);
           // t = 0;
            switch (t)
            {
                case 0:
                {
                    texture = TB.t1;
                    type = BonusType.PlusPoints;
                    text.DisplayedString = "100";
                    break;
                }
                case 1:
                {
                    texture = TB.t2;
                    type = BonusType.PlusPlatf;
                    break;
                }
                case 2:
                {
                    texture = TB.t3;
                    type = BonusType.MinusPlatf;
                    break;
                }
                case 3:
                {
                    texture = TB.t4;
                    type = BonusType.PlusLive;
                    break;
                }
                case 4:
                {
                    texture = TB.t5;
                    type = BonusType.PlusSpeedPl;
                    break;
                }
            }
            sprite = new Sprite(texture);
        }

        public override void draw(RenderWindow window)
        {
            window.Draw(sprite);
            window.Draw(text);
        }

        public void UpdatePosition()
        {
            x2 = x1 + 30;
            y2 = y1 + 25;
            sprite.Position = new Vector2f(x1, y1);
            text.Position = new Vector2f(x1, y1);
        }

        public override void Move()
        {
            if (visible)
            {
                if (y1 <=600)
                {
                    y1 += 1;
                    x2 = x1 + 30;
                    y2 = y1 + 25;
                }
                else
                {
                    visible = false;
                }
                sprite.Position = new Vector2f(x1, y1);
                text.Position = new Vector2f(x1, y1);
            }
        }

        public override void OnCollision(GameEntity obj)
        {
            if (obj is Platform platform)
            {
                this.visible = false;
                switch (this.type)
                {
                    case BonusType.MinusPlatf:
                    {
                        obj.ChangeWidth(-1);
                        break;
                    }
                    case BonusType.PlusPlatf:
                    {
                        obj.ChangeWidth(+1);
                        break;
                    }
                    case BonusType.PlusLive:
                    {
                        break;
                    }
                    case BonusType.PlusPoints:
                    {
                        break;
                    }
                    case BonusType.PlusSpeedPl:
                    {
                        break;
                    }
                }
            }
        }
    }
    
    public static class TB
    {
        public static Texture t1 =
            new Texture("C:/Users/Asus/Desktop/TerraEngine-master/TerraEngine-master/Terraria/bSprites/бонус.PNG");
        public static Texture t2 =
            new Texture("C:/Users/Asus/Desktop/TerraEngine-master/TerraEngine-master/Terraria/bSprites/бонус1.PNG");
        public static Texture t3 =
            new Texture("C:/Users/Asus/Desktop/TerraEngine-master/TerraEngine-master/Terraria/bSprites/бонус2.PNG");
        public static Texture t4 =
            new Texture("C:/Users/Asus/Desktop/TerraEngine-master/TerraEngine-master/Terraria/bSprites/бонус3.PNG");
        public static Texture t5 =
            new Texture("C:/Users/Asus/Desktop/TerraEngine-master/TerraEngine-master/Terraria/bSprites/бонус6.PNG");
    }

    public enum BonusType
    {
        PlusPoints,
        PlusPlatf,
        MinusPlatf,
        PlusLive,
        PlusSpeedPl,
    }

    public class Bonuses
    {
        public List<Bonus> bonuses = new List<Bonus>();

        public Bonuses()
        {
            for (int i = 0; i < 36; i++)
            {
                bonuses.Add(new Bonus("")); 
            }
        }


        public void SetBrick(Bricks br)
        {
            int i = 0;
            foreach (var bonus in bonuses)
            {
                Brick brick = br.bricks[i];
                br.bricks[i].bonus = bonus;
                bonus.x1 = brick.x1;
                bonus.y1 = brick.y1;
                bonus.x2 = bonus.x1 + 20;
                bonus.y2 = bonus.y1 + 15;
                i++;
            }
        }
        
        
    }
}