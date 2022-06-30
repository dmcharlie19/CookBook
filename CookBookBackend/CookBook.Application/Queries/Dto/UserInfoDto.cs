using System;

namespace CookBook.Application.Queries.Dto
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserDescription { get; set; }
        public int RecipesCount { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}

