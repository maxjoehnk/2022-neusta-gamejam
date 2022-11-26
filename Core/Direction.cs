using System;
using System.Linq;

using Godot;

public enum Direction
{
    Down = 1,
    Left = 2,
    Up = 3,
    Right = 4,
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
        return direction is Direction.Left or Direction.Right;
    }

    public static bool IsVertical(this Direction direction)
    {
        return direction is Direction.Up or Direction.Down;
    }

    public static Vector3 ToRotation(this Direction direction)
    {
        int rotation = direction switch
        {
            Direction.Down => 0,
            Direction.Left => 270,
            Direction.Up => 180,
            Direction.Right => 90,
        };
		Vector3 rotationVector = new Vector3 { y = Mathf.DegToRad(rotation) };

        return rotationVector;
    }

    public static Side Leave(this Direction direction)
    {
        return direction switch
        {
            Direction.Down => Side.Bottom,
            Direction.Up => Side.Top,
            Direction.Left => Side.Left,
            Direction.Right => Side.Right,
        };
    }

    public static Side Enter(this Direction direction)
    {
        return direction switch
        {
            Direction.Down => Side.Top,
            Direction.Up => Side.Bottom,
            Direction.Left => Side.Right,
            Direction.Right => Side.Left,
        };
    }
}