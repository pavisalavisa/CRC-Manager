using System.Collections.Generic;
using System.Linq;

namespace CRC
{
    public class BinaryPolynomial
    {
        public int Degree { get; set; }
        public List<BinaryPolynomialMember> Value { get; set; }


        public BinaryPolynomial(params int[] values)
        {
            Degree = GetPolynomialDegreeFromArray(values);
            InitializeListFromArray(values);
        }

        public BinaryPolynomial(BinaryPolynomial polynomial)
        {
            Degree = polynomial.Degree;
            Value = polynomial.Value;
        }

        public void Append(BinaryPolynomial polynomial)
        {
            //Move original value |polynomial| times to the left.
            LeftShift(polynomial.Degree + 1);

            //Append those bits
            polynomial.Value.ForEach(x => { this.Value.First(y => y.Position == x.Position).Value = x.Value; });
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

        public BinaryPolynomial GetDivisionRemainder(BinaryPolynomial polynomial)
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
                    return new BinaryPolynomial(0);
                }
            }
            return new BinaryPolynomial(TrimHeadingZeros(initialPolynomial));
        }

        public PolynomialDivisionResult<BinaryPolynomial> Divide(BinaryPolynomial divisorPolynomial)
        {
            var divisor = divisorPolynomial.Value.Select(x => x.Value).ToArray();
            var leadingMemberIndex = 0;
            var batchSize = divisorPolynomial.Degree + 1;
            var dividend = this.Value.Select(x => x.Value).ToArray();
            BinaryPolynomial resultPolynomial = new BinaryPolynomial(1);

            while (EnoughBitsRemaining(leadingMemberIndex, batchSize))//jos uvik ima bitova
            {
                var j = 0;
                for (var i = leadingMemberIndex; i < leadingMemberIndex + batchSize; i++)
                {
                    dividend[i] = (dividend[i] + divisor[j++]) % 2; //xor Batch
                }

                var nextLeadingMemberIndex = GetNextLeadingMemberIndex(dividend);
                if (EnoughNonZeroBitsRemaining(nextLeadingMemberIndex, batchSize))//Ako ima dovoljno ne nula brojeva
                {
                    AppendDivisonResultMember(resultPolynomial, nextLeadingMemberIndex - leadingMemberIndex);
                }
                else if (NoLeadingOnes(nextLeadingMemberIndex))//No more leading 1 => we're done
                {
                    AppendDivisionResultTrailingZeros(resultPolynomial, Degree - (leadingMemberIndex + divisorPolynomial.Degree));
                    return new PolynomialDivisionResult<BinaryPolynomial>(resultPolynomial, new BinaryPolynomial(0));
                }
                else//Not enough numbers => we're done
                {
                    AppendDivisionResultTrailingZeros(resultPolynomial, Degree - (leadingMemberIndex + divisorPolynomial.Degree));

                    var remainingBits = GetRemainingBitsFromDividend(dividend, nextLeadingMemberIndex);
                    var remainderPolynomial=new BinaryPolynomial(remainingBits);

                    return new PolynomialDivisionResult<BinaryPolynomial>(resultPolynomial, remainderPolynomial);
                }
                leadingMemberIndex = nextLeadingMemberIndex;
            }
            //Dodaj onoliko nula koliko je ostalo do kraja
            AppendDivisionResultTrailingZeros(resultPolynomial, Degree - (leadingMemberIndex + divisorPolynomial.Degree));
            return new PolynomialDivisionResult<BinaryPolynomial>(resultPolynomial, new BinaryPolynomial(TrimHeadingZeros(dividend)));
        }

        private bool NoLeadingOnes(int nextLeadingMemberIndex)
        {
            return nextLeadingMemberIndex == -1;
        }

        private int[] GetRemainingBitsFromDividend(int[] initialPolynomial, int nextLeadingMemberIndex)
        {
            var list = new List<int>();
            for (var i = nextLeadingMemberIndex; i < initialPolynomial.Length; i++)
            {
                list.Add(initialPolynomial[i]);
            }

            return list.ToArray();
        }

        private bool EnoughNonZeroBitsRemaining(int nextLeadingMemberIndex, int batchSize)
        {
            return nextLeadingMemberIndex != -1 && Degree+1-nextLeadingMemberIndex>=batchSize;
        }

        private bool EnoughBitsRemaining(int leadingMemberIndex, int batchSize)
        {
            return Degree + 1 - leadingMemberIndex >= batchSize;
        }

        private void AppendDivisionResultTrailingZeros(BinaryPolynomial resultPolynomial, int restOfMembers)
        {
            while (restOfMembers > 0)
            {
                resultPolynomial.Append(new BinaryPolynomial(0));
                restOfMembers--;
            }
        }

        private void AppendDivisonResultMember(BinaryPolynomial resultPolynomial, int numbersTaken)
        {
            while (numbersTaken > 1)
            {
                resultPolynomial.Append(new BinaryPolynomial(0));
                numbersTaken--;
            }
            resultPolynomial.Append(new BinaryPolynomial(1));
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
