using NUnit.Framework;

namespace Tests;

[TestFixture]
public class DirectionTests
{
    [TestCase(Direction.Up, Direction.Right, 1)]
    [TestCase(Direction.Right, Direction.Down, 1)]
    [TestCase(Direction.Down, Direction.Left, 1)]
    [TestCase(Direction.Left, Direction.Up, 1)]
    [TestCase(Direction.Up, Direction.Down, 2)]
    [TestCase(Direction.Right, Direction.Left, 2)]
    [TestCase(Direction.Down, Direction.Up, 2)]
    [TestCase(Direction.Left, Direction.Right, 2)]
    public void RotateShouldRotateDirection(Direction direction, Direction expected, int rotation)
    {
        Direction result = direction.Rotate(rotation);
        
        Assert.AreEqual(expected, result);
    }

    [TestCase(Direction.Up)]
    [TestCase(Direction.Right)]
    [TestCase(Direction.Down)]
    [TestCase(Direction.Left)]
    public void RotateShouldKeepDirectionWhenRotationIsZero(Direction direction)
    {
        Direction result = direction.Rotate(0);
        
        Assert.AreEqual(direction, result);
    }
}