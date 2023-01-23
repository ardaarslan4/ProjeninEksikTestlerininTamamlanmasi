using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                if(context.Books.Any()) //Eğer burada hiç veri varsa
                {
                    return;
                }
                context.Genres.AddRange
                (
                    new Genre
                    {
                        Name = "PersonalGrowth"
                    },
                    new Genre
                    {
                        Name = "ScienceFiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );


                context.Books.AddRange
                (
                    new Book
                    {
                        //Id = 1,
                        Title ="Lean Startup",
                        GenreId =1, //PersonalGrowth
                        PageCount =200,
                        PublishDate = new DateTime(2001,06,12)
                    },

                    new Book
                    {
                        //Id = 2,
                        Title ="Herland",
                        GenreId =2, //ScienceFiction
                        PageCount =250,
                        PublishDate = new DateTime(2010,05,23) 

                    },

                    new Book
                    {
                        //Id = 3,
                        Title ="Herland",
                        GenreId =2, //ScienceFiction
                        PageCount =540,
                        PublishDate = new DateTime(2001,12,21)
                    }
                );
            }
        }
    }
}