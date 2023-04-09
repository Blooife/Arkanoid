using System.Collections.Generic;

namespace Arkanoid
{
    public class MenuItem: GameEntity
    {
        private string info;

        public MenuItem()
        {
            
        }
        
        public override void Move()
        {
            
        }

        public override bool CheckCollisions(GameEntity obj)
        {
            return false;
        }
        public override void decreaseStrength()
        {
            
        }
        
    }

    public class Menu
    {
        private List<MenuItem> menu = new List<MenuItem>();

        public void ShowMenu()
        {
            
        }

        public void HideMenu()
        {
            
        }
    }
}