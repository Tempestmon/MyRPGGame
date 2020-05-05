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
            var world = new GameModel.World(11, 11);
            var player = new GameModel.Character(new GameModel.Stats(3,3,3), world) {Position = new Point(0, 0)};
            var knife = new Knife();
            player.PickUpWeapon(knife);
            player.ChangeWeapon(knife);
            Assert.AreEqual(knife, player.CurrentWeapon);
        }

        [Test]
        public void ChangeHigherWeapon()
        {
            var world = new GameModel.World(11, 11);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 2), world) {Position = new Point(0, 0)};
            var gun = new PlasmaRifle();
            player.PickUpWeapon(gun);
            player.ChangeWeapon(gun);
            Assert.AreNotEqual(player.CurrentWeapon, gun);
        }
        
        [Test]
        public void ChangeWeapon()
        {
            var world = new GameModel.World(11, 11);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(0, 0)};
            var gun = new PlasmaRifle();
            player.PickUpWeapon(gun);
            player.ChangeWeapon(gun);
            Assert.AreEqual(player.CurrentWeapon, gun);
        }

        [Test]
        public void CreateMap()
        {
            var world = new GameModel.World(11, 11);
            Assert.AreEqual(world.Map[0,0], GameModel.Essence.Terrain);
        }

        [Test]
        public void PlaceCharacter()
        {
            var world = new GameModel.World(11, 11);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(0, 0)};
            Assert.AreEqual(GameModel.Essence.Character, world.Map[0,0]);
        }

        [Test]
        public void Move()
        {
            var world = new GameModel.World(11, 11);
            var target = new Point(3, 3);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(0, 0)};
            player.Move(target, world);
            Assert.AreEqual(GameModel.Essence.Character, world.Map[3,3]);
        }
        
        [Test]
        public void MoveUpperLeft()
        {
            var world = new GameModel.World(11, 11);
            var target = new Point(0, 0);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(5, 5)};
            player.Move(target, world);
            Assert.AreEqual(GameModel.Essence.Character, world.Map[0,0]);
        }
        
        [Test]
        public void MoveLowerLeft()
        {
            var world = new GameModel.World(11, 11);
            var target = new Point(10, 0);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(5, 5)};
            player.Move(target, world);
            Assert.AreEqual(GameModel.Essence.Character, world.Map[10,0]);
        }
        
        [Test]
        public void MoveUpperRight()
        {
            var world = new GameModel.World(11, 11);
            var target = new Point(0, 10);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(5, 5)};
            player.Move(target, world);
            Assert.AreEqual(GameModel.Essence.Character, world.Map[0,10]);
        }
        
        [Test]
        public void MoveLowerRight()
        {
            var world = new GameModel.World(11, 11);
            var target = new Point(10, 10);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(5, 5)};
            player.Move(target, world);
            Assert.AreEqual(GameModel.Essence.Character, world.Map[10,10]);
        }

        [Test]
        public void AttackWithFistClose()
        {
            var world = new GameModel.World(11, 11);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(1, 1)};
            player.Attack(target, world);
            Assert.AreNotEqual(player.CurrentHealthPoint, target.CurrentHealthPoint);
        }
        
        [Test]
        public void AttackWithFistFar()
        {
            var world = new GameModel.World(11, 11);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(0, 2)};
            player.Attack(target, world);
            Assert.AreEqual(player.CurrentHealthPoint, target.CurrentHealthPoint);
        }

        [Test]
        public void AttackCheckCapacity()
        {
            var world = new GameModel.World(11, 11);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            player.Attack(target, world);
            Assert.AreNotEqual(player.CurrentWeapon.CurrentCapacity, player.CurrentWeapon.Capacity); 
        }
        
        [Test]
        public void AttackAndReload()
        {
            var world = new GameModel.World(11, 11);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            player.Attack(target, world);
            player.Reload();
            Assert.AreEqual(player.CurrentWeapon.CurrentCapacity, player.CurrentWeapon.Capacity); 
        }
        
        [Test]
        public void AttackAndKill()
        {
            var world = new GameModel.World(11, 11);
            var player = new GameModel.Character(new GameModel.Stats(8, 5, 10), world, true) {Position = new Point(0, 0)};
            var target = new GameModel.Character(new GameModel.Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            player.PickUpWeapon(new Minigun());
            player.Attack(target, world);
            player.Attack(target, world);
            player.Attack(target, world);
            player.Attack(target, world);
            Assert.AreEqual(world.Map[0, 1], GameModel.Essence.Terrain); 
        }
    }
}