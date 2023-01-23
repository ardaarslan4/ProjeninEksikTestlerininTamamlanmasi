using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.GetBooksQuery;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController (IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /*
        private static List<Book> BookList = new List<Book>()
        {
            
            new Book
            {
                Id = 1,
                Title ="Lean Startup",
                GenreId =1, //PersonalGrowth
                PageCount =200,
                PublishDate = Convert.ToInt32(new DateOnly(2001,06,12))
            },

            new Book
            {
                Id = 2,
                Title ="Herland",
                GenreId =2, //ScienceFiction
                PageCount =250,
                PublishDate = Convert.ToInt32(new DateOnly(2010,05,23)) 

            },

            new Book
            {
                Id = 3,
                Title ="Herland",
                GenreId =2, //ScienceFiction
                PageCount =540,
                PublishDate = Convert.ToInt32(new DateOnly(2001,12,21)) 
            }

        };*/

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result); //http 200 bilgisiyle result'ı dönecek. Bunu IActionResult ile kullanabiliyoruz sadece.
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            BookDetailViewModel result;

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            
            /*try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }*/
            return Ok(result);
        }

        /*
        [HttpGet]
        public Book Get([FromQuery] string id)
        {
            var book = BookList.Where(book=> book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }
        */


        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
            /*try //Burada try yapısın kullanmamızın nedeni handle çağırıldığında hata mesajı dönerse kod kırılır bunu engellemeye çalışıyoruz.
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                
                /*if(!result.IsValid)
                foreach(var item in result.Errors)
                {
                    Console.WriteLine("Özellik"+item.PropertyName+"- Error Message:"+item.ErrorMessage);
                }

                else
                {
                    command.Handle();
                } 
                    
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }*/
            
            /*
            var book = _context.Books.SingleOrDefault(x=>x.Title == newBook.Title);

            if(book is not null)
                return BadRequest();
            
            _context.Books.Add(newBook);
            _context.SaveChanges();
            */
            return Ok();
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            /*
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;

                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }*/
            return Ok();

        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            /*try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }*/
                 
            return Ok();
        }


    }
}