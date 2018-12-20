using FluentAssertions;
using NUnit.Framework;

namespace CRC.Tests
{
    [TestFixture]
    class CyclicRedundancyCheckManagerTests
    {
        private ICyclicRedundancyCheckManager _cyclicRedundancyCheckManager;

        [SetUp]
        public void SetUp()
        {
            _cyclicRedundancyCheckManager = new CyclicRedundancyCheckManager();
        }

        [TestCase(new[] { 1, 0, 1, 1, 0 }, new[] { 1, 0, 1 }, new[] { 1, 0, 1, 1, 0, 1, 0 })]
        [TestCase(new[] { 1, 1, 1, 1, 0, 1 }, new[] { 1, 0, 1 }, new[] { 1, 1, 1, 1, 0, 1, 0, 1 })]
        public void AppendRedundantBits_ShouldAppendCorrectBits(int[] original, int[] generator, int[] expected)
        {
            var originalBinaryPolinomial = new BinaryPolinomial(original);
            var generatorBinaryPolinomial = new BinaryPolinomial(generator);
            var expectedBinaryPolinomial = new BinaryPolinomial(expected);

            _cyclicRedundancyCheckManager.AppendRedundantBits(originalBinaryPolinomial, generatorBinaryPolinomial)
                .Should().BeEquivalentTo(expectedBinaryPolinomial);
        }

        [TestCase(new[] { 1, 0, 1, 1, 0 }, new[] { 1, 0, 1 }, new[] { 1, 0, 1, 1, 0, 1, 0 })]
        [TestCase(new[] { 1, 1, 1, 1, 0, 1 }, new[] { 1, 0, 1 }, new[] { 1, 1, 1, 1, 0, 1, 0, 1 })]
        public void IsUnchanged_WithNoChange_ShouldReturnTrue(int[] original, int[] generator, int[] expected)
        {
            var generatorPolynomial = new BinaryPolinomial(generator);
            var polynomialWithFcs = new BinaryPolinomial(expected);
            _cyclicRedundancyCheckManager.IsUnchanged(polynomialWithFcs, generatorPolynomial).Should().BeTrue();
        }

        
    }
}
