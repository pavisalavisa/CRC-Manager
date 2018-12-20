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
            LeftShift(polinomial.Degree + 1);

            //Append those bits
            polinomial.Value.ForEach(x => { this.Value.First(y => y.Position == x.Position).Value = x.Value; });
        }

        public void LeftShift(int steps)
        {
            Degree += steps;
            Value.ForEach(x => x.Position += steps);

            for (var i = 0; i < steps; i++)
            {
                Value.Add(new BinaryPolynomialMember(i, 0));
            }
        }

        public BinaryPolinomial GetDivisionRemainder(BinaryPolinomial polynomial)
        {
            var generatingPolynomial = polynomial.Value.Select(x => x.Value).ToArray();
            var leadingMemberIndex = 0;
            var batchSize = polynomial.Degree + 1;
            var initialPolynomial = this.Value.Select(x => x.Value).ToArray();

            while (leadingMemberIndex + batchSize <= Degree + 1)//jos uvik ima bitova
            {
                var j = 0;
                for (var i = leadingMemberIndex; i < leadingMemberIndex + batchSize; i++)
                {
                    initialPolynomial[i] = (initialPolynomial[i] + generatingPolynomial[j++]) % 2;//xor vrijednosti
                }
                leadingMemberIndex = GetNextLeadingMemberIndex(initialPolynomial);
                if (leadingMemberIndex == -1)//No more leading 1 => we're done
                {
                    return new BinaryPolinomial(0);
                }
            }
            return new BinaryPolinomial(TrimHeadingZeros(initialPolynomial));
        }

        public PolynomialDivisionResult<BinaryPolinomial> Divide(BinaryPolinomial polynomial)
        {
            var generatingPolynomial = polynomial.Value.Select(x => x.Value).ToArray();
            var leadingMemberIndex = 0;
            var batchSize = polynomial.Degree + 1;
            var initialPolynomial = this.Value.Select(x => x.Value).ToArray();
            BinaryPolinomial resultPolynomial = new BinaryPolinomial();

            while (leadingMemberIndex + batchSize <= Degree + 1)//jos uvik ima bitova
            {
                var j = 0;
                for (var i = leadingMemberIndex; i < leadingMemberIndex + batchSize; i++)
                {
                    initialPolynomial[i] = (initialPolynomial[i] + generatingPolynomial[j++]) % 2;//xor vrijednosti
                }

                var nextLeadingMemberIndex = GetNextLeadingMemberIndex(initialPolynomial);
                if (nextLeadingMemberIndex != -1)
                {
                    AppendDivisonResultMember(resultPolynomial, nextLeadingMemberIndex - leadingMemberIndex);
                }
                else if (nextLeadingMemberIndex == -1)//No more leading 1 => we're done
                {
                    AppendDivisionResultTrailingZeros(resultPolynomial, Degree - leadingMemberIndex);
                    return new PolynomialDivisionResult<BinaryPolinomial>(resultPolynomial, new BinaryPolinomial(0));
                }
                leadingMemberIndex = nextLeadingMemberIndex;
            }
            //Dodaj onoliko nula koliko je ostalo do kraja
            AppendDivisionResultTrailingZeros(resultPolynomial, Degree - leadingMemberIndex);
            return new PolynomialDivisionResult<BinaryPolinomial>(resultPolynomial, new BinaryPolinomial(TrimHeadingZeros(initialPolynomial)));
        }

        private void AppendDivisionResultTrailingZeros(BinaryPolinomial resultPolynomial, int restOfMembers)
        {
            while (restOfMembers > 0)
            {
                resultPolynomial.Append(new BinaryPolinomial(0));
                restOfMembers--;
            }
        }

        private void AppendDivisonResultMember(BinaryPolinomial resultPolynomial, int numbersTaken)
        {
            while (numbersTaken > 1)
            {
                resultPolynomial.Append(new BinaryPolinomial(0));
                numbersTaken--;
            }
            resultPolynomial.Append(new BinaryPolinomial(1));
        }

        private int[] TrimHeadingZeros(int[] polynomial)
        {
            var index = -1;
            for (var i = 0; i < polynomial.Length; i++)
            {
                if (polynomial[i] != 0)
                {
                    index = i;
                    break;
                }
            }
            var polynomialList = polynomial.ToList();
            polynomialList.RemoveRange(0, index);

            return polynomialList.ToArray();
        }


        private int GetNextLeadingMemberIndex(int[] initialPolynomial)
        {
            for (var i = 0; i < initialPolynomial.Length; i++)
            {
                if (initialPolynomial[i] == 1)
                {
                    return i;
                }
            }

            return -1;
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
