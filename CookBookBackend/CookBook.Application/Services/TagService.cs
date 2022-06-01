using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Api.Dto;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Core.Exceptions;

namespace CookBook.Application.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService( ITagRepository tagRepository )
        {
            _tagRepository = tagRepository;
        }

        public List<Tag> AddTags( string[] tagNames )
        {
            if ( tagNames is null ||
                 tagNames.Length == 0 )
                throw new InvalidClientParameterException( "Тэги не должны быть пустыми" );

            List<Tag> tags = new();
            foreach ( var tagName in tagNames )
            {
                Tag tag = _tagRepository.Get( tagName );
                if ( tag is null )
                {
                    tag = new Tag( tagName );
                    _tagRepository.Add( tag );
                }

                tags.Add( tag );
            }

            return tags;
        }

    }
}
