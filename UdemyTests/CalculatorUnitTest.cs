using NUnit.Framework;
using WfaUdemy.lib;

namespace UdemyTests
{
    public class CalculatorUnitTest
    {
        private Calculator _calculator;


        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Somar()
        {
            int result = _calculator.Sum(2, 2);
            int correctResult = 4;

            Assert.AreEqual(correctResult, result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Subtrair()
        {
            int result = _calculator.Subtract(2, 2);
            int correctResult = 0;
            
            Assert.AreEqual(correctResult, result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Multiply()
        {
            int result = _calculator.Multiply(2, 3);
            int correctResult = 6;

            Assert.AreEqual(correctResult, result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Divide()
        {
            int result = _calculator.Divide(2, 2);
            int correctResult = 1;

            Assert.AreEqual(correctResult, result);
            Assert.IsNotNull(result);
        }

    }
}