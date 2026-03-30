using System;
using Xunit;
using lab30v5;

namespace lab30v5.Tests
{
    public class MathHelperTests
    {
        private readonly MathHelper _mathHelper;

        public MathHelperTests()
        {
            _mathHelper = new MathHelper();
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(5, true)]
        [InlineData(7, true)]
        [InlineData(11, true)]
        public void IsPrime_PrimeNumbers_ReturnsTrue(int number, bool expected)
        {
            bool result = _mathHelper.IsPrime(number);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(4, false)]
        [InlineData(9, false)]
        [InlineData(15, false)]
        [InlineData(-7, false)]
        public void IsPrime_NonPrimeNumbers_ReturnsFalse(int number, bool expected)
        {
            bool result = _mathHelper.IsPrime(number);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Factorial_OfZero_ReturnsOne()
        {
            long result = _mathHelper.Factorial(0);

            Assert.Equal(1, result);
        }

        [Fact]
        public void Factorial_OfFive_ReturnsOneHundredTwenty()
        {
            long result = _mathHelper.Factorial(5);

            Assert.Equal(120, result);
        }

        [Fact]
        public void Factorial_NegativeNumber_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _mathHelper.Factorial(-3));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(6, 8)]
        [InlineData(10, 55)]
        public void Fibonacci_ValidNumbers_ReturnsCorrectResult(int number, int expected)
        {
            int result = _mathHelper.Fibonacci(number);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Fibonacci_NegativeNumber_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _mathHelper.Fibonacci(-1));
        }

        [Theory]
        [InlineData(12, 18, 6)]
        [InlineData(100, 25, 25)]
        [InlineData(7, 3, 1)]
        [InlineData(-12, 18, 6)]
        [InlineData(0, 9, 9)]
        public void GCD_ValidNumbers_ReturnsCorrectResult(int a, int b, int expected)
        {
            int result = _mathHelper.GCD(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GCD_BothZero_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _mathHelper.GCD(0, 0));
        }

        [Fact]
        public void Factorial_OfOne_ReturnsOne()
        {
            long result = _mathHelper.Factorial(1);

            Assert.Equal(1, result);
        }

        [Fact]
        public void Fibonacci_OfSeven_ReturnsThirteen()
        {
            int result = _mathHelper.Fibonacci(7);

            Assert.Equal(13, result);
        }
    }
}