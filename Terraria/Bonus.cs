using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Bonus: Text
    {
        private Texture texture;
        public Sprite sprite;
        [JsonProperty]public BonusType Btype;
        public Bonus(string str):base(str)
        {
            type = "Bonus";
            text.CharacterSize = 9;
            visible = false; //////////////
            isMoving = true;
            RandBonus();
        }
        
        public Bonus(string str, BonusType t, bool v, int x, int y):base(str)
        {
            type = "Bonus";
            Btype = t;
            text.CharacterSize = 9;
            visible = v; 
            isMoving = true;
            SetBonus();
            x1 = x;
            y1 = y;
            UpdatePosition();
        }

        public void SetBonus()
        {
            switch (this.Btype)
            {
                case BonusType.MinusPlatf:
                {
                    texture = TB.t3;
                    break;
                }
                case BonusType.PlusPlatf:
                {
                    texture = TB.t2;
                    break;
                }
                case BonusType.PlusLive:
                {
                    texture = TB.t4;
                    break;
                }
                case BonusType.PlusPoints:
                {
                    texture = TB.t1;
                    text.DisplayedString = "100";
                    text.CharacterSize = 9;
                    break;
                }
                case BonusType.PlusSpeedPl:
                {
                    texture = TB.t5;
                    break;
                }
            }

            sprite = new Sprite(texture);
        }

        public void RandBonus()
        {
            int t = Game.rndm.Next(0, 5);
            switch (t)
            {
                case 0:
                {
                    texture = TB.t1;
                    Btype = BonusType.PlusPoints;
                    text.DisplayedString = "100";
                    break;
                }
                case 1:
                {
                    texture = TB.t2;
                    Btype = BonusType.PlusPlatf;
                    break;
                }
                case 2:
                {
                    texture = TB.t3;
                    Btype = BonusType.MinusPlatf;
                    break;
                }
                case 3:
                {
                    texture = TB.t4;
                    Btype = BonusType.PlusLive;
                    break;
                }
                case 4:
                {
                    texture = TB.t5;
                    Btype = BonusType.PlusSpeedPl;
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
                switch (this.Btype)
                {
                    case BonusType.MinusPlatf:
                    {
                        platform.ChangeWidth(-1);
                        break;
                    }
                    case BonusType.PlusPlatf:
                    {
                        platform.ChangeWidth(+1);
                        break;
                    }
                    case BonusType.PlusLive:
                    {
                        Game.player.stat.lives += 1;
                        break;
                    }
                    case BonusType.PlusPoints:
                    {
                        Game.player.stat.score += 100;
                        break;
                    }
                    case BonusType.PlusSpeedPl:
                    {
                        if(platform.speed <=8)
                            platform.speed += 1;
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
        
        public Bonuses(int n)
        {
            
        }
        
       


        public void SetBrick(Bricks br)
        {
            int i = 0;
            foreach (var bonus in bonuses)
            {
                Brick brick = br.bricks[i];
                if (brick.visible && !bonus.visible)
                {
                    br.bricks[i].bonus = bonus;
                    bonus.x1 = brick.x1;
                    bonus.y1 = brick.y1;
                    bonus.x2 = bonus.x1 + 20;
                    bonus.y2 = bonus.y1 + 15;
                }
                i++;
            }
        }
        
        
    }
}