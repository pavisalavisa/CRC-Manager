namespace CRC
{
    public class PolynomialDivisionResult<T>
    {
        public T Result { get; set; }
        public T Remainder { get; set; }

        public PolynomialDivisionResult(T result, T remainder)
        {
            Result = result;
            Remainder = remainder;
        }
    }
}
