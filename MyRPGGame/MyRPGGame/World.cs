using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyRPGGame
{
    public class World
    {
        public bool IsFighting { get; set; }
        public List<Character> Characters { get; set; }
        public Queue<Character> Queue { get; set; }
        public Essence[,] Map;
        public Dictionary<Point, List<Item>> GroundItem { get; set; }
        public List<Entrance> Entrances { get; set; }

        public World(int width, int height)
        {
            Map = new Essence[width,height];
            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
                Map[i, j] = Essence.Terrain;
            Characters = new List<Character>();
            Entrances = new List<Entrance>();
            GroundItem = new Dictionary<Point, List<Item>>();
            IsFighting = false;
        }

        public void SetEntrance(Entrance entrance)
        {
            Entrances.Add(entrance);
            Map[entrance.Position.X, entrance.Position.Y] = Essence.Entrance;
        }

        public void PlaceItem(Item item, Point point)
        {
            if(item.IsUnique)
                return;
            var result = new List<Item>();
            if (GroundItem.ContainsKey(point) && GroundItem[point].Count > 0)
            {
                foreach (var i in GroundItem[point].Where(i => i.Name == item.Name))
                {
                    item.Count += i.Count; 
                    result.Add(item);
                }

                GroundItem[point] = result;
            }
            else
                GroundItem[point] = new List<Item>{item};
        }

        public void PlaceItem(List<Item> items, Point point)
        {
            if (GroundItem.ContainsKey(point))
            {
                if (GroundItem[point].Count <= 0)
                    GroundItem[point] = items.ToList();
                else
                {
                    var groundItemName = GroundItem[point].Select(x => x.Name).Distinct().ToList();
                    var result = new List<Item>();
                    foreach (var i in items.Where(i => !i.IsUnique))
                    {
                        for (var j = 0; j < GroundItem[point].Count; j++)
                        {
                            if (i.Name == GroundItem[point][j].Name)
                            {
                                var item = i;
                                item.Count += GroundItem[point][j].Count;
                                result.Add(item);
                            }

                            if (groundItemName.Contains(i.Name)) continue;
                            result.Add(i);
                            groundItemName.Add(i.Name);
                        }
                    }

                    GroundItem[point] = result;
                    items.Clear();
                }
            }
            else
                GroundItem[point] = items.ToList();
        }

        public void SetQueue()
        {
            Queue = new Queue<Character>(Characters.OrderBy(x => x.Statistics.Agility).ToList());
        }

        public void Fight()
        {
            while (IsFighting)
            {
                while (Queue.Count > 0)
                {
                    var current = Queue.Dequeue();
                    while (current.IsCurrent)
                    {
                        current.CurrentMovePoints = current.Statistics.MovePoints;
                        if(!current.IsPlayer)
                            current.AIFight();
                    }
                }
            }
        }
    }
}