namespace MyRPGGame
{
    public class Item
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public bool IsUnique { get; set; }

        public Item()
        {
            Count = 1;
        }
    }
}