using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace Arkanoid
{
    public class Player
    {
        public PlayerStatistics stat;
        public string type;

        public Player()
        {
            stat = new PlayerStatistics("Player1", 0, 3);
            type = "Player";
        }
        
        public void SerializeToText(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename,true))
            {
                writer.WriteLine("Player");
                writer.WriteLine($"{stat.name} {stat.score} {stat.lives}");
                writer.Flush();
            }
        }

        public string GetStat()
        {
            return $"Name:{stat.name}   Score:{stat.score}     Lives:{stat.lives}   Level:{Game.settings.level}   Difficulty:{Game.settings.difficulty}";
        }
        public void UpdateStat(string n,int score, int lives)
        {
            stat.name = n;
            stat.lives = lives;
            stat.score = score;
        }
    }

    public class Players
    {
        public List<Player> players = new List<Player>();

        public Players()
        {
            players.Add(new Player());
        }
    }

    public class PlayerStatistics
    {
        [JsonInclude] public string name;
        [JsonInclude] public int score;
        [JsonInclude] public int lives;

        public PlayerStatistics(string n, int s, int l)
        {
            name = n;
            score = s;
            lives = l;
        }
        public void Update()
        {
            
        }
    }
}