using System.Drawing;

namespace MyRPGGame
{
    public class Entrance
    {
        public Point Position { get; set; }
        public World CurrentWorld { get; set; }
        public World EnterWorld { set; get; }

        public Entrance(Point position, World current, World enter)
        {
            Position = position;
            CurrentWorld = current;
            EnterWorld = enter;
        }
    }
}