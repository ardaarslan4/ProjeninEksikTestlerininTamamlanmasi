using WebApi.DBOperations;
using WebApi.Entities;
using System;

namespace TestSetup
{
    public static class Genres 
    {
        public static void AddGenres(this BookStoreDBContext context)
        {
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
        }
    }
}