using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

namespace MyRPGGame
{
    public class GameModel
    {
        private static bool IsFighting;
        public static Stack<Character> Playing;

        public GameModel()
        {
            Playing = new Stack<Character>();
            IsFighting = false;
            var world = new World(10, 10);
        }
        
        public class World
        {
            public Essence[,] Map;

            public World(int width, int height)
            {
                Map = new Essence[width,height];
                for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                    Map[i, j] = Essence.Terrain;
            }
        }
        
        public enum Essence
        {
            Character,
            Terrain,
            Entrance
        }

        public class Stats
        {
            public int Strength { get; set; }
            public int Agility { get; set; }
            public int Intelligence { get; set; }

            public Stats()
            {
                Strength = 1;
                Agility = 1;
                Intelligence = 1;
            }

            public Stats(int strength, int agility, int intelligence)
            {
                Strength = strength;
                Agility = agility;
                Intelligence = intelligence;
            }
        }

        public enum Tier
        {
            Strength,
            Intelligence,
            Agility
        }

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

        public class Item
        {
            public string Name { get; set; }
            public bool IsUnique { get; set; }
        }

        public class Weapon : Item
        {
            public int Damage { get; set; }
            public int Range { get; set; }
            public int Capacity { get; set; }
            public int CurrentCapacity { get; set; }
            public int AttackPoint { get; set; }
            public WeaponRequirement WeaponRequirement { get; set; }
        }

        public class Character
        {
            public Point Position { get; set; }
            private Stats Statistics { get; set; }
            private int HealthPoint { get; set; }
            public int CurrentHealthPoint { get; set; }
            private int MovePoints { get; set; }
            public int CurrentMovePoints { get; set; }
            private List<Item> Inventory { get; set; }
            public Weapon CurrentWeapon { get; private set; }
            private bool IsCurrent { get; set; }
            private bool IsPlayer { get; set; }
            private List<Character> Enemies { get; set; }

            public Character(Stats stats, World world, bool isPlayer)
            {
                Statistics = stats;
                IsPlayer = isPlayer;
                HealthPoint = 50 + stats.Strength * 15;
                MovePoints = stats.Agility;
                CurrentHealthPoint = HealthPoint;
                CurrentMovePoints = MovePoints;
                CurrentWeapon = new Fist();
                Inventory = new List<Item> {CurrentWeapon};
                Enemies = new List<Character>();
                CurrentWeapon.CurrentCapacity = CurrentWeapon.Capacity;
                world.Map[Position.X, Position.Y] = Essence.Character;
            }

            public Character(Stats stats, World world)
            {
                Statistics = stats;
                IsPlayer = false;
                HealthPoint = 50 + stats.Strength * 15;
                MovePoints = stats.Agility;
                CurrentHealthPoint = HealthPoint;
                CurrentMovePoints = MovePoints;
                CurrentWeapon = new Fist();
                Inventory = new List<Item> {CurrentWeapon};
                Enemies = new List<Character>();
                CurrentWeapon.CurrentCapacity = CurrentWeapon.Capacity;
                world.Map[Position.X, Position.Y] = Essence.Character;
            }
            
            public void Reload()
            {
                if(IsFighting)
                    CurrentMovePoints -= 2;
                CurrentWeapon.CurrentCapacity = CurrentWeapon.Capacity;
            }

            public double GetRange(Character target)
            {
                var differenceX = (target.Position.X - Position.X) * (target.Position.X - Position.X);
                var differenceY = (target.Position.Y - Position.Y) * (target.Position.Y - Position.Y);
                return Math.Sqrt(differenceX + differenceY);
            }

            public bool CheckRequirement(Weapon weapon)
            {
                return weapon.WeaponRequirement.Tier == Tier.Agility 
                   && weapon.WeaponRequirement.Level <= Statistics.Agility 
                   || weapon.WeaponRequirement.Tier == Tier.Intelligence
                   && weapon.WeaponRequirement.Level <= Statistics.Intelligence
                   || weapon.WeaponRequirement.Tier == Tier.Strength
                   && weapon.WeaponRequirement.Level <= Statistics.Strength;
            }

            public void ChangeWeapon(Weapon weapon)
            {
                if (CheckRequirement(weapon))
                    CurrentWeapon = weapon;
            }

            public void PickUpWeapon(Weapon weapon)
            {
                Inventory.Add(weapon);
            }
            
            public void Attack(Character target, World world)
            {
                Enemies.Add(target);
                //if (!IsCurrent) return;
                if (CurrentMovePoints < CurrentWeapon.AttackPoint || CurrentWeapon.Range * Math.Sqrt(2) < GetRange(target) ||
                    CurrentWeapon.CurrentCapacity <= 0) return;
                CurrentWeapon.CurrentCapacity--;
                target.CurrentHealthPoint -= CurrentWeapon.Damage;
                if (IsPlayer && Enemies.Count == 0)
                    IsFighting = false;
                if (target.CurrentHealthPoint <= 0)
                    world.Map[target.Position.X, target.Position.Y] = Essence.Terrain;
            }

            public void Pass()
            {
                IsCurrent = false;
                CurrentMovePoints = MovePoints;
                Playing.Pop();
            }

            public void Move(Point target, World world)
            {
                if (world.Map[target.X, target.Y] != Essence.Terrain && world.Map[target.X, target.Y] != Essence.Entrance) return;
                while (true)
                {
                    if (Position.X == target.X && Position.Y == target.Y)
                    {
                        world.Map[Position.X, Position.Y] = Essence.Character;
                        return;
                    }
                    world.Map[Position.X, Position.Y] = Essence.Terrain;
                    if(IsFighting)
                        CurrentMovePoints--;
                    var destination = new Point(target.X - Position.X, target.Y - Position.Y);
                    if (destination.X >= 0 && destination.Y >= 0)
                        Position = destination.X >= destination.Y
                            ? new Point(Position.X + 1, Position.Y)
                            : new Point(Position.X, Position.Y + 1);
                    else if (destination.X >= 0 && destination.Y <= 0)
                        Position = destination.X >= Math.Abs(destination.Y)
                            ? new Point(Position.X + 1, Position.Y)
                            : new Point(Position.X, Position.Y - 1);
                    else if (destination.X <= 0 && destination.Y >= 0)
                        Position = Math.Abs(destination.X) >= destination.Y
                            ? new Point(Position.X - 1, Position.Y)
                            : new Point(Position.X, Position.Y + 1);
                    else
                        Position = Math.Abs(destination.X) >= Math.Abs(destination.Y)
                            ? new Point(Position.X - 1, Position.Y)
                            : new Point(Position.X, Position.Y - 1);
                    world.Map[Position.X, Position.Y] = Essence.Character;
                }
            }
        }
    }
}