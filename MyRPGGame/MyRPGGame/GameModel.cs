/*
 * Создать очерёдность хода, изменить методы
 * Создать вход
 * Создать ИИ
 * Добавить очки опыта
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

namespace MyRPGGame
{
    public partial class GameModel
    {
        public GameModel()
        {
            var world = new World(10, 10) {IsFighting = false};
        }
        
        public class World
        {
            public bool IsFighting { get; set; }
            public Stack<Character> Active { get; set; }
            public Essence[,] Map;

            public World(int width, int height)
            {
                Map = new Essence[width,height];
                for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                    Map[i, j] = Essence.Terrain;
                Active = new Stack<Character>();
                IsFighting = false;
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
                ExperiencePoint = Level * 30;
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
                ExperiencePoint = Level * 30;
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
                ExperiencePoint = Level * 30;
                ExpToLevelUp = 25 * (4 * (Level + 1) + 2) * Level;
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
    }
}