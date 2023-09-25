namespace TourPackage.Exceptions
{
    public class DatabaseEmptyException : Exception
    {
        public DatabaseEmptyException() : base()
        {
        }

        public DatabaseEmptyException(string message) : base(message)
        {
        }
    }
}
