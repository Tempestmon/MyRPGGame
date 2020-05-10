using System.Drawing;
using NUnit.Framework;

namespace MyRPGGame.Tests
{
    public class HealingTest
    {
        [Test]
        public void HealWithBandage()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true)
                {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var revolver = new Revolver();
            var bandage = new Bandage();
            world.PlaceItem(revolver, player.Position);
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Reload();
            player.Attack(target);
            world.PlaceItem(bandage, target.Position);
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
            world.PlaceItem(bandage, player.Position);
            player.PickUpItem(bandage);
            player.Heal(bandage);
            Assert.AreEqual(170, player.CurrentHealthPoint);
        }

        [Test]
        public void HealCheckHealingItemDisappear()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true)
                {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var revolver = new Revolver();
            var bandage = new Bandage();
            world.PlaceItem(revolver, player.Position);
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Reload();
            player.Attack(target);
            world.PlaceItem(bandage, target.Position);
            target.PickUpItem(bandage);
            target.Heal(bandage);
            Assert.AreEqual(1, target.Inventory.Count);
        }

        [Test]
        public void HealWithoutHealingItem()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true)
                {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var revolver = new Revolver();
            var bandage = new Bandage();
            world.PlaceItem(revolver, player.Position);
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Reload();
            player.Attack(target);
            world.PlaceItem(bandage, target.Position);
            target.Heal(bandage);
            Assert.AreNotEqual(player.CurrentHealthPoint, target.CurrentHealthPoint);
        }
        
        [Test]
        public void HealCheckHealingItemCount()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world, true)
                {Position = new Point(0, 0)};
            var target = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 1)};
            var revolver = new Revolver();
            var bandage = new Bandage();
            var anotherBandage = new Bandage();
            world.PlaceItem(revolver, player.Position);
            player.PickUpItem(revolver);
            player.ChangeWeapon(revolver);
            player.Reload();
            player.Attack(target);
            world.PlaceItem(bandage, target.Position);
            target.PickUpItem(bandage);
            world.PlaceItem(anotherBandage, target.Position);
            target.PickUpItem(anotherBandage);
            target.Heal(bandage);
            Assert.AreEqual(2, target.Inventory.Count);
        }
    }
}