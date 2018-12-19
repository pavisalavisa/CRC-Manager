using System.Collections.Generic;
using System.Linq;

namespace CRC
{
    public class BinaryPolinomial
    {
        public int Degree { get; set; }
        public List<BinaryPolynomialMember> Value { get; set; }


        public BinaryPolinomial(params int[] values)
        {
            Degree = GetPolynomialDegreeFromArray(values); 
            InitializeListFromArray(values);
        }

        public BinaryPolinomial(BinaryPolinomial polynomial)
        {
            Degree = polynomial.Degree;
            Value = polynomial.Value;
        }

        public void Append(BinaryPolinomial polinomial)
        {
            //Move original value |polinomial| times to the left.
            LeftShift(polinomial.Degree+1);
            //append those bits
            polinomial.Value.ForEach(x => { this.Value.First(y => y.Position == x.Position).Value = x.Value; });
        }

        public void LeftShift(int steps)
        {
            Degree += steps;
            Value.ForEach(x => x.Position += steps);

            for (var i = 0; i < steps; i++)
            {
                Value.Add(new BinaryPolynomialMember(i,0));
            }
        }

        public BinaryPolinomial GetDivisionRemainder(BinaryPolinomial polynomial)
        {
            if (polynomial.Degree > Degree)
            {
                return new BinaryPolinomial(this);
            }

            //var initialKBits = new List<BinaryPolynomialMember>();
            //for (var i = 0; i < polynomial.Degree; i++)
            //{
            //    if (Value.Any(x => x.Position == Degree - i))
            //    {
            //        initialKBits.Add(new BinaryPolynomialMember());
            //    }
            //}
            //var initialKBits = Value.GetRange(0, polynomial.Degree);
            ////uzmi prvih k bitova
            ////xoraj ih s polinomom
            ////uzmi bitove s desna ako ih ima dovoljno
            ////uzmi iducih k bitova
            ////
            //while (GetNextKBits()==polynomial.Degree)
            //    //xor next polynomial.degree bits

            //for (var i = 0; i < Degree; i++)
            //{

            //}
            return null;
        }

       

        private int GetPolynomialDegreeFromArray(int[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                var currentValue = values[i];
                if (currentValue == 1)
                {
                    return values.Length - 1 - i;
                }
            }

            return 0;
        }

        private void InitializeListFromArray(int[] values)
        {
            Value = new List<BinaryPolynomialMember>();
            for (var i = 0; i < values.Length; i++)
            {
                var currentValue = values[i];
                Value.Add(currentValue == 1
                    ? new BinaryPolynomialMember(values.Length - 1 - i, 1)
                    : new BinaryPolynomialMember(values.Length - 1 - i, 0));
            }
        }
    }
}
