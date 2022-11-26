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
        int result = (int)direction + rotation;

        while (result > (int) last)
        {
            result -= (int)last;
        }

        if (result == 0)
        {
            throw new ApplicationException(
                $"Direction rotate would return invalid result.\nBefore: {direction}\nRotation: {rotation}\nHighest Valid Value: {last}\nResult: {result}");
        }
        return (Direction)result;
    }

    public static bool IsHorizontal(this Direction direction)
    {
        return direction == Direction.Left || direction == Direction.Right;
    }

    public static bool IsVertical(this Direction direction)
    {
        return direction == Direction.Up || direction == Direction.Down;
    }
}