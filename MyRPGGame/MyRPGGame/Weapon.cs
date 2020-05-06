namespace MyRPGGame
{
    public class Weapon : Item
    {
        public int Damage { get; set; }
        public int Range { get; set; }
        public int Capacity { get; set; }
        public int CurrentCapacity { get; set; }
        public int AttackPoint { get; set; }
        public WeaponRequirement WeaponRequirement { get; set; }
    }
}