namespace MyRPGGame
{
    public class WeaponRequirement
    {
        public Tier Tier { get; set; }
        public int Level { get; set; }

        public WeaponRequirement(Tier tier, int level)
        {
            Tier = tier;
            Level = level;
        }
    }
}