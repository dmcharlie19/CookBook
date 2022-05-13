using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Application.Queries.Dto
{
  public class RecipeDto
  {
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public int PreparingTime { get; set; }
    public int PersonCount { get; set; }
    public string[]? Tags { get; set; }
    public int LikesCount { get; set; }
    public int FavoritesCount { get; set; }
  }
}
