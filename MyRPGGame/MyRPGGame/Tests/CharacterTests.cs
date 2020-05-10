using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace MyRPGGame.Tests
{
    public class CharacterTests
    {
        [Test]
        public void CreateKnifePickUpUse()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(3,3,3), world) {Position = new Point(0, 0)};
            var knife = new Knife();
            world.PlaceItem(knife, player.Position);
            player.PickUpItem(knife);
            player.ChangeWeapon(knife);
            Assert.AreEqual(knife, player.CurrentWeapon);
        }

        [Test]
        public void PickUpTwiceSameItem()
        {
            var world = new World(11, 11);
            var item = new Item();
            var player = new Character(new Stats(3,3,3), world) {Position = new Point(0, 0)};
            world.PlaceItem(item, player.Position);
            player.PickUpItem(item);
            player.PickUpItem(item);
            Assert.AreEqual(1, player.Inventory[1].Count);
        }
        
        [Test]
        public void PickUpItemsCheckCount()
        {
            var world = new World(11, 11);
            var item = new Revolver();
            var player = new Character(new Stats(3,3,3), world) {Position = new Point(0, 0)};
            world.PlaceItem(item, player.Position);
            player.PickUpItem(item);
            world.PlaceItem(item, new Point(1, 0));
            player.Move(new Point(1,0));
            player.PickUpItem(item);
            world.PlaceItem(item, new Point(2, 0));
            player.Move(new Point(2,0));
            player.PickUpItem(item);
            Assert.AreEqual(3, player.Inventory[1].Count);
        }

        [Test]
        public void ChangeHigherWeapon()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 2), world) {Position = new Point(0, 0)};
            var gun = new PlasmaRifle();
            world.PlaceItem(gun, player.Position);
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
            world.PlaceItem(gun, player.Position);
            player.PickUpItem(gun);
            player.ChangeWeapon(gun);
            Assert.AreEqual(player.CurrentWeapon, gun);
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
        public void AttackAndKill()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var minigun = new Minigun();
            world.PlaceItem(minigun, player.Position);
            player.PickUpItem(minigun);
            player.Attack(target);
            player.Attack(target);
            player.Attack(target);
            player.Attack(target);
            Assert.AreEqual(world.Map[0, 1], Essence.Terrain); 
        }
        
        [Test]
        public void AttackWithEmptyCapacity()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var revolver = new Revolver();
            world.PlaceItem(revolver, player.Position);
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
            world.PlaceItem(revolver, player.Position);
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
            world.PlaceItem(revolver, player.Position);
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
            world.PlaceItem(minigun, player.Position);
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
            world.PlaceItem(minigun, player.Position);
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
        
        /*[Test]
        public void AIFight()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(2,3,2), world, true)
                {Position = new Point(0,0)};
            var knife = new Knife();
            player.Inventory.Add(knife);
            player.Reload();
            var fEnemy = new Character(new Stats(2,2,2), world, true)
                {Position = new Point(3,3)};
            var sEnemy = new Character(new Stats(2,2,2), world, true)
                {Position = new Point(5,5)};
            fEnemy.AddFriend(sEnemy);
            sEnemy.AddFriend(fEnemy);
            player.Move(fEnemy);
            player.Attack(fEnemy);
            player.Attack(fEnemy);
            player.Attack(fEnemy);
        }*/
    }
}