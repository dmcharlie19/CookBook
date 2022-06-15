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
        [DisableRequestSizeLimit]
        public void AddRecipe()
        {
            string? userIdString = User.FindFirst( UserClaim.UserId )?.Value;
            if ( userIdString == null )
                throw new InvalidClientParameterException( "user id not found" );

            if ( Request.Form.Files.Count != 1 )
                throw new InvalidClientParameterException( "Неверное количество файлов" );
            IFormFile imgFile = Request.Form.Files[ 0 ];

            if ( Request.Form.Keys.Count != 1 )
                throw new InvalidClientParameterException( "Недостаточно данных" );
            AddRecipeRequestDto addRecipeRequest = JsonConvert.DeserializeObject<AddRecipeRequestDto>( Request.Form[ "data" ] );

            string imgPath = _imageService.SaveImage( imgFile.OpenReadStream(), imgFile.FileName );

            var tags = _tagService.AddTags( addRecipeRequest.Tags );
            _unitOfWork.Commit();

            _recipeService.AddRecipe( int.Parse( userIdString ), addRecipeRequest, tags, imgPath );
            _unitOfWork.Commit();

        }

        [HttpGet, Route( "User/{userId}" )]
        public IReadOnlyList<RecipeShortDto>? GetRecipesByUserId( [FromRoute] int userId )
        {
            return _recipeQuery.GetRecipesByUserId( userId );
        }

        [HttpGet, Route( "Image/{recipeId}" )]
        public void GetRecipeImage( [FromRoute] int recipeId )
        {
            var path = _recipeQuery.GetRecipeImagePath( recipeId );
            if ( path != "" )
            {
                // var file = _imageService.LoadImage( path );
                Response.ContentType = "image/jpeg";
                Response.SendFileAsync( path );
            }

        }
    }
}

