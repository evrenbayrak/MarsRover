using MarsRover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Rover
    {
        private Node startingPoint;
        private List<Commands> commandList;

        public Node StartingPoint { get => startingPoint; set => startingPoint = value; }
        internal List<Commands> CommandList { get => commandList; set => commandList = value; }

        public Rover()
        {

        }
    }
}
