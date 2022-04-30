using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Application.Queries.Dto;

namespace CookBook.Application.Queries
{
  public interface IRecipeQuery
  {
    List<RecipeDto> GetAll();
  }
}
