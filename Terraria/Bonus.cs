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
    }

    public class Bonuses
    {
        private List<Bonus> bonuses = new List<Bonus>();
    }
}