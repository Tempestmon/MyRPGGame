using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace MyRPGGame
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CreateKnifePickUpUse()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(3,3,3), world) {Position = new Point(0, 0)};
            var knife = new Knife();
            player.PickUpItem(knife);
            player.ChangeWeapon(knife);
            Assert.AreEqual(knife, player.CurrentWeapon);
        }

        [Test]
        public void ChangeHigherWeapon()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 2), world) {Position = new Point(0, 0)};
            var gun = new PlasmaRifle();
            player.PickUpItem(gun);
            player.ChangeWeapon(gun);
            Assert.AreNotEqual(player.CurrentWeapon, gun);
        }
        
        [Test]
        public void ChangeWeapon()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 0)};
            var gun = new PlasmaRifle();
            player.PickUpItem(gun);
            player.ChangeWeapon(gun);
            Assert.AreEqual(player.CurrentWeapon, gun);
        }

        [Test]
        public void CreateMap()
        {
            var world = new World(11, 11);
            Assert.AreEqual(world.Map[0,0], Essence.Terrain);
        }

        [Test]
        public void PlaceCharacter()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 0)};
            Assert.AreEqual(Essence.Character, world.Map[0,0]);
        }

        [Test]
        public void Move()
        {
            var world = new World(11, 11);
            var target = new Point(3, 3);
            var player = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 0)};
            player.Move(target);
            Assert.AreEqual(Essence.Character, world.Map[3,3]);
        }
        
        [Test]
        public void MoveUpperLeft()
        {
            var world = new World(11, 11);
            var target = new Point(0, 0);
            var player = new Character(new Stats(8, 5, 10), world) {Position = new Point(5, 5)};
            player.Move(target);
            Assert.AreEqual(Essence.Character, world.Map[0,0]);
        }
        
        [Test]
        public void MoveLowerLeft()
        {
            var world = new World(11, 11);
            var target = new Point(10, 0);
            var player = new Character(new Stats(8, 5, 10), world) {Position = new Point(5, 5)};
            player.Move(target);
            Assert.AreEqual(Essence.Character, world.Map[10,0]);
        }
        
        [Test]
        public void MoveUpperRight()
        {
            var world = new World(11, 11);
            var target = new Point(0, 10);
            var player = new Character(new Stats(8, 5, 10), world) {Position = new Point(5, 5)};
            player.Move(target);
            Assert.AreEqual(Essence.Character, world.Map[0,10]);
        }
        
        [Test]
        public void MoveLowerRight()
        {
            var world = new World(11, 11);
            var target = new Point(10, 10);
            var player = new Character(new Stats(8, 5, 10), world) {Position = new Point(5, 5)};
            player.Move(target);
            Assert.AreEqual(Essence.Character, world.Map[10,10]);
        }

        [Test]
        public void AttackWithFistClose()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(1, 1)};
            player.Attack(target);
            Assert.AreNotEqual(player.CurrentHealthPoint, target.CurrentHealthPoint);
        }
        
        [Test]
        public void AttackWithFistFar()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 2)};
            player.Attack(target);
            Assert.AreEqual(player.CurrentHealthPoint, target.CurrentHealthPoint);
        }

        [Test]
        public void AttackCheckCapacity()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            player.Attack(target);
            Assert.AreNotEqual(player.CurrentWeapon.CurrentCapacity, player.CurrentWeapon.Capacity); 
        }
        
        [Test]
        public void AttackAndReload()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            player.Attack(target);
            player.Reload();
            Assert.AreEqual(player.CurrentWeapon.CurrentCapacity, player.CurrentWeapon.Capacity); 
        }
        
        [Test]
        public void AttackAndKill()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            player.PickUpItem(new Minigun());
            player.Attack(target);
            player.Attack(target);
            player.Attack(target);
            player.Attack(target);
            Assert.AreEqual(world.Map[0, 1], Essence.Terrain); 
        }

        [Test]
        public void AttackOutOfRangeRevolver()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(9, 9)};
            var revolver = new Revolver();
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Attack(target);
            Assert.AreEqual(target.Statistics.HealthPoint, target.CurrentHealthPoint); 
        }
        
        [Test]
        public void AttackInRangeRevolver()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(4, 3)};
            var revolver = new Revolver();
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Reload();
            player.Attack(target);
            Assert.AreNotEqual(target.Statistics.HealthPoint, target.CurrentHealthPoint); 
        }

        [Test]
        public void AttackWithRevolverRightDamage()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var revolver = new Revolver();
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Reload();
            player.Attack(target);
            Assert.AreEqual(140,target.CurrentHealthPoint); 
        }

        [Test]
        public void AttackWithEmptyCapacity()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var revolver = new Revolver();
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Attack(target);
            Assert.AreEqual(target.Statistics.HealthPoint,target.CurrentHealthPoint); 
        }

        [Test]
        public void AttackCheckEnemyListAttacker()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true)
                {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var revolver = new Revolver();
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Reload();
            player.Attack(target);
            Assert.AreEqual(target, player.Enemies[0]);
        }
        
        [Test]
        public void AttackCheckEnemyListBeingAttacked()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true)
                {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var revolver = new Revolver();
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Reload();
            player.Attack(target);
            Assert.AreEqual(player, target.Enemies[0]);
        }

        [Test]
        public void ChangeWeaponOutInventory()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true)
                {Position = new Point(0, 0)};
            var revolver = new Revolver();
            player.ChangeWeapon(revolver);
            Assert.AreNotEqual(player.CurrentWeapon, revolver);
        }

        [Test]
        public void HealWithBandage()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true)
                {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var revolver = new Revolver();
            var bandage = new Bandage();
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Reload();
            player.Attack(target);
            target.PickUpItem(bandage);
            target.Heal(bandage);
            Assert.AreEqual(150, target.CurrentHealthPoint);
        }

        [Test]
        public void HealFullHealthPoints()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true)
                {Position = new Point(0, 0)};
            var bandage = new Bandage();
            player.PickUpItem(bandage);
            player.Heal(bandage);
            Assert.AreEqual(170, player.CurrentHealthPoint);
        }

        [Test]
        public void LevelUpNoPoints()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10, 1), world, true)
                {Position = new Point(0, 0)};
            player.LevelUp(Tier.Agility);
            Assert.AreEqual(1, player.Statistics.Level);
        }
        
        [Test]
        public void AttackKillGetExp()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 10, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(1, 1, 1), world) {Position = new Point(0, 1)};
            var minigun = new Minigun();
            player.PickUpItem(minigun);
            player.ChangeWeapon(minigun);
            player.Reload();
            player.Attack(target);
            player.Attack(target);
            Assert.AreEqual(50, player.Statistics.ExperiencePoint); 
        }
        
        [Test]
        public void LevelUpWithPoints()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(10, 50, 10), world, true)
                {Position = new Point(0, 0)};
            var minigun = new Minigun();
            var targets = new List<Character>
            {
                new Character(new Stats(), world),
                new Character(new Stats(), world),
                new Character(new Stats(), world),
                new Character(new Stats(), world),
                new Character(new Stats(), world)
            };
            player.PickUpItem(minigun);
            player.ChangeWeapon(minigun);
            foreach (var target in targets)
            {
                player.Reload();
                player.Attack(target);
            }
            player.LevelUp(Tier.Agility);
            Assert.AreEqual(2, player.Statistics.Level);
        }

        [Test]
        public void EnterNewWorld()
        {
            var first = new World(11, 11);
            var second = new World(5, 5);
            var firstEntrance = new Entrance(new Point(10,10), first, second);
            var secondEntrance = new Entrance(new Point(3,0), second, first);
            first.SetEntrance(firstEntrance);
            second.SetEntrance(secondEntrance);
            var player = new Character(new Stats(10, 50, 10), first, true)
                {Position = new Point(0, 0)};
            player.Move(new Point(10, 10));
            Assert.AreEqual(player.CurrentWorld, second);
        }
    }
}