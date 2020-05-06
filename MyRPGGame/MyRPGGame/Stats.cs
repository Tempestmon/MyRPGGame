namespace MyRPGGame
{
    public class Stats
    {
        public int Level { get; set; }
        public int ExperiencePoint { get; set; }
        public int ExpToLevelUp { get; set; }
        public int HealthPoint { get; set; }
        public int MovePoints { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }

        public Stats()
        {
            Strength = 1;
            Agility = 1;
            Intelligence = 1;
            Level = 1;
            HealthPoint = 50 + Strength * 15;
            MovePoints = Agility;
            ExperiencePoint = 0;
            ExpToLevelUp = 25 * (4 * (Level + 1) + 2) * Level;
        }

        public Stats(int strength, int agility, int intelligence)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Level = 1;
            HealthPoint = 50 + Strength * 15;
            MovePoints = Agility;
            ExperiencePoint = 0;
            ExpToLevelUp = 25 * (4 * (Level + 1) + 2) * Level;
        }
            
        public Stats(int strength, int agility, int intelligence, int level)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Level = level;
            HealthPoint = 50 + Strength * 15;
            MovePoints = Agility;
            ExperiencePoint = 25 * (4 * Level + 2);
            ExpToLevelUp = 25 * (4 * (Level + 1) + 2) * Level;
        }
        
        public Stats(int strength, int agility, int intelligence, int level, int experiencePoint)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Level = level;
            HealthPoint = 50 + Strength * 15;
            MovePoints = Agility;
            ExperiencePoint = experiencePoint;
            ExpToLevelUp = 25 * (4 * (Level + 1) + 2) * Level;
        }
    }
}