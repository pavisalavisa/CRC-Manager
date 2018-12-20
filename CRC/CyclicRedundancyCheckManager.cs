namespace CRC
{
    public class CyclicRedundancyCheckManager : ICyclicRedundancyCheckManager
    {
        public BinaryPolynomial AppendRedundantBits(BinaryPolynomial originalPolynomial, BinaryPolynomial crcPolynomial)
        {
            var redundantBits = CalculateRedundantBits(originalPolynomial, crcPolynomial);
            var polynomialWithFCS = new BinaryPolynomial(originalPolynomial);

            PadWithLeadingZeros(redundantBits, crcPolynomial);

            polynomialWithFCS.Append(redundantBits);

            return polynomialWithFCS;
        }

        private void PadWithLeadingZeros(BinaryPolynomial redundantBits, BinaryPolynomial crcPolynomial)
        {
            for (var i = redundantBits.Degree; i < crcPolynomial.Degree - 1; i++)
            {
                redundantBits.Degree++;
                redundantBits.Value.Add(new BinaryPolynomialMember(i + 1, 0));
            }
        }

        public BinaryPolynomial CalculateRedundantBits(BinaryPolynomial originalPolynomial, BinaryPolynomial crcPolynomial)
        {
            return originalPolynomial.GetDivisionRemainder(crcPolynomial);
        }

        public bool IsUnchanged(BinaryPolynomial crcEncodedPolynomial, BinaryPolynomial crcPolynomial)
        {
            var polynomial = crcEncodedPolynomial.GetDivisionRemainder(crcPolynomial);

            return polynomial.Value.TrueForAll(x => x.Value == 0) && polynomial.Degree == 0;
        }
    }
}
