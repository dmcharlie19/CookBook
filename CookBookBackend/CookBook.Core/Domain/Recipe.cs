namespace CookBookBackend.Core.Domain
{
  public class Recipe
  {
    public int Id { get; protected set; }
    public string? Title { get; protected set; }
    public string? ShortDescription { get; protected set; }
    public int PreparingTime { get; protected set; }
    public int PersonCount { get; set; }
    public string? Tags { get; protected set; }
    public int LikesCount { get; protected set; }
    public int FavoritesCount { get; protected set; }

    public Recipe(
        string title,
        string shortDescription,
        int preparingTime,
        int personCount,
        string tags,
        int likesCount,
        int favoritesCount )
    {
      Title = title;
      ShortDescription = shortDescription;
      PreparingTime = preparingTime;
      PersonCount = personCount;
      Tags = tags;
      LikesCount = likesCount;
      FavoritesCount = favoritesCount;
    }
  }
}
