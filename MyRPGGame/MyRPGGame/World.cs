using System.Collections.Generic;

namespace MyRPGGame
{
    public class World
    {
        public bool IsFighting { get; set; }
        public Stack<Character> Active { get; set; }
        public Essence[,] Map;

        public World(int width, int height)
        {
            Map = new Essence[width,height];
            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
                Map[i, j] = Essence.Terrain;
            Active = new Stack<Character>();
            IsFighting = false;
        }
    }
}