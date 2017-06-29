using FlooringMastery.BLL.Factories;
using FlooringMastery.BLL.Managers;
using FlooringMastery.Models;
using FlooringMastery.Models.Models.Responses;
using NUnit.Framework;
using System;

namespace FlooringMastery.Tests
{
    [TestFixture]
    public class FlooringMasteryTests
    {
        [TestCase(767, false)]
        [TestCase(2, true)]
        public void OrderNumberCheck(int orderNumber, bool expectedResult)
        {
            OrderManager orderManager = OrderManagerFactory.Create();
            DisplaySingleOrderResponse response = orderManager.DisplayOrder(new DateTime(2000, 1, 1), orderNumber);
            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("MN", false)]
        [TestCase("", false)]
        [TestCase("99", false)]
        [TestCase("Mexico", false)]
        [TestCase("OH", true)]
        [TestCase("IN", true)]
        public void StateCheck(string state, bool expectedResult)
        {
            TaxManager taxManager = TaxManagerFactory.Create();
            Tax tax = taxManager.TaxByState(state);
        }

        [Test]
        public void OrderCheck()
        {
            OrderManager manager = OrderManagerFactory.Create();

            DisplaySingleOrderResponse response = manager.DisplayOrder(new DateTime(2000, 1, 1), 2);

            Assert.IsNotNull(response.OrderDetails);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(2, response.OrderDetails.OrderNumber);
            Assert.AreEqual("Big Homes", response.OrderDetails.CustomerName);
            Assert.AreEqual("OH", response.OrderDetails.State);
            Assert.AreEqual(6.25, response.OrderDetails.TaxRate);
            Assert.AreEqual("Tile", response.OrderDetails.ProductType);
            Assert.AreEqual(500, response.OrderDetails.Area);
            Assert.AreEqual(3.50, response.OrderDetails.CostPerSquareFoot);
            Assert.AreEqual(4.15, response.OrderDetails.LaborCostPerSquareFoot);
            Assert.AreEqual(1750.00, response.OrderDetails.MaterialCost);
            Assert.AreEqual(2075.00, response.OrderDetails.LaborCost);
            Assert.AreEqual("239.06", response.OrderDetails.Tax.ToString("00.00"));
            Assert.AreEqual("4064.06", response.OrderDetails.Total.ToString("00.00"));
        }
    }
}