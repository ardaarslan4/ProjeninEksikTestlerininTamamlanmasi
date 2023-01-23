using AutoMapper;
using WebApi.BookOperations.GetBookDetail;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.Entities;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.BookOperations.GetBooksQuery;
using WebApi.Application.AuthorOperation.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {   
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();
            CreateMap<Book,BookDetailViewModel>().ForMember(dest =>dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book,BooksViewModel>().ForMember(dest =>dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            //Author
            CreateMap<Author,AuthorsViewModel>();
            CreateMap<Author,AuthorsDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();
        }
    }
}