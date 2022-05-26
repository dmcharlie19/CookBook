using System;
using System.Reflection.Metadata;

namespace CookBook.Core.Domain
{
  public class RecipeStep
  {
    public int Id { get; protected set; }
    public string Content { get; protected set; }

    public int RecipeId { get; protected set; }
    public Recipe Recipe { get; protected set; }

    public RecipeStep( string content, int recipeId )
    {
      Content = content;
      RecipeId = recipeId;
    }
  }
}