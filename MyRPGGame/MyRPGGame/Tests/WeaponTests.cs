using System.Drawing;
using NUnit.Framework;

namespace MyRPGGame.Tests
{
    public class WeaponTests
    {
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
            world.PlaceItem(revolver, player.Position);
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
            world.PlaceItem(revolver, player.Position);
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Reload();
            player.Attack(target);
            Assert.AreEqual(140,target.CurrentHealthPoint); 
        }
    }
}