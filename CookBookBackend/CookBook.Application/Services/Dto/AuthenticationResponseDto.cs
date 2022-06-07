using System;

namespace CookBook.Api.Dto
{
    public class AuthenticationResponseDto
    {
        public string? AccesToken { get; set; }
        public string? UserName { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
