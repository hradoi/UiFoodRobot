using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrawlerLibrary;

namespace CrawlerUnitTest
{
    [TestClass]
    public class FoodRobotTest
    {
        YellowFoodConstructor x = new YellowFoodConstructor();

        [TestMethod]
        public void Update()
        {
            x.UpdateMenu();
            Assert.IsTrue(true);
        }
    }
}
