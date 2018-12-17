using System;

namespace CRC
{
    public class CyclicRedundancyCheckManager :ICyclicRedundancyCheckManager
    {
        public void AppendRedundantBits(BinaryPolinomial originalPolinomial, BinaryPolinomial crcPolynomial)
        {
            throw new NotImplementedException();
        }

        public BinaryPolinomial CalculateRedundantBits(BinaryPolinomial originalPolinomial, BinaryPolinomial crcPolynomial)
        {
            throw new NotImplementedException();
        }

        public bool IsUnchanged(BinaryPolinomial crcEncodedPolinomial, BinaryPolinomial crcPolynomial)
        {
            throw new NotImplementedException();
        }
    }
}
