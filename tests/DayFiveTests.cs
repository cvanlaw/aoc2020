using System;
using Xunit;
using AoC2020.Entities;

namespace AoC2020.Tests
{
    public class DayFiveTests
    {
        [Theory]
        [InlineData("BFFFBBFRRR", 70, 7, 567)]
        [InlineData("FFFBBBFRRR", 14, 7, 119)]
        [InlineData("BBFFBBFRLL", 102, 4, 820)]
        public void SeatIdTests(string input, int expectedRow, int expectedColumn, int expectedSeatId)
        {
            var boardingPass = new BoardingPass(input);
            Assert.Equal(expectedColumn, boardingPass.Column);
            Assert.Equal(expectedRow, boardingPass.Row);
            Assert.Equal(expectedSeatId, boardingPass.SeatId);
        }
    }
}
