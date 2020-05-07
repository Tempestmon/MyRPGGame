using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

//Добавить фракции врагов и игрока, связь с врагами и союзниками
namespace MyRPGGame
{
        public class Character
        {
            public Point Position { get; set; }
            public Stats Statistics { get; set; }
            public int CurrentHealthPoint { get; set; }
            public int CurrentMovePoints { get; set; }
            private List<Item> Inventory { get; set; }
            public Weapon CurrentWeapon { get; private set; }
            private bool IsCurrent { get; set; }
            private bool IsPlayer { get; set; }
            public World CurrentWorld { get; set; }
            public List<Character> Enemies { get; set; }

            public Character(Stats stats, World world, bool isPlayer)
            {
                Statistics = stats;
                IsPlayer = isPlayer;
                CurrentHealthPoint = stats.HealthPoint;
                CurrentMovePoints = stats.MovePoints;
                CurrentWeapon = new Fist();
                Inventory = new List<Item> {CurrentWeapon};
                Enemies = new List<Character>();
                CurrentWeapon.CurrentCapacity = CurrentWeapon.Capacity;
                CurrentWorld = world;
                CurrentWorld.Map[Position.X, Position.Y] = Essence.Character;
                CurrentWorld.Characters.Add(this);
            }

            public Character(Stats stats, World world)
            {
                Statistics = stats;
                IsPlayer = false;
                CurrentHealthPoint = stats.HealthPoint;
                CurrentMovePoints = stats.MovePoints;
                CurrentWeapon = new Fist();
                Inventory = new List<Item> {CurrentWeapon};
                Enemies = new List<Character>();
                CurrentWeapon.CurrentCapacity = CurrentWeapon.Capacity;
                CurrentWorld = world;
                CurrentWorld.Map[Position.X, Position.Y] = Essence.Character;
                CurrentWorld.Characters.Add(this);
            }
            
            public void Reload()
            {
                if(CurrentWorld.IsFighting)
                    CurrentMovePoints -= 2;
                CurrentWeapon.CurrentCapacity = CurrentWeapon.Capacity;
            }

            public double GetRange(Character target)
            {
                var differenceX = (target.Position.X - Position.X) * (target.Position.X - Position.X);
                var differenceY = (target.Position.Y - Position.Y) * (target.Position.Y - Position.Y);
                return Math.Sqrt(differenceX + differenceY);
            }

            public void LevelUp(Tier attribute)
            {
                if (Statistics.ExperiencePoint < Statistics.ExpToLevelUp) return;
                Statistics = attribute switch
                {
                    Tier.Agility => new Stats(Statistics.Strength, Statistics.Agility + 1, Statistics.Intelligence,
                        Statistics.Level + 1, Statistics.ExperiencePoint),
                    Tier.Intelligence => new Stats(Statistics.Strength, Statistics.Agility + 1, Statistics.Intelligence++,
                        Statistics.Level + 1, Statistics.ExperiencePoint),
                    Tier.Strength => new Stats(Statistics.Strength++, Statistics.Agility, Statistics.Intelligence,
                        Statistics.Level + 1, Statistics.ExperiencePoint),
                };
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
                if (Inventory.Contains(weapon) && CheckRequirement(weapon))
                    CurrentWeapon = weapon;
            }

            public void PickUpItem(Item item)
            {
                Inventory.Add(item);
            }

            public int GetPointsForKill(Character target)
            {
                if (target.Statistics.Level <= Statistics.Level) return 50;
                var difference = target.Statistics.Level - Statistics.Level;
                return 50 + difference * 50;
            }
            
            public void Attack(Character target)
            {
                if(target.CurrentHealthPoint < 0)
                    return;
                Enemies.Add(target);
                target.Enemies.Add(this);
                if (CurrentMovePoints < CurrentWeapon.AttackPoint
                    || CurrentWeapon.Range * Math.Sqrt(2) < GetRange(target)
                    || CurrentWeapon.CurrentCapacity <= 0) return;
                CurrentWeapon.CurrentCapacity--;
                target.CurrentHealthPoint -= CurrentWeapon.Damage;
                if (IsPlayer && Enemies.Count == 0)
                    CurrentWorld.IsFighting = false;
                if (target.CurrentHealthPoint > 0) return;
                CurrentWorld.Map[target.Position.X, target.Position.Y] = Essence.Terrain;
                Statistics.ExperiencePoint += GetPointsForKill(target);
            }

            public void Heal(HealthItem item)
            {
                if (CurrentHealthPoint >= Statistics.HealthPoint && CurrentMovePoints < item.RequiredMovePoint) return;
                CurrentHealthPoint += item.HealthRestore;
                CurrentMovePoints -= item.RequiredMovePoint;
                if (CurrentHealthPoint > Statistics.HealthPoint)
                    CurrentHealthPoint = Statistics.HealthPoint;
            }

            public void Pass()
            {
                IsCurrent = false;
                CurrentMovePoints = Statistics.MovePoints;
                CurrentWorld.Characters.Add(this);
            }

            public bool CheckEntrance(Point target)
            {
                foreach (var entrance in CurrentWorld.Entrances.Where(entrance => entrance.Position == target && target == Position))
                {
                    foreach (var e in entrance.EnterWorld.Entrances.Where(e => e.EnterWorld == CurrentWorld))
                        Position = e.Position;
                    CurrentWorld = entrance.EnterWorld;
                    return true;
                }

                return false;
            }

            public void Move(Point target)
            {
                if (CurrentWorld.Map[target.X, target.Y] != Essence.Terrain && CurrentWorld.Map[target.X, target.Y] != Essence.Entrance) return;
                while (true)
                {
                    if(CheckEntrance(target))
                        return;
                    if (Position.X == target.X && Position.Y == target.Y)
                    {
                        CurrentWorld.Map[Position.X, Position.Y] = Essence.Character;
                        return;
                    }
                    CurrentWorld.Map[Position.X, Position.Y] = Essence.Terrain;
                    if(CurrentWorld.IsFighting)
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
                    CurrentWorld.Map[Position.X, Position.Y] = Essence.Character;
                }
            }
        }
}