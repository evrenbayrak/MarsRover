using MarsRover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.Unit.Test
{
    public class MarsRoverSolverTest
    {

        [Fact]
        public void GivenCorrectCoordinatesStringForMove_ShouldChangeCoordinates()
        {
            //given
            Node testNode = new Node((int)Directions.N, 1, 1); 
            Coordinates maxCoordinates = new Coordinates(5, 5);
            var marsRoverSolver = new MarsRoverSolver();
            marsRoverSolver.CreateLinkedList();
            //when
            marsRoverSolver.Move(ref testNode, maxCoordinates);

            //then
            Assert.Equal(1, testNode.X);
            Assert.Equal(2, testNode.Y);
        }


        [Fact]
        public void GivenUpperRightCoordinatesStringForMove_ShouldNotChangeCoordinates()
        {
            //given
            Node testNode = new Node((int)Directions.N, 5, 5);
            Coordinates maxCoordinates = new Coordinates(5, 5);
            var marsRoverSolver = new MarsRoverSolver();
            marsRoverSolver.CreateLinkedList();
            //when
            marsRoverSolver.Move(ref testNode, maxCoordinates);

            //then
            Assert.Equal(5, testNode.X);
            Assert.Equal(5, testNode.Y);
        }


        [Theory]
        [MemberData(nameof(GetNodes))]
        public void GivenCorrectNodeForLinkedList_ShouldChangeHeadNode(Node node)
        {
            //given
            var marsRoverSolver = new MarsRoverSolver();
            marsRoverSolver.CreateLinkedList();

            //when
            marsRoverSolver.SetHeadNode(node.CurrentDirection);

            //then
            Assert.Equal(node.CurrentDirection, MarsRoverSolver.directionList.Head.CurrentDirection);

        }

        public static IEnumerable<object[]> GetNodes()
        {
            yield return new object[] { new Node((int)Directions.N, (int)VerticalMove.STAND, (int)HorizontalMove.UP) };
            yield return new object[] { new Node((int)Directions.E, (int)VerticalMove.RIGHT, (int)HorizontalMove.STAND) };
            yield return new object[] { new Node((int)Directions.S, (int)VerticalMove.STAND, (int)HorizontalMove.DOWN) };
            yield return new object[] { new Node((int)Directions.W, (int)VerticalMove.LEFT, (int)HorizontalMove.STAND) };

        }


        [Fact]
        public void GivenNorthNodeForLinkedList_ShouldFindWestLeftAndEastRight()
        {
            //given
            var marsRoverSolver = new MarsRoverSolver();
            marsRoverSolver.CreateLinkedList();
            marsRoverSolver.SetHeadNode((int)Directions.N);

            //when
            Node leftMustWest = MarsRoverSolver.directionList.Head.Left;
            Node rightMustEast = MarsRoverSolver.directionList.Head.Right;

            //then
            Assert.Equal((int) Directions.W, leftMustWest.CurrentDirection);
            Assert.Equal((int) Directions.E, rightMustEast.CurrentDirection);
        }

        [Fact]
        public void GivenEastNodeForLinkedList_ShouldFindNorthLeftAndSouthRight()
        {
            //given
            var marsRoverSolver = new MarsRoverSolver();
            marsRoverSolver.CreateLinkedList();
            marsRoverSolver.SetHeadNode((int)Directions.E);

            //when
            Node leftMustNorth = MarsRoverSolver.directionList.Head.Left;
            Node rightMustSouth = MarsRoverSolver.directionList.Head.Right;

            //then
            Assert.Equal((int)Directions.N, leftMustNorth.CurrentDirection);
            Assert.Equal((int)Directions.S, rightMustSouth.CurrentDirection);
        }


        [Fact]
        public void GivenSouthNodeForLinkedList_ShouldFindEastLeftAndWestRight()
        {
            //given
            var marsRoverSolver = new MarsRoverSolver();
            marsRoverSolver.CreateLinkedList();
            marsRoverSolver.SetHeadNode((int)Directions.S);

            //when
            Node leftMustEast = MarsRoverSolver.directionList.Head.Left;
            Node rightMustWest = MarsRoverSolver.directionList.Head.Right;

            //then
            Assert.Equal((int)Directions.E, leftMustEast.CurrentDirection);
            Assert.Equal((int)Directions.W, rightMustWest.CurrentDirection);
        }

        [Fact]
        public void GivenWestNodeForLinkedList_ShouldFindSouthLeftAndNorthRight()
        {
            //given
            var marsRoverSolver = new MarsRoverSolver();
            marsRoverSolver.CreateLinkedList();
            marsRoverSolver.SetHeadNode((int)Directions.W);

            //when
            Node leftMustSouth = MarsRoverSolver.directionList.Head.Left;
            Node rightMustNorth = MarsRoverSolver.directionList.Head.Right;

            //then
            Assert.Equal((int)Directions.S, leftMustSouth.CurrentDirection);
            Assert.Equal((int)Directions.N, rightMustNorth.CurrentDirection);
        }
    }
}

