using System;
using System.Reflection.Metadata;

namespace CookBook.Core.Domain
{
  public class RecipeIngredient
  {
    public int Id { get; protected set; }
    public string Title { get; protected set; }
    public string Content { get; protected set; }

    public int RecipeId { get; protected set; }
    public Recipe Recipe { get; protected set; }

    public RecipeIngredient( string title, string content, int recipeId )
    {
      Title = title;
      Content = content;
      RecipeId = recipeId;
    }
  }
}
