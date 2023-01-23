using WebApi.DBOperations;
using AutoMapper; 
using Microsoft.EntityFrameworkCore;

namespace TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDBContext Context {get;set;}
        public IMapper Mapper {get;set;}
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDB").Options;
            Context = new BookStoreDBContext(options);
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg=>{cfg.AddProfile<MappingProfile>(); }).CreateMapper(); 
        }

    }
}