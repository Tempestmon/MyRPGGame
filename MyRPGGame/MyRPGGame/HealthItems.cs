namespace MyRPGGame
{
    public class PainKiller : HealthItem
    {
        public PainKiller()
        {
            Name = "Painkillers";
            HealthRestore = 20;
            RequiredMovePoint = 2;
        }
    }

    public class Bandage : HealthItem
    {
        public Bandage()
        {
            Name = "Bandage";
            HealthRestore = 10;
            RequiredMovePoint = 2;
        }
    }
    
    public class HealthKit : HealthItem
    {
        public HealthKit()
        {
            Name = "HealthKit";
            HealthRestore = 40;
            RequiredMovePoint = 3;
        }
    }
}