namespace CookBookBackend.Core.Domain
{
  public class Recipe
  {
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public int PreparingTime { get; set; }
    public string? Tags { get; set; }
    public int LikesCount { get; set; }
    public int FavoritesCount { get; set; }
  }
}
