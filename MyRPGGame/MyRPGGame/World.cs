using System.Collections.Generic;
using System.Drawing;

namespace MyRPGGame
{
    public class World
    {
        public bool IsFighting { get; set; }
        public List<Character> Characters { get; set; }
        public Essence[,] Map;
        public List<Entrance> Entrances { get; set; }

        public World(int width, int height)
        {
            Map = new Essence[width,height];
            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
                Map[i, j] = Essence.Terrain;
            Characters = new List<Character>();
            Entrances = new List<Entrance>();
            IsFighting = false;
        }

        public void SetEntrance(Entrance entrance)
        {
            Entrances.Add(entrance);
            Map[entrance.Position.X, entrance.Position.Y] = Essence.Entrance;
        }
    }
}