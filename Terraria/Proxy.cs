using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SFML.Graphics;


namespace GameEngine
{
    public class Proxy
    {
        private string path = "C:/Users/Asus/Desktop/TerraEngine-master/TerraEngine-master/Terraria/files/";
        

        public void SerializeToText(List<GameEntity> objs, Player player, Settings settings)
        {
            settings.SerializeObject(settings,path+"s.txt");
            File.WriteAllText(path + "s.txt", "");
            foreach (var obj in objs)
            {
                obj.SerializeObject(obj, path+"s.txt");
            }
            player.SerializeObject(player,path+"s.txt");
            
        }

        public void DeserializeText(List<GameEntity> objs, Player player, Settings settings)
        {
            string[] lines = File.ReadAllLines(path+"save.txt");
            for (int i = 0; i < lines.Length-1; i += 2)
            {
                string[] prop = lines[i+1].Split(' ');
                switch (lines[i])
                {
                        case "Ball":
                        {
                            objs.Add(new Ball(prop));
                            break;
                        }
                        case "Brick":
                        {
                            objs.Add(new Brick(prop));
                            break;
                        }
                        case "Platform":
                        {
                            objs.Add(new Platform(prop));
                            break;
                        }
                        case "Player":
                        {
                            player.UpdateStat(prop[0],Int32.Parse(prop[1]), Int32.Parse(prop[2]));
                            break;
                        }
                        case "Settings":
                        {
                            settings.UpdateSettings(Int32.Parse(prop[0]), Int32.Parse(prop[1]), Int32.Parse(prop[2]), Int32.Parse(prop[3]), Int32.Parse(prop[4]));
                            break;
                        }
                }
            }
        }

        public void SerializeJson(List<GameEntity> objs,Player player, Settings settings)
        {
            string jsonString;
            jsonString = JsonConvert.SerializeObject(objs, Formatting.Indented);
            string jsonPLayer = JsonConvert.SerializeObject(player, Formatting.Indented);
            string jsonSettings = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path+"save.json", jsonString);
            File.WriteAllText(path+"player.json", jsonPLayer);
            File.WriteAllText(path+"settings.json", jsonSettings);
        }

        public void DeserializeJsonFile(List<GameEntity> objs, Player player, Settings settings)
        {
            string jsonSettings =
                File.ReadAllText(path+"settings.json");
            Settings sn = JsonConvert.DeserializeObject<Settings>(jsonSettings);
            settings.UpdateSettings(sn.level, sn.volume, sn.difficulty, sn.size.X, sn.size.Y);
            string json =
                File.ReadAllText(path+"save.json");
            List<dynamic> deserializedObjects = JsonConvert.DeserializeObject<List<dynamic>>(json);
            foreach (var obj in deserializedObjects)
            {
                switch (obj.type.ToString())
                {
                    case "Ball":
                    {
                        objs.Add(new Ball((int)obj.x1, (int)obj.y1, (int)obj.radius, (int)obj.speed, (double)obj.direction, new Color(
                            (byte)obj.color.R,(byte)obj.color.G,(byte)obj.color.B)));   
                     break;   
                    }
                    case "Platform":
                    {
                        objs.Add(new Platform((int)obj.x1, (int)obj.y1, (int)obj.x2,(int)obj.y2, new Color(
                            (byte)obj.color.R,(byte)obj.color.G,(byte)obj.color.B), (int)obj.speed));    
                        break;
                    }
                    case "Brick":
                    {
                        objs.Add(new Brick((int)obj.x1, (int)obj.y1, (int)obj.x2,(int)obj.y2, (int)obj.strength,new Color(
                            (byte)obj.color.R,(byte)obj.color.G,(byte)obj.color.B), (bool)obj.visible));   
                        break;
                    }
                    case "Bonus":
                    {
                        if((BonusType)obj.Btype != BonusType.Score)
                        objs.Add(new Bonus("", (BonusType)obj.Btype, (bool)obj.visible,  (int)obj.x1, (int)obj.y1));
                        break;
                    }
                }
            }
            string jsonPl =
                File.ReadAllText(path+"player.json");
            Player pl = JsonConvert.DeserializeObject<Player>(jsonPl);
            player.UpdateStat(pl.stat.name, pl.stat.score, pl.stat.lives);
            
        }
        
        public void SerializeText(List<GameEntity> objs, Player player, Settings settings)
        {
            File.WriteAllText(path+"save.txt","");
            settings.SerializeToText(path+"save.txt");
            foreach (var obj in objs)
            {
                obj.SerializeToText(path+"save.txt");
                obj.SerializeToText(path+"s.txt");
            }
            player.SerializeToText(path+"save.txt");
            
        }
    }
}