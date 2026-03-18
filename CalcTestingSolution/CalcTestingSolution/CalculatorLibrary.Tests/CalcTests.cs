using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLibrary;
using System;

namespace CalculatorLibrary.Tests
{
    [TestClass]
    public class CalcTests
    {
        private Calculator calc=null!;

        [TestInitialize]
        public void Setup()
        {
            calc = new Calculator();
        }

        [TestMethod]
        public void Add_ReturnsCorrectSum()
        {
            double result = calc.Add(5, 3);
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void Subtract_ReturnsCorrectDifference()
        {
            double result = calc.Subtract(10, 4);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Multiply_ReturnsCorrectProduct()
        {
            double result = calc.Multiply(2, 5);
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Divide_ReturnsCorrectResult()
        {
            double result = calc.Divide(20, 4);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Divide_ByZero_ThrowsException()
        {
            try
            {
                calc.Divide(10, 0);
                Assert.Fail("Expected DividedByZeroException was not thrown.");
            }
            catch(DivideByZeroException)
            {

            }
           
        }
    }
}