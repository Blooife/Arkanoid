using System.Collections.Generic;

namespace Arkanoid
{
    public class Player
    {
        private string name;
        private PlayerStatistics playerStatistics;

        public Player()
        {
            
        }
        
    }

    public class Players
    {
        private List<Player> players = new List<Player>();
    }

    public class PlayerStatistics
    {
        private int score;
        private int time;
        private int lives;

        public void Update()
        {
            
        }
    }
}