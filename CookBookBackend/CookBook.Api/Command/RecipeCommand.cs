namespace CookBookBackend.Api.Command
{
  public class RecipeCommand
  {
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public int PreparingTime { get; set; }
  }
}
