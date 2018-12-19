using System;

namespace CRC
{
    public class CyclicRedundancyCheckManager :ICyclicRedundancyCheckManager
    {
        public BinaryPolinomial AppendRedundantBits(BinaryPolinomial originalPolinomial, BinaryPolinomial crcPolynomial)
        {
            var redundantBits = CalculateRedundantBits(originalPolinomial, crcPolynomial);
            var polynomialWithFCS=new BinaryPolinomial(originalPolinomial);

            PadWithLeadingZeros(redundantBits, crcPolynomial);

            polynomialWithFCS.Append(redundantBits);

            return polynomialWithFCS;
        }

        private void PadWithLeadingZeros(BinaryPolinomial redundantBits, BinaryPolinomial crcPolynomial)
        {
            for (var i = redundantBits.Degree; i < crcPolynomial.Degree-1; i++)
            {
                redundantBits.Degree++;
                redundantBits.Value.Add(new BinaryPolynomialMember(i+1,0));
            }
        }

        public BinaryPolinomial CalculateRedundantBits(BinaryPolinomial originalPolinomial, BinaryPolinomial crcPolynomial)
        {
            return originalPolinomial.GetDivisionRemainder(crcPolynomial);
        }

        public bool IsUnchanged(BinaryPolinomial crcEncodedPolinomial, BinaryPolinomial crcPolynomial)
        {
            throw new NotImplementedException();
        }
    }
}
