using CookBook.Api.Dto;
using CookBook.Application.Entities.Users;
using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBook.Application.Repositories;
using CookBook.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Api.Controllers
{
    [ApiController]
    [Route( "api/[controller]" )]

    public class RecipesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipeQuery _recipeQuery;
        private readonly IRecipeService _recipeService;
        private readonly ITagService _tagService;

        public RecipesController(
            IUnitOfWork unitOfWork,
            IRecipeQuery recipeQuery,
            IRecipeService recipeService,
            ITagService tagService )
        {
            _unitOfWork = unitOfWork;
            _recipeQuery = recipeQuery;
            _recipeService = recipeService;
            _tagService = tagService;
        }

        [HttpGet]
        public IReadOnlyList<RecipeShortDto>? GetAll()
        {
            return _recipeQuery.GetAll();
        }

        [HttpGet, Route( "{recipeId}" )]
        public RecipeFullDto GetRecipeDetail( [FromRoute] int recipeId )
        {
            return _recipeQuery.GetRecipeDetail( recipeId );
        }

        [HttpPost, Authorize, Route( "AddRecipe" )]
        public void AddRecipe( [FromBody] AddRecipeRequestDto addRecipeRequest )
        {
            string? userIdString = User.FindFirst( UserClaim.UserId )?.Value;
            if ( userIdString == null )
                throw new ApplicationException( "user id not found" );

            var tags = _tagService.AddTags( addRecipeRequest.Tags );
            _unitOfWork.Commit();

            _recipeService.AddRecipe( int.Parse( userIdString ), addRecipeRequest, tags );
            _unitOfWork.Commit();
        }
    }
}

