using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class DoubleLinkedList
    {
        private Node head;

        public DoubleLinkedList()
        {
            head = null;
        }

        public Node Head { get => head; set => head = value; }


    }
}
