using WebApi.DBOperations;
using WebAp.Entities;
using System;

namespace TestSetuo
{
    public static class Books 
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
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