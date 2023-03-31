using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpidersFromMarsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWallCreate()
        {
            SpidersFromMars.Wall wall = new SpidersFromMars.Wall("5 17");
            Assert.AreEqual(wall.left, 0);
            Assert.AreEqual(wall.bottom, 0);
            Assert.AreEqual(wall.right, 5);
            Assert.AreEqual(wall.top, 17);
        }

        [TestMethod]
        public void TestSpiderCreate()
        {
            SpidersFromMars.Wall wall = new SpidersFromMars.Wall("5 17");
            SpidersFromMars.Spider spider = new SpidersFromMars.Spider("4 10 Left", wall);
            Assert.AreEqual(spider.xPos, 4);
            Assert.AreEqual(spider.yPos, 10);
            Assert.AreEqual(spider.SpiderPositionDetails(), "4 10 Left");
        }

        [TestMethod]
        public void TestFailWallCreate()
        {
            SpidersFromMars.Wall wall = new SpidersFromMars.Wall("5 0");
            Assert.AreEqual(wall.left, 0);
            Assert.AreEqual(wall.bottom, 0);
            Assert.AreEqual(wall.right, 0);
            Assert.AreEqual(wall.top, 0);
            Assert.AreEqual(SpidersFromMars.Wall.ValidateWallSize("5 0"), "Must enter 2 positive integers for wall size!");
        }

        [TestMethod]
        public void TestFailSpiderCreate()
        {
            SpidersFromMars.Wall wall = new SpidersFromMars.Wall("5 17");
            SpidersFromMars.Spider spider = new SpidersFromMars.Spider("4 20 Left", wall);
            Assert.AreEqual(spider.xPos, 0);
            Assert.AreEqual(spider.yPos, 0);
            Assert.AreEqual(spider.SpiderPositionDetails(), "0 0 Up");
            Assert.AreEqual(SpidersFromMars.Spider.CheckSpiderPosition("4 20 Left", wall), "Invalid Y coordinate 20 in input - must be within wall bottom/top 0/17");
        }

        [TestMethod]
        public void TestInputSeqeunce()
        {
            List<string> inputLines = new List<string>() { "7 15", "4 10 Left", "FLFLFRFFLF" };
            SpidersFromMars.Wall wall = new SpidersFromMars.Wall(inputLines[0]);
            SpidersFromMars.Spider spider = new SpidersFromMars.Spider(inputLines[1], wall);
            Assert.AreEqual(spider.SpiderPositionDetails(), "4 10 Left");
            spider.RunAllCommands(inputLines[2]);
            Assert.AreEqual(spider.SpiderPositionDetails(), "5 7 Right");
        }
    }
}
