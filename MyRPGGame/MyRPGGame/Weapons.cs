﻿using static MyRPGGame.GameModel;

namespace MyRPGGame
{
    public class Fist : Weapon
    {
        public Fist()
        {
            Damage = 24;
            Range = 1;
            Capacity = 1000;
            WeaponRequirement = new WeaponRequirement(Tier.Strength, 2);
            AttackPoint = 2;
        }
    }

    public class Hammer : Weapon
    {
        public Hammer()
        {
            Damage = 24;
            Range = 1;
            Capacity = 30;
            WeaponRequirement = new WeaponRequirement(Tier.Strength, 3);
            AttackPoint = 4;
        }
    }

    public class Knife : Weapon
    {
        public Knife()
        {
            Damage = 10;
            Range = 1;
            Capacity = 20;
            WeaponRequirement = new WeaponRequirement(Tier.Agility, 3);
            AttackPoint = 2;
        }
    }

    public class SuperFist : Weapon
    {
        public SuperFist()
        {
            Damage = 18;
            Range = 1;
            Capacity = 4;
            WeaponRequirement = new WeaponRequirement(Tier.Intelligence, 4);
            AttackPoint = 3;
        }
    }

    public class SawedOff : Weapon
    {
        public SawedOff()
        {
            Damage = 50;
            Range = 3;
            Capacity = 5;
            WeaponRequirement = new WeaponRequirement(Tier.Strength, 5);
            AttackPoint = 3;
        }
    }

    public class Revolver : Weapon
    {
        public Revolver()
        {
            Damage = 30;
            Range = 5;
            Capacity = 6;
            WeaponRequirement = new WeaponRequirement(Tier.Agility, 5);
            AttackPoint = 2;
        }
    }

    public class PlasmaHandGun : Weapon
    {
        public PlasmaHandGun()
        {
            Damage = 40;
            Range = 4;
            Capacity = 12;
            WeaponRequirement = new WeaponRequirement(Tier.Intelligence, 6);
            AttackPoint = 3;
        }
    }

    public class Minigun : Weapon
    {
        public Minigun()
        {
            Damage = 100;
            Range = 8;
            Capacity = 2;
            WeaponRequirement = new WeaponRequirement(Tier.Strength, 7);
            AttackPoint = 5;
        }
    }

    public class Rifle : Weapon
    {
        public Rifle()
        {
            Damage = 60;
            Range = 10;
            Capacity = 15;
            WeaponRequirement = new WeaponRequirement(Tier.Agility, 6);
            AttackPoint = 4;
        }
    }

    public class PlasmaRifle : Weapon
    {
        public PlasmaRifle()
        {
            Damage = 70;
            Range = 6;
            Capacity = 8;
            WeaponRequirement = new WeaponRequirement(Tier.Intelligence, 8);
            AttackPoint = 4;
        }
    }
}