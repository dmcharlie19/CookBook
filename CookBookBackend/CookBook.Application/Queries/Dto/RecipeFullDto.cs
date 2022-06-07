using CookBook.Api.Dto;

namespace CookBook.Application.Queries.Dto
{
    public class RecipeFullDto
    {
        public RecipeShortDto RecipeShortInfo { get; set; }
        public string[] CookingSteps { get; set; }
        public RecipeIngridientDto[] RecipeIngridients { get; set; }
    }
}
