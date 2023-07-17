using System.Runtime.Serialization;

namespace RealEstateApp.Exceptions
{
    [Serializable]
    internal class InvalidPermissionException : Exception
    {
        public InvalidPermissionException()
        {
        }

        public InvalidPermissionException(string? message) : base(message)
        {
        }

        public InvalidPermissionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidPermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
