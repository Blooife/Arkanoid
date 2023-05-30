using System;

namespace GameEngine
{
    public class CollisionEventArgs : EventArgs
    {
        public GameEntity withWho;

        public CollisionEventArgs(GameEntity w)
        {
            withWho = w;
        }
    }
}