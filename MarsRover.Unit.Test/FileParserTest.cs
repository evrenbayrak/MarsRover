using MarsRover.Enums;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRover.Unit.Test
{
    public class FileParserTest
    {
        [Fact]

        public void GivenEmptyStringForUpperRightCordinates_ShouldThrowFormatException()
        {
            //given
            var fileParser = new FileParser();

            //then
            Assert.Throws<FormatException>(() => fileParser.GetUpperRightCoordinates(""));
            
        }

        [Fact]
        public void GivenCorrectStringForUpperRightCordinates_ShouldReturnCoordinates()
        {
            //given
            string coordinate = "2 3";
            var fileParser = new FileParser();

            //when
            Coordinates coordinates = fileParser.GetUpperRightCoordinates(coordinate);

            //then
            Assert.Equal(2, coordinates.X);
            Assert.Equal(3, coordinates.Y);

        }

        [Fact]
        public void GivenWrongStringForRoverPosition_ShouldThrowFormatException()
        {
            //given
            string position = "2 3 X";
            var fileParser = new FileParser();

            //then
            Assert.Throws<FormatException>(() => fileParser.GetRoverPosition(position));

        }

        [Fact]
        public void GivenCorrectStringForRoverPosition_ShouldReturnNode()
        {
            //given
            string position = "2 3 W";
            var fileParser = new FileParser();

            //when
            Node node = fileParser.GetRoverPosition(position);


            //then
            Assert.Equal(2, node.X);
            Assert.Equal(3, node.Y);
            Assert.Equal(3, node.CurrentDirection);

        }

        [Fact]
        public void GivenWrongStringForDirections_ShouldThrowFormatException()
        {
            //given
            string direction = "X";
            var fileParser = new FileParser();

            //then
            Assert.Throws<FormatException>(() => fileParser.GetDirection(direction));

        }

        [Theory]
        [MemberData(nameof(enumValues))]
        public void GivenCorrectStringForDirections_ShouldReturnDirection(Directions direction)
        {
            //given
            string directionString = direction.ToString();
            var fileParser = new FileParser();

            //when
            Directions directions = fileParser.GetDirection(directionString);

            //then
            Assert.Equal(directionString, directions.ToString());
          
        }

        public static IEnumerable<object[]> enumValues()
        {
            foreach (var direction in Enum.GetValues(typeof(Directions)))
            {
                yield return new object[] { direction };
            }
        }

        [Fact]
        public void GivenWrongStringForCommands_ShouldThrowFormatException()
        {
            //given
            string commands = "LMABCABCABCR";
            var fileParser = new FileParser();

            //then
            Assert.Throws<FormatException>(() => fileParser.GetCommandList(commands));

        }


        [Fact]
        public void GivenCorrectStringForCommands_ShouldReturnCommand()
        {
            //given
            string commands = "LMLMRM";
            var fileParser = new FileParser();

            //when
            List<Commands> commandList = fileParser.GetCommandList(commands);

            //then
            Assert.True(commandList.Count > 0);

        }

    }
}
