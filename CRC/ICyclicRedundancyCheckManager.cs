namespace CRC
{
    interface ICyclicRedundancyCheckManager
    {
        BinaryPolinomial AppendRedundantBits(BinaryPolinomial originalPolinomial, BinaryPolinomial crcPolynomial);
        BinaryPolinomial CalculateRedundantBits(BinaryPolinomial originalPolinomial, BinaryPolinomial crcPolynomial);
        bool IsUnchanged(BinaryPolinomial crcEncodedPolinomial, BinaryPolinomial crcPolynomial);
    }
}
