using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace MyRPGGame.Tests
{
    public class WorldTests
    {
        [Test]
        public void CreateMap()
        {
            var world = new World(11, 11);
            Assert.AreEqual(world.Map[10, 10], Essence.Terrain);
        }
        
        [Test]
        public void PlaceCharacter()
        {
            var world = new World(11, 11);
            var player = new Character(new Stats(8, 5, 10), world) {Position = new Point(0, 0)};
            Assert.AreEqual(Essence.Character, world.Map[0,0]);
        }
        
        [Test]
        public void EnterNewWorld()
        {
            var first = new World(11, 11);
            var second = new World(5, 5);
            var firstEntrance = new Entrance(new Point(0,1), first, second);
            var secondEntrance = new Entrance(new Point(3,0), second, first);
            first.SetEntrance(firstEntrance);
            second.SetEntrance(secondEntrance);
            var player = new Character(new Stats(10, 50, 10), first, true)
                {Position = new Point(0, 0)};
            player.Move(new Point(0, 1));
            Assert.AreEqual(second, player.CurrentWorld);
        }
        
        [Test]
        public void EnterNewWorldRightCoordinates()
        {
            var first = new World(11, 11);
            var second = new World(5, 5);
            var firstEntrance = new Entrance(new Point(0,1), first, second);
            var secondEntrance = new Entrance(new Point(3,0), second, first);
            first.SetEntrance(firstEntrance);
            second.SetEntrance(secondEntrance);
            var player = new Character(new Stats(10, 50, 10), first, true)
                {Position = new Point(0, 0)};
            player.Move(new Point(0, 1));
            Assert.AreEqual(new Point(3,0), player.Position);
        }
        
        [Test]
        public void EnterNewWorldPlaceEntranceOldWorld()
        {
            var first = new World(11, 11);
            var second = new World(5, 5);
            var firstEntrance = new Entrance(new Point(0,1), first, second);
            var secondEntrance = new Entrance(new Point(3,0), second, first);
            first.SetEntrance(firstEntrance);
            second.SetEntrance(secondEntrance);
            var player = new Character(new Stats(10, 50, 10), first, true)
                {Position = new Point(0, 0)};
            player.Move(new Point(0, 1));
            Assert.AreEqual(Essence.Entrance, first.Map[0, 1]);
        }
        
        [Test]
        public void SetEntranceCheckEssence()
        {
            var first = new World(11, 11);
            var second = new World(5, 5);
            var firstEntrance = new Entrance(new Point(0,1), first, second);
            first.SetEntrance(firstEntrance);
            Assert.AreEqual(first.Map[0, 1], Essence.Entrance);
        }

        [Test]
        public void PlaceItemOnGround()
        {
            var world = new World(11, 11);
            var knife = new Knife();
            world.PlaceItem(knife, new Point(0,0));
            Assert.AreEqual(knife, world.GroundItem[new Point(0,0)][0]);
        }
        
        [Test]
        public void PlaceItemsOnGround()
        {
            var world = new World(11, 11);
            var items = new List<Item> {new Bandage(), new Rifle()};
            world.PlaceItem(items, new Point(0,0));
            Assert.AreEqual(items, world.GroundItem[new Point(0,0)]);
        }

        [Test]
        public void PlaceDifferentItemsCheckCount()
        {
            var world = new World(11, 11);
            var fKnife = new Knife();
            var sKnife = new Knife();
            fKnife.Count+=2;
            sKnife.Count++;
            world.PlaceItem(fKnife, new Point(0,0));
            world.PlaceItem(sKnife, new Point(0,0));
            Assert.AreEqual(5, world.GroundItem[new Point(0,0)][0].Count);
        }
        
        [Test]
        public void PlaceDifferentItemsDifferentPlacesCheckCount()
        {
            var world = new World(11, 11);
            var fKnife = new Knife();
            var sKnife = new Knife();
            var tKnife = new Knife();
            fKnife.Count+=2;
            sKnife.Count++;
            world.PlaceItem(fKnife, new Point(0,0));
            world.PlaceItem(tKnife, new Point(1, 0));
            world.PlaceItem(sKnife, new Point(0,0));
            Assert.AreEqual(5, world.GroundItem[new Point(0,0)][0].Count);
        }
        
        [Test]
        public void PlaceDifferentSetOfItemsOnGround()
        {
            var world = new World(11, 11);
            var items = new List<Item>();
            var knife = new Knife();
            knife.Count++;
            var bandage = new Bandage();
            bandage.Count+=4;
            items.Add(knife);
            items.Add(bandage);
            world.PlaceItem(items, new Point(0,0));
            var drop = new List<Item>();
            var sKnife = new Knife();
            var sBandage = new Bandage();
            sBandage.Count++;
            var rifle = new Rifle();
            drop.Add(sKnife);
            drop.Add(sBandage);
            drop.Add(rifle);
            world.PlaceItem(drop, new Point(0,0));
            var expected = new List<Item> {new Knife {Count = 3}, new Bandage {Count = 7}, new Rifle()};
            var actual = world.GroundItem[new Point(0, 0)];
            Assert.IsTrue(expected.Count == actual.Count
            && expected[0].Count == actual[0].Count
            && expected[1].Count == actual[1].Count
            && expected[2].Count == actual[2].Count);
        }

        
    }
}