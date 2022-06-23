using System;

namespace CookBook.Application.Dto
{
    public class AuthenticationResponseDto
    {
        public int Id { get; set; }
        public string? AccesToken { get; set; }
        public string? UserName { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
