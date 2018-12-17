using System;

namespace CRC
{
    public class BinaryPolynomialMember : IComparable
    {
        public int Position { get; set; }

        public BinaryPolynomialMember(int position)
        {
            Position = position;
        }

        public int CompareTo(object obj)
        {
            return Position.CompareTo(((BinaryPolynomialMember)obj).Position);
        }
    }
}
