using System.Collections.Generic;

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

        public void Append(BinaryPolinomial polinomial)
        {
            //Move original value |polinomial| times to the left.
            LeftShift(polinomial.Degree+1);
            //append those bits
            Value.AddRange(polinomial.Value);
        }

        public void LeftShift(int steps)
        {
            Degree += steps;
            Value.ForEach(x => x.Position += steps);
        }

        public BinaryPolinomial GetDivisionRemainder(BinaryPolinomial polynomial)
        {

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
                if (currentValue == 1)
                {
                    //add value to the list with index of max degree - current
                    Value.Add(new BinaryPolynomialMember(values.Length -1- i));
                }
            }
        }
    }
}
