using MarsRover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class MarsRoverSolver
    {
        public static DoubleLinkedList directionList;
        public MarsRoverSolver()
        {   
        }

        /// <summary>
        /// Read input file, set static linked list by making left, reft node connection
        /// Running solver.
        /// </summary>
        public void Initialize()
        {
            InputModel inputModel = new FileParser().ReadFile(); 
            directionList = CreateLinkedList(); 
            Execute(inputModel);        
        }

        /// <summary>
        /// Execute input model by calling command actions.
        /// </summary>
        /// <param name="inputModel"></param>
        public void Execute(InputModel inputModel)
        {
            Coordinates maxCoordinates = inputModel.UpperRight;
            foreach (Rover rover in inputModel.Rover)
            {
                //head node is setting from rover which is implemented from text file.
                SetHeadNode(rover.StartingPoint.CurrentDirection);
                Node currentNode = rover.StartingPoint;
                foreach (Commands command in rover.CommandList)
                {
                    switch (command)
                    {
                        case Commands.L:
                        case Commands.R:
                            ChangeDirection(ref currentNode, command);
                            break;
                        case Commands.M:
                            Move(ref currentNode, maxCoordinates);
                            break;

                    }
                }
                PrintResult(currentNode);
            }
        }

        /// <summary>
        /// Print result coordinates and direction after every rover execution
        /// </summary>
        /// <param name="currentNode"></param>
        private void PrintResult(Node currentNode)
        {
            Console.WriteLine(currentNode.X + " " + currentNode.Y + " " + Enum.GetName(typeof(Directions), currentNode.CurrentDirection));
        }

        /// <summary>
        /// changes linkedlist head for command direction value
        /// </summary>
        /// <param name="node">Rover node</param>
        /// <param name="command">must be L(left) or R(right)</param>
        public void ChangeDirection(ref Node node, Commands command)
        {
            if (Commands.L.Equals(command))
            {
                directionList.Head = directionList.Head.Left;            
            }
            else if (Commands.R.Equals(command))
            {
                directionList.Head = directionList.Head.Right;
            }

            node.CurrentDirection = directionList.Head.CurrentDirection;
        }

        /// <summary>
        /// Change rover location by following current direction
        /// </summary>
        /// <param name="node">Rover node</param>
        /// <param name="maxCoordinates">determine boundaries</param>
        public void Move(ref Node node, Coordinates maxCoordinates)
        {
            int tempX = node.X + directionList.Head.X;
            int tempY = node.Y + directionList.Head.Y;
            node.X += IsInsideArea(tempX, maxCoordinates.X) ? directionList.Head.X : 0;
            node.Y += IsInsideArea(tempY, maxCoordinates.Y) ? directionList.Head.Y : 0;
        }

        /// <summary>
        /// Before move action, checks coordinates between lower left and upper right
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private bool IsInsideArea(int value, int max)
        {
            return value >= 0 && value <= max;
        }
         
        /// <summary>
        /// Create default doublelinked list by left, right connection.
        /// </summary>
        /// <returns></returns>
        public DoubleLinkedList CreateLinkedList()
        {
            directionList = new DoubleLinkedList();
            //define node one by one,
            Node north = new((int)Directions.N,(int)VerticalMove.STAND, (int)HorizontalMove.UP);
            Node east = new((int)Directions.E, (int)VerticalMove.RIGHT, (int)HorizontalMove.STAND);
            Node south = new((int)Directions.S, (int)VerticalMove.STAND, (int)HorizontalMove.DOWN);
            Node west = new((int)Directions.W, (int)VerticalMove.LEFT, (int)HorizontalMove.STAND);

            //create node connection, example : North->right = east, North->left = west
            north.SetLink(west, east);
            east.SetLink(north, south);
            south.SetLink(east, west);
            west.SetLink(south, north);
            directionList.Head = north; //North is default node head

            return directionList;
        }

        /// <summary>
        /// setting linked list head node by input direction and commands,
        /// rover know which direction it can move by head node.
        /// </summary>
        /// <param name="currentDirection"></param>
        public void SetHeadNode(int currentDirection)
        {
            Node currentNode = directionList.Head;
     
            while(currentNode != null)
            {
                if(currentDirection == currentNode.CurrentDirection)
                {
                    directionList.Head = currentNode;
                    return;
                }
                currentNode = currentNode.Left;
            }
        }
    
    }
}
