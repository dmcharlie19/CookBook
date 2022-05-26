using System;
using System.Reflection.Metadata;

namespace CookBook.Core.Domain
{
  public class Tag
  {
    public int Id { get; protected set; }
    public string Name { get; protected set; }

    public Tag( string name )
    {
      Name = name;
    }
  }
}
