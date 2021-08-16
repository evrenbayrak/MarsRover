using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Node : Coordinates 
    {

        private int currentDirection;
        private Node right;
        private Node left;
        public Node Right { get => right; set => right = value; }
        public Node Left { get => left; set => left = value; }
        public int CurrentDirection { get => currentDirection; set => currentDirection = value; }

        public Node(int currentDirection, int x, int y): base(x, y)
        {
            this.currentDirection = currentDirection;
        }

        public void SetLink(Node left, Node right)
        {
            this.left = left;
            this.right = right;
        }

    }
}
