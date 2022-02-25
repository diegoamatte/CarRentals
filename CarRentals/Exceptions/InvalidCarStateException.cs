namespace CarRentals.Exceptions
{
    public class InvalidCarStateException : Exception
    {
        public InvalidCarStateException(string message)
            : base(message)
        {

        }

        public InvalidCarStateException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
