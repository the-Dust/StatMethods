using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatMethods.MedianFilters;
using StatMethods.Winsorizators;

namespace UnitTests
{
    [TestClass]
    public class StatTests
    {
        [TestMethod]
        public void CanGetValidWinsorization()
        {
            //Arrange
            Winsorizator wins = new Winsorizator();

            double[] input1 = { 2, 3, 17, 6, 1 };
            int m1 = 2;
            double reference1 = 5.8;

            double[] input2 = { 17, 38, 9, 63, 41, 15 };
            int m2 = 4;
            double reference2 = 31.75;

            double[] input3 = { 7, 82, 33, 33, 33, 1, 56 };
            int m3 = 2;
            double reference3 = 35;

            //Action
            double result1 = wins.FindWinsorizedMedium(input1, m1);

            double result2 = wins.FindWinsorizedMedium(input2, m2);

            double result3 = wins.FindWinsorizedMedium(input3, m3);

            //Assert
            Assert.AreEqual(reference1, result1, 0.0001);
            Assert.AreEqual(reference2, result2, 0.0001);
            Assert.AreEqual(reference3, result3, 0.0001);
        }

        [TestMethod]
        public void CanGetValidMedianFiltration()
        {
            //Arrange
            MedianFilter filter = new MedianFilter();

            double[] input1 = { 2, 80, 6, 3 };
            int window1 = 3;
            double[] reference1 = { 2,6,6,3};

            double[] input2 = { 17, 38, 9, 63, 41, 15 };
            int window2 = 4;
            double[] reference2 = { 17, 17, 27.5, 39.5, 28, 28 };

            double[] input3 = { 7, 100, 1, 33, 12, 1, 56 };
            int window3 = 5;
            double[] reference3 = { 7, 7, 12, 12, 12, 33, 56 };

            //Action
            double[] result1 = filter.GetMedianFiltration(input1, window1);

            double[] result2 = filter.GetMedianFiltration(input2, window2);

            double[] result3 = filter.GetMedianFiltration(input3, window3);

            //Assert
            CollectionAssert.AreEqual(reference1, result1);
            CollectionAssert.AreEqual(reference2, result2);
            CollectionAssert.AreEqual(reference3, result3);
        }
    }
}
