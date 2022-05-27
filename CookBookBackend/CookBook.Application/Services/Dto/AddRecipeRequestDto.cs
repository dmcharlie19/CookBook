namespace CookBook.Api.Dto
{

  public class RecipeIngridientDto
  {
    public string? IngridientTitle { get; set; }
    public string? IngridientBody { get; set; }
  }

  public class AddRecipeRequestDto
  {
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public int PreparingTime { get; set; }
    public int PersonCount { get; set; }
    public string[]? Tags { get; set; }
    public string[]? CookingSteps { get; set; }
    public RecipeIngridientDto[]? RecipeIngridients { get; set; }
  }
}
