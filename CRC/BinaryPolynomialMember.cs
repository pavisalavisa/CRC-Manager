using System;

namespace CRC
{
    public class BinaryPolynomialMember : IComparable
    {
        public int Position { get; set; }
        public int Value { get; set; }

        public BinaryPolynomialMember(int position, int value)
        {
            Position = position;
            Value = value;
        }

        public int CompareTo(object obj)
        {
            return Position.CompareTo(((BinaryPolynomialMember)obj).Position);
        }
    }
}
