using Xunit;
using TestSetup;
using Automapper;
using WebApi.DBOperations; 
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using FluentAssertions;
using System;

namespace Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            var book = new Book(){Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                                 PageCOunt =100, PublishDate = new System.DateTime(1990,01,10),GenreId =1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel(){Title = book.Title};
            //act & assert (Çalıştırma)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut.");
        }
    }
}