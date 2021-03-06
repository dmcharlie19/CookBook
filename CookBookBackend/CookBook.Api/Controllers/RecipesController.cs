using CookBook.Api.Dto;
using CookBook.Application.Entities.Users;
using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBook.Application.Repositories;
using CookBook.Application.Services;
using CookBook.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        private readonly IImageService _imageService;

        public RecipesController(
            IUnitOfWork unitOfWork,
            IRecipeQuery recipeQuery,
            IRecipeService recipeService,
            ITagService tagService,
            IImageService imageService )
        {
            _unitOfWork = unitOfWork;
            _recipeQuery = recipeQuery;
            _recipeService = recipeService;
            _tagService = tagService;
            _imageService = imageService;
        }

        [HttpGet]
        [Route( "getRecipes/{page}" )]
        public IReadOnlyList<RecipeShortDto>? GetAll( [FromRoute] int page )
        {
            return _recipeQuery.GetAll( page );
        }

        [HttpGet, Route( "getRecipeFull/{recipeId}" )]
        public RecipeFullDto GetRecipeDetail( [FromRoute] int recipeId )
        {
            return _recipeQuery.GetRecipeDetail( recipeId );
        }

        [HttpPost, Authorize, Route( "addRecipe" )]
        [DisableRequestSizeLimit]
        public void AddRecipe()
        {
            string? userIdString = User.FindFirst( UserClaim.UserId )?.Value;
            if ( userIdString == null )
                throw new InvalidClientParameterException( "user id not found" );

            if ( Request.Form.Files.Count != 1 )
                throw new InvalidClientParameterException( "???????????????? ???????????????????? ????????????" );
            IFormFile imgFile = Request.Form.Files[ 0 ];

            if ( Request.Form.Keys.Count != 1 )
                throw new InvalidClientParameterException( "???????????????????????? ????????????" );
            AddRecipeRequestDto addRecipeRequest = JsonConvert.DeserializeObject<AddRecipeRequestDto>( Request.Form[ "data" ] );

            string imgPath = _imageService.SaveImage( imgFile.OpenReadStream(), imgFile.FileName );

            var tags = _tagService.AddTags( addRecipeRequest.Tags );
            _unitOfWork.Commit();

            _recipeService.AddRecipe( int.Parse( userIdString ), addRecipeRequest, tags, imgPath );
            _unitOfWork.Commit();

        }

        [HttpGet, Route( "users/{userId}" )]
        public IReadOnlyList<RecipeShortDto>? GetRecipesByUserId( [FromRoute] int userId )
        {
            return _recipeQuery.GetByUserId( userId );
        }

        [HttpGet, Route( "images/{recipeId}" )]
        public void GetRecipeImage( [FromRoute] int recipeId )
        {
            var path = _recipeQuery.GetRecipeImagePath( recipeId );
            if ( path != "" )
            {
                Response.ContentType = "image/jpeg";
                Response.SendFileAsync( path ).Wait();
            }
        }

        [HttpDelete]
        [Route( "delete/{recipeId}" )]
        [Authorize]
        public void DeleteRecipe( [FromRoute] int recipeId )
        {
            string? userIdString = User.FindFirst( UserClaim.UserId )?.Value;
            if ( userIdString == null )
                throw new InvalidClientParameterException( "user id not found" );

            _recipeService.DeleteRecipe( int.Parse( userIdString ), recipeId );
            _unitOfWork.Commit();
        }

        [HttpGet]
        [Route( "search/{searchRequest}" )]
        public IReadOnlyList<RecipeShortDto>? SearchRecipe( [FromRoute] string searchRequest )
        {
            return _recipeQuery.SearchRecipe( searchRequest );
        }
    }
}

