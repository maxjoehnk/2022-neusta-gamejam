using System;
using System.Linq;

public enum Direction
{
    Up = 1,
    Right = 2,
    Down = 3,
    Left = 4,
}

public static class DirectionExtensions
{
    public static Direction Rotate(this Direction direction, int rotation)
    {
        Direction last = Enum.GetValues<Direction>().Last();
        int result = (int)(direction) + rotation;

        while (result > (int) last)
        {
            result -= (int)last;
        }
        return (Direction)result;
    }
}