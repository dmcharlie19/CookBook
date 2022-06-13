using System.Collections.Generic;
using CookBook.Core.Domain;

namespace CookBook.Application.Services
{
    public interface ITagService
    {
        List<Tag> AddTags( string[] tags );
    }
}