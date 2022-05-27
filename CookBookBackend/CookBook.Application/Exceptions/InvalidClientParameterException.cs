using System;

namespace CookBook.Application.Exceptions
{
  [Serializable]
  public class InvalidClientParameterException : Exception
  {
    public InvalidClientParameterException() : base() { }
    public InvalidClientParameterException( string message ) : base( message ) { }
    public InvalidClientParameterException( string message, Exception inner ) : base( message, inner ) { }

    protected InvalidClientParameterException( System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context ) : base( info, context ) { }
  }
}
