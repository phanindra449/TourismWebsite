using System.Runtime.Serialization;

namespace TourPackage.Exceptions
{
    [Serializable]
    internal class TourNotFoundException : Exception
    {
        public TourNotFoundException()
        {
        }

        public TourNotFoundException(string? message) : base(message)
        {
        }

        public TourNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TourNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}