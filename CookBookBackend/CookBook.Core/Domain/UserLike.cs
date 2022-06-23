namespace CookBook.Core.Domain
{
    public class UserLike
    {
        public int Id { get; protected set; }

        public User User { get; protected set; }
        public int UserId { get; protected set; }

        public Recipe Recipe { get; protected set; }
        public int RecipeId { get; protected set; }

        public UserLike( int recipeId )
        {
            RecipeId = recipeId;
        }
    }
}
