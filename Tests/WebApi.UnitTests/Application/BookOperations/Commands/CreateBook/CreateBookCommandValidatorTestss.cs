using Xunit;
using TestSetup;
using AutoMapper;
using WebApi.DBOperations; 
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using FluentAssertions;
using System;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests:IClassFixture<CommonTestFixture>
    {


        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {

        }
    }
}