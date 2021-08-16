using MarsRover.Constants;
using MarsRover.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class FileParser
    {
        string fileName = Path.Combine(Environment.CurrentDirectory, @"Data\InputFile.txt");
        
        /// <summary>
        /// Read & parse input file, generates inputmodel
        /// </summary>
        /// <returns></returns>
        public InputModel ReadFile()
        {
            InputModel inputModel = new InputModel();
            
            using (StreamReader sr = File.OpenText(fileName))
            {
                
                string coordinates;
                //first line must be 2 upper right coordinates
                if ((coordinates = sr.ReadLine()) != null) 
                {
                    inputModel.UpperRight = GetUpperRightCoordinates(coordinates);
                }
                else
                {
                    throw new FileLoadException();
                }

                //if line must contains position info -> true, if line contains commands -> false
                bool mustPosition = true; 
                List<Rover> roverList = new List<Rover>();
                Rover rover = new();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (mustPosition)
                    {
                        rover.StartingPoint = GetRoverPosition(line);
                    }
                    else
                    {
                        rover.CommandList = GetCommandList(line);
                        roverList.Add(rover);
                        rover = new();
                    }
                    mustPosition = !mustPosition;
                }

                //last line must contain command info.
                if (!mustPosition)
                {
                    throw new FormatException("Unexpected end of file format found. Last line must contain commands");
                }
                inputModel.Rover = roverList;
            }

            return inputModel;
        }

        /// <summary>
        /// Get commands string from text line, generate enumlist 
        /// </summary>
        /// <param name="commands">can contain L,M,R only</param>
        /// <returns></returns>
        public List<Commands> GetCommandList(string commands)
        {
            List<Commands> commandList = new List<Commands>();
            foreach (char c in commands)
            {
                Commands command;
                if(Enum.TryParse(c.ToString(), out command))
                {
                    commandList.Add(command);
                }
                else
                {
                    throw new FormatException("Unexpected format found. Commands can be " + Enum.GetNames(typeof(Commands)).ToList());
                }
            }

            return commandList;
        }

        /// <summary>
        /// Get direction string from text line, define matched direction from enum set
        /// </summary>
        /// <param name="currentDirection">can be N,E,W,S</param>
        /// <returns></returns>
        public Directions GetDirection(string currentDirection)
        {
            Directions direction;
            if(Enum.TryParse(currentDirection, out direction))
            {
                return direction;
            }
            else
            {
                throw new FormatException("Unexpected format found. Directions can be " + Enum.GetNames(typeof(Directions)).ToList());
            }

        }

        /// <summary>
        /// Get coordinates string from text line, generate rover node from it.
        /// </summary>
        /// <param name="position">can contains integer x, y coordinates string</param>
        /// <returns></returns>
        public Node GetRoverPosition(string position)
        {
            string[] positionArray = position.Split(" ");
            Node currentPosition = null;
            if (RoverConstants.positionSize == positionArray.Length)
            {
                //values cant be lower than 0
                int x = Convert.ToUInt16(positionArray[RoverConstants.index_X]);
                int y = Convert.ToUInt16(positionArray[RoverConstants.index_Y]);
                int direction = (int)GetDirection(positionArray[RoverConstants.index_Direction]);
                currentPosition = new Node(direction, x, y);
            }
            else
            {
                throw new FormatException("Unexpected format found. Positions must contaion X Y coordinates and single direction values only.");
            }
 


            return currentPosition;
        }

        /// <summary>
        /// Get upper right coordinates string from text file, define max boundaries.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Coordinates GetUpperRightCoordinates(string coordinate)
        {
            Coordinates upperRight = new Coordinates();

            string[] coordinatesArray = coordinate.Split(" ");
            if(RoverConstants.coordinateSize == coordinatesArray.Length)
            {
                upperRight.X = Convert.ToUInt16(coordinatesArray[RoverConstants.index_X]);
                upperRight.Y = Convert.ToUInt16(coordinatesArray[RoverConstants.index_Y]);
            }
            else
            {
                throw new FormatException("Unexpected format found at first line of file. First line must contain X Y values only.");
            }

            return upperRight;
        }

    }
}
