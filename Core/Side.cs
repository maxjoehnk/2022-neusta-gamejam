using System;
using System.Linq;

public enum Side
{
    Bottom = 1,
    Left = 2,
    Top = 3,
    Right = 4,
}

public static class SideExtensions
{
    public static Side Rotate(this Side side, int rotation)
    {
        Side last = Enum.GetValues<Side>().Last();
        int result = (int)side + rotation;

        while (result > (int) last)
        {
            result -= (int)last;
        }

        if (result == 0)
        {
            throw new ApplicationException(
                $"Side rotate would return invalid result.\nBefore: {side}\nRotation: {rotation}\nHighest Valid Value: {last}\nResult: {result}");
        }
        return (Side)result;
    }
}