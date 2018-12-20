using System;

namespace CRC
{
    public class DecimalPolynomialMember : IComparable
    {
        public int Position { get; set; }
        public int Value { get; set; }

        public DecimalPolynomialMember(int position, int value)
        {
            Position = position;
            Value = value;
        }

        public int CompareTo(object obj)
        {
            return Position.CompareTo(((DecimalPolynomialMember)obj).Position);
        }
    }
}