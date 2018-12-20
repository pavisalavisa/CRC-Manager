using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace CRC.Tests
{
    [TestFixture]
    public class BinaryPolinomialTests
    {
        [Test]
        public void Append_ShouldAppendBits()
        {
            var originalPolynomial = new BinaryPolynomial(1, 0, 1, 1, 1, 0);
            var polynomialToAppend = new BinaryPolynomial(1, 0, 1);
            originalPolynomial.Append(polynomialToAppend);

        }

        [TestCase(new[] { 0, 1, 1, 0 }, 2)]
        [TestCase(new[] { 1, 0, 1, 1, 0 }, 4)]
        [TestCase(new[] { 1, 1, 0, 1, 1, 0 }, 5)]
        public void BinaryPolynomial_FromParams_ShouldHaveCorrectDegree(int[] polynomialValues, int degree)
        {
            var polynomial = new BinaryPolynomial(polynomialValues);

            polynomial.Degree.Should().Be(degree);
        }

        [TestCase(new[] { 0, 1, 1, 0 })]
        public void BinaryPolynomial_FromParams_ShouldHaveUniqueItems(int[] polynomialValues)
        {
            var polynomial = new BinaryPolynomial(polynomialValues);

            polynomial.Value.Should().OnlyHaveUniqueItems();
        }

        [TestCase(new[] { 0, 1, 1, 0 })]
        public void BinaryPolynomial_FromParams_ShouldItemsInDescendingOrder(int[] polynomialValues)
        {
            var polynomial = new BinaryPolynomial(polynomialValues);

            polynomial.Value.Should().BeInDescendingOrder();
        }

        [TestCase(new[] { 0, 1, 1, 0 })]
        public void BinaryPolynomialFromParams_ShouldHaveCorrectValues(int[] polynomialValues)
        {
            var polynomial = new BinaryPolynomial(polynomialValues);
            var expectedPolynomial = new List<BinaryPolynomialMember>()
            {
                new BinaryPolynomialMember(3, 0),
                new BinaryPolynomialMember(2, 1),
                new BinaryPolynomialMember(1, 1),
                new BinaryPolynomialMember(0, 0)
            };

            polynomial.Value.Should().BeEquivalentTo(expectedPolynomial);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(0)]
        public void LeftShift_ShouldIncreaseDegree(int steps)
        {
            var originalPolynomial = new BinaryPolynomial(1, 0, 1, 1);
            originalPolynomial.LeftShift(steps);

            originalPolynomial.Degree.Should().Be(3 + steps);
        }

        [Test]
        public void LeftShift_ShouldMoveBits()
        {
            var originalPolynomial = new BinaryPolynomial(1, 0, 1, 1);
            var expectedPolynomial = new BinaryPolynomial(1, 0, 1, 1, 0, 0);
            originalPolynomial.LeftShift(2);

            originalPolynomial.Should().BeEquivalentTo(expectedPolynomial);
        }

        [TestCase(new[] { 1, 0, 1 }, 6)]
        [TestCase(new[] { 0, 1, 0, 1 }, 6)]
        [TestCase(new[] { 1 }, 4)]
        public void Append_ShouldIncreaseDegree(int[] polynomialToAppendValues, int expectedDegree)
        {
            var originalPolynomial = new BinaryPolynomial(1, 0, 1, 1);
            var polynomialToAppend = new BinaryPolynomial(polynomialToAppendValues);

            originalPolynomial.Append(polynomialToAppend);

            originalPolynomial.Degree.Should().Be(expectedDegree);
        }

        [TestCase(new[] { 1, 0, 1 }, new[] { 1, 0, 1, 1, 1, 0, 1 })]
        [TestCase(new[] { 1, 0, 1, 0, 1 }, new[] { 1, 0, 1, 1, 1, 0, 1, 0, 1 })]
        [TestCase(new[] { 0, 1 }, new[] { 1, 0, 1, 0, 1 })]
        public void Append_ShouldAppendPolynomial(int[] polynomialToAppendValues, int[] expectedPolynomialValues)
        {
            var originalPolynomial = new BinaryPolynomial(1, 0, 1, 1);
            var polynomialToAppend = new BinaryPolynomial(polynomialToAppendValues);
            var expectedPolynomial = new BinaryPolynomial(expectedPolynomialValues);

            originalPolynomial.Append(polynomialToAppend);

            originalPolynomial.Should().BeEquivalentTo(expectedPolynomial);
        }

        [TestCase(new[] { 1, 0, 1, 1 }, new[] { 1, 1, 1 })]
        [TestCase(new[] { 1, 0, 1 }, new[] { 1, 0 })]
        [TestCase(new[] { 1, 0 }, new[] { 0 })]
        [TestCase(new[] { 1, 0, 1, 1, 1, 1, 1, 0, 0 }, new[] { 1, 0, 1, 1, 1, 1, 0, 0 })]

        public void GetDivisionRemainder_ShouldReturnCorrectRemainder(int[] divisor, int[] expected)
        {
            var originalPolynomial = new BinaryPolynomial(1, 0, 1, 1, 1, 1, 0, 0);
            var divisorPolynomial = new BinaryPolynomial(divisor);
            var expectedPolynomial = new BinaryPolynomial(expected);
            originalPolynomial.GetDivisionRemainder(divisorPolynomial).Should().BeEquivalentTo(expectedPolynomial);
        }

        [TestCase(new[] { 1, 0, 1, 1, 0, 0, 0 }, new[] { 1, 1, 0 }, new[] { 1, 0 })]
        public void GetDivisionRemainder_ShouldReturnCorrectRemainder(int[] original, int[] divisor, int[] expected)
        {
            var originalPolynomial = new BinaryPolynomial(original);
            var divisorPolynomial = new BinaryPolynomial(divisor);
            var expectedPolynomial = new BinaryPolynomial(expected);
            originalPolynomial.GetDivisionRemainder(divisorPolynomial).Should().BeEquivalentTo(expectedPolynomial);
        }

        [TestCase(new[] { 1, 0, 1, 1, 0 }, new[] { 1, 0, 1 }, new[] { 1, 0, 0 })]
        [TestCase(new[] { 1, 0, 1, 1, 0, 1, }, new[] { 1, 0, 1, 0 }, new[] { 1, 0, 0 })]
        [TestCase(new[] { 1, 0, 1, 0 }, new[] { 1, 1 }, new[] { 1, 1, 0, })]
        [TestCase(new[] { 1, 0, 1, 1, 0, 0, 0 }, new[] { 1, 1, 0 }, new[] { 1, 1, 0, 1, 1 })]

        public void Divide_ShouldReturnCorrectResult(int[] original, int[] divisor, int[] expected)
        {
            var originalBinaryPolinomial = new BinaryPolynomial(original);
            var divisorBinaryPolinomial = new BinaryPolynomial(divisor);
            var expectedBinaryPolinomial = new BinaryPolynomial(expected);

            originalBinaryPolinomial.Divide(divisorBinaryPolinomial).Result.Should().BeEquivalentTo(expectedBinaryPolinomial);
        }

        [TestCase(new[] { 1, 0, 1, 1, 0 }, new[] { 1, 0, 1 }, new[] { 1, 0 })]
        [TestCase(new[] { 1, 0, 1, 1, 0, 1, }, new[] { 1, 0, 1, 0 }, new[] { 1, 0, 1 })]
        [TestCase(new[] { 1, 0, 1, 0 }, new[] { 1, 1 }, new[] { 0 })]
        [TestCase(new[] { 1, 0, 1, 1, 0, 0, 0 }, new[] { 1, 1, 0 }, new[] { 1, 0 })]
        public void Divide_shouldReturnCorrectRemainder(int[] original, int[] divisor, int[] expected)
        {
            var originalBinaryPolinomial = new BinaryPolynomial(original);
            var divisorBinaryPolinomial = new BinaryPolynomial(divisor);
            var expectedBinaryPolinomial = new BinaryPolynomial(expected);

            originalBinaryPolinomial.Divide(divisorBinaryPolinomial).Remainder.Should().BeEquivalentTo(expectedBinaryPolinomial);
        }
    }
}
