using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Enums
{
    public enum Commands
    {
        [Description("Left")]
        L,
        [Description("Right")]
        R,
        [Description("Move")]
        M
    }

    public enum Directions
    {
        [Description("North")]
        N,
        [Description("East")]
        E,
        [Description("South")]
        S,
        [Description("West")]
        W
    }
    public enum HorizontalMove
    {
        UP = 1,
        STAND = 0,
        DOWN = -1
    }

    public enum VerticalMove
    {
        RIGHT = 1,
        STAND = 0,
        LEFT = -1
    }
}
