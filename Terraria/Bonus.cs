using System.Collections.Generic;

namespace Arkanoid
{
    public class Bonus: GameEntity
    {
        private int type;

        public Bonus()
        {
            
        }
        
        public override void Move()
        {
            throw new System.NotImplementedException();
        }

        public override bool CheckCollisions(GameEntity obj)
        {
            return false;
        }
        public override void decreaseStrength()
        {
            
        }
    }

    public class Bonuses
    {
        private List<Bonus> bonuses = new List<Bonus>();
    }
}