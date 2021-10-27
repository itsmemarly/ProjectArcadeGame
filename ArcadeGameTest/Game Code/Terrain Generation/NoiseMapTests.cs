using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPF_Arcade;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Arcade.Tests
{
    [TestClass()]
    public class NoiseMapTests
    {
        //make one map to run all tests on
        NoiseMap map = new NoiseMap(10, 20, 100, 3, "");

        [TestMethod()]
        public void SetSeedTest()
        {
            String seed = "test";
            String actual;

            map.SetSeed(seed);
            actual = map.Seed();

            Assert.AreEqual(seed, actual);
        }

        [TestMethod()]
        public void HeightTest()
        {
            int actual = map.Height();
            Assert.AreEqual(actual, 10);
        }

        [TestMethod()]
        public void WidthTest()
        {
            int actual = map.Width();
            Assert.AreEqual(actual, 20);
        }

        [TestMethod()]
        public void SeedTest()
        {
            string actual = map.Seed();
            Assert.AreEqual(actual, "");
        }

        [TestMethod()]
        public void MaxValueTest()
        {
            int actual = map.MaxValue();
            Assert.AreEqual(actual, 100);
        }

        [TestMethod()]
        public void PopulateMapTest()
        {
            map.PopulateMap();
            float actual = map.GetNoiseAt(1, 1);
            Assert.IsTrue(actual >= 0 && actual <= map.MaxValue());
        }

        [TestMethod()]
        public void GetSmoothNoiseAtPositionTest()
        {
            float actual = map.GetSmoothNoiseAtPosition(1, 1, 1, map.MaxValue());
            Assert.IsTrue(actual >= 0 && actual <= map.MaxValue());
        }

        [TestMethod()]
        public void GeneratePsuedoRandomValueTest()
        {
            //arrange

            double x = 0;
            double y = 0;
            float maxVal = 100;

            float actual;

            //act
            actual = map.GeneratePsuedoRandomValue(x, y, maxVal);

            //assert
            bool isBiggerThanZero = actual >= 0;
            bool isSmallerThanMaxVal = actual <= maxVal;
            Assert.IsTrue(isBiggerThanZero && isSmallerThanMaxVal);
        }
    }
}