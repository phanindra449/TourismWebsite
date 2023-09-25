using System.Runtime.Serialization;

namespace TourPackage.Exceptions
{
    [Serializable]
    internal class TourDestinationNotFoundException : Exception
    {
        public TourDestinationNotFoundException()
        {
        }

        public TourDestinationNotFoundException(string? message) : base(message)
        {
        }

        public TourDestinationNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TourDestinationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}