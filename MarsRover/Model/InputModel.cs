using MarsRover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class InputModel
    {

        private Coordinates upperRight;
        private List<Rover> rover;

        public Coordinates UpperRight { get => upperRight; set => upperRight = value; }
        public List<Rover> Rover { get => rover; set => rover = value; }

        public InputModel()
        {

        }


    }
}
