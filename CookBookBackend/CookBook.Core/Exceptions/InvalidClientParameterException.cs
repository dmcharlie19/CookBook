using System;

namespace CookBook.Core.Exceptions
{
    [Serializable]
    public class InvalidClientParameterException : Exception
    {
        public InvalidClientParameterException() : base() { }
        public InvalidClientParameterException( string message ) : base( message ) { }
        public InvalidClientParameterException( string message, Exception inner ) : base( message, inner ) { }
    }
}
