namespace CRC
{
    interface ICyclicRedundancyCheckManager
    {
        BinaryPolynomial AppendRedundantBits(BinaryPolynomial originalPolynomial, BinaryPolynomial crcPolynomial);
        BinaryPolynomial CalculateRedundantBits(BinaryPolynomial originalPolynomial, BinaryPolynomial crcPolynomial);
        bool IsUnchanged(BinaryPolynomial crcEncodedPolynomial, BinaryPolynomial crcPolynomial);
    }
}
