using System;
using System.Collections.Generic;
using System.Linq;

namespace CRC
{
    public class DecimalPolynomial
    {
        public int Degree { get; set; }
        public List<DecimalPolynomialMember> Value { get; set; }

        public DecimalPolynomial(DecimalPolynomial decimalPolynomial)
        {
            Degree = decimalPolynomial.Degree;
            Value = decimalPolynomial.Value;
        }

        public PolynomialDivisionResult<DecimalPolynomial> Divide(DecimalPolynomial divisorPolynomial)
        {
            if (divisorPolynomial.Value.TrueForAll(x => x.Value == 0))
            {
                throw new ArithmeticException();
            }

            var quotientPolynomial = new int[Degree];
            var remainderPolynomial = Value.Select(x=>x.Value).ToArray();

            while (remainderPolynomial.Any(x=>x==1) && PolynomialDegree(remainderPolynomial) > divisorPolynomial.Degree)
            {
                var temp = //Divide( leading terms)
                    quotientPolynomial.Add(temp);
                remainderPolynomial

            }

            
            //   q ← 0
            //   r ← n       # At each step n = d × q + r
            //   while r ≠ 0 AND degree(r) ≥ degree(d):
            //   t ← lead(r)/lead(d)     # Divide the leading terms
            //   q ← q + t
            //   r ← r − t * d
            //   return (q, r)
               
        }

        private int PolynomialDegree(int[] polynomial)
        {
            for (int i = polynomial.Length - 1; i >= 0; --i)
            {
                if (polynomial[i] != 0) return i;
            }
            return -1;
        }
    }
}
