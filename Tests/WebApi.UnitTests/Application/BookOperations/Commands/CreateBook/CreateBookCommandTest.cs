using Xunit;
using TestSetup;
using AutoMapper;
using WebApi.DBOperations; 
using WebApi.BookOperations.CreateBook;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using FluentAssertions;
using System;


namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
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
                                 PageCount =100, PublishDate = new System.DateTime(1990,01,10),GenreId =1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel(){Title = book.Title};

            //act & assert (Çalıştırma)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel()
            {Title ="Hobbit", PageCount =1000, PublishDate=DateTime.Now.Date.AddYears(-10),GenreId =1};
            command.Model = model;

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assert   
            var book = _context.Books.SingleOrDefault(book=>book.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}