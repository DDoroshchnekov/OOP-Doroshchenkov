using System;
using Lab24;
using Lab24.Strategies;
using Xunit;

namespace Lab24.Tests
{
    public class NumericProcessorTests
    {
        [Fact]
        public void Process_UsesCurrentStrategy_Square()
        {
            var processor = new NumericProcessor(new SquareOperationStrategy());

            var result = processor.Process(6);

            Assert.Equal(36, result);
            Assert.Equal("Square", processor.CurrentOperationName);
        }

        [Fact]
        public void SetStrategy_ChangesAlgorithm_Cube()
        {
            var processor = new NumericProcessor(new SquareOperationStrategy());
            processor.SetStrategy(new CubeOperationStrategy());

            var result = processor.Process(3);

            Assert.Equal(27, result);
            Assert.Equal("Cube", processor.CurrentOperationName);
        }

        [Fact]
        public void SquareRoot_Negative_Throws()
        {
            var processor = new NumericProcessor(new SquareRootOperationStrategy());

            Assert.Throws<ArgumentOutOfRangeException>(() => processor.Process(-1));
        }
    }
}