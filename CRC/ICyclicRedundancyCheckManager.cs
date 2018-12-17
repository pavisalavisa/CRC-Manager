namespace CRC
{
    interface ICyclicRedundancyCheckManager
    {
        void AppendRedundantBits(BinaryPolinomial originalPolinomial, BinaryPolinomial crcPolynomial);
        BinaryPolinomial CalculateRedundantBits(BinaryPolinomial originalPolinomial, BinaryPolinomial crcPolynomial);
        bool IsUnchanged(BinaryPolinomial crcEncodedPolinomial, BinaryPolinomial crcPolynomial);
    }
}
