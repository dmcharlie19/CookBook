using System;
using System.Collections.Generic;
using CookBookBackend.Application.AppServices.Dto;
using CookBookBackend.Application.Repositories;
using CookBookBackend.Core.Domain;

namespace CookBookBackend.Application.AppService
{
  public class RecipeService : IRecipeService
  {
    IRecipeRepository _recipeRepository;
    IUnitOfWork _unitOfWork;
    public RecipeService( IRecipeRepository recipeRepository, IUnitOfWork unitOfWork )
    {
      _recipeRepository = recipeRepository;
      _unitOfWork = unitOfWork;
    }

    public void EditRecipe( RecipeDto recipeDto )
    {
      //#Todo реализовать..
      throw new NotImplementedException();
    }

    public int CreateRecipe( RecipeDto recipeDto )
    {
      Recipe recipe = new Recipe
      {
        Title = recipeDto.Title,
        ShortDescription = recipeDto.ShortDescription,
        PreparingTime = recipeDto.PreparingTime
      };

      _recipeRepository.Create( recipe );
      _unitOfWork.Commit();

      return recipe.Id;
    }

    public void Delete( int recipeId )
    {
      _recipeRepository.Delete( recipeId );
      _unitOfWork.Commit();
    }

    public RecipeDto? GetRecipe( int recipeId )
    {
      var recipe = _recipeRepository.Get( recipeId );
      return new RecipeDto
      {
        Id = recipe.Id,
        Title = recipe.Title,
        ShortDescription = recipe.ShortDescription,
        PreparingTime = recipe.PreparingTime
      };
    }

    public List<RecipeDto> GetRecipes()
    {
      return _recipeRepository.GetRecipes().ConvertAll( recipe => new RecipeDto
      {
        Id = recipe.Id,
        Title = recipe.Title,
        ShortDescription = recipe.ShortDescription,
        PreparingTime = recipe.PreparingTime
      } );
    }
  }
}
