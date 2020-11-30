using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Client;
using FakeItEasy;

namespace Unit_Tests
{
    [TestClass]
    public class TestClient
    {
        [TestMethod]
        public void TestInputCheck()
        {
            //Arrange
            string[] requests = { "+", "-", "/", "*", "0", "-123", "145.98"};
            bool expected = true;
            IInput input = new Input();

            //Act
            bool actual = input.CheckInput(requests);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInputCheckFalse()
        {
            //Arrange
            string[] requests = { "+", "-", "/", "*", "0", "-123", "145.98", "Yarin" };
            bool expected = false;
            IInput input = new Input();

            //Act
            bool actual = input.CheckInput(requests);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
