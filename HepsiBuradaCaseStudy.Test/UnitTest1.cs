using HepsiBuradaCaseStudy.Services;
using System;
using Xunit;

namespace HepsiBuradaCaseStudy.Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new string[] { "5 5", "1 2 N", "LMLMLMLMM" }, "1 3 N", "Rover navigated to expected destination.")]
        [InlineData(new string[] { "5 5", "3 3 E", "MMRMMRMRRM" }, "5 1 E", "Rover navigated to expected destination.")]
        [InlineData(new string[] { }, "Please check the parameters!", "Invalid input because of empty string array")]
        [InlineData(new string[] { "5 5", "3 3", "MMRMMRMRRM" }, "Please check the parameters!", "Invalid input because the starting destination doesn't have direction.")]
        [InlineData(new string[] { "5 5", "3 3 E", "LMLMLL" }, "2 4 E", "Rover navigated to expected destination.")]
        [InlineData(new string[] { "5", "3 3", "MMRMMRMRRM" }, "Please check the parameters!", "Invalid input because no y position to create surface array.")]
        [InlineData(new string[] { "5 5", "1 2 N", "" }, "1 2 N", "Rover stayed in the same position.")]
        public void Test1(string[] input, string expected, string description)
        {
            var SUT = new NavigationService();
            var actual = SUT.Navigate(input);
            Assert.Equal(expected, actual);
        }
    }
}
