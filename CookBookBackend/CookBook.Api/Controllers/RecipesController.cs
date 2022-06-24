using CookBook.Api.Utils;
using CookBook.Application.Dto;
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
        public IReadOnlyList<RecipeShortDto>? GetRecipesPage( [FromRoute] int page )
        {
            return _recipeQuery.GetRecipesPage( page );
        }

        [HttpGet, Route( "getRecipeFull/{recipeId}" )]
        public RecipeFullDto GetRecipeDetail( [FromRoute] int recipeId )
        {
            return _recipeQuery.GetRecipeDetail( recipeId );
        }

        [HttpGet, Route( "getRecipeShort/{recipeId}" )]
        public RecipeShortDto GetRecipeShort( [FromRoute] int recipeId )
        {
            return _recipeQuery.GetRecipeShort( recipeId );
        }

        [HttpGet, Route( "users/{userId}" )]
        public IReadOnlyList<RecipeShortDto>? GetRecipesByUserId( [FromRoute] int userId )
        {
            return _recipeQuery.GetByUserId( userId );
        }

        [HttpGet]
        [Route( "search/{searchRequest}" )]
        public IReadOnlyList<RecipeShortDto>? SearchRecipe( [FromRoute] string searchRequest )
        {
            return _recipeQuery.SearchRecipe( searchRequest );
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

        [HttpPost, Authorize, Route( "addRecipe" )]
        [DisableRequestSizeLimit]
        public void AddRecipe()
        {
            if ( Request.Form.Keys.FirstOrDefault( key => key == "data" ) == null )
                throw new InvalidClientParameterException( "Недостаточно данных" );

            string imgPath = "";
            if ( Request.Form.Files.Count != 0 )
            {
                IFormFile imgFile = Request.Form.Files[ 0 ];
                imgPath = _imageService.SaveImage( imgFile.OpenReadStream(), imgFile.FileName );
            }

            AddRecipeRequestDto addRecipeRequest = JsonConvert.DeserializeObject<AddRecipeRequestDto>( Request.Form[ "data" ] );

            var tags = _tagService.AddTags( addRecipeRequest.Tags );
            _unitOfWork.Commit();

            _recipeService.AddRecipe( UserIdQualifier.GetUserId( this.HttpContext ), addRecipeRequest, tags, imgPath );
            _unitOfWork.Commit();

        }

        [HttpPost]
        [Route( "addLike/{recipeId}" )]
        [Authorize]
        public IActionResult AddLike( [FromRoute] int recipeId )
        {
            int userId = UserIdQualifier.GetUserId( this.HttpContext );
            _recipeService.AddLike( userId, recipeId );
            _unitOfWork.Commit();
            return Ok();
        }

        [HttpPost]
        [Route( "addFavorite/{recipeId}" )]
        [Authorize]
        public IActionResult AddFavorite( [FromRoute] int recipeId )
        {
            int userId = UserIdQualifier.GetUserId( this.HttpContext );
            _recipeService.AddFavorite( userId, recipeId );
            _unitOfWork.Commit();
            return Ok();
        }

        [HttpDelete]
        [Route( "delete/{recipeId}" )]
        [Authorize]
        public void DeleteRecipe( [FromRoute] int recipeId )
        {
            _recipeService.DeleteRecipe( UserIdQualifier.GetUserId( this.HttpContext ), recipeId );
            _unitOfWork.Commit();
        }
    }
}

