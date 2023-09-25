using System.Runtime.Serialization;

namespace TourPackage.Exceptions
{
    [Serializable]
    internal class TourDetailsNotFoundException : Exception
    {
        public TourDetailsNotFoundException()
        {
        }

        public TourDetailsNotFoundException(string? message) : base(message)
        {
        }

        public TourDetailsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TourDetailsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}