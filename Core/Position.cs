using Godot;

public record Position
{
	public int X { get; set; }

	public int Y { get; set; }

	public Position()
	{
	}

	public Position(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	public Direction GetDirection(Position to)
	{
		int x = this.X - to.X;
		int y = this.Y - to.Y;

		Direction? direction = (x, y) switch
		{
			(> 0, 0) => Direction.Left,
			(< 0, 0) => Direction.Right,
			(0, > 0) => Direction.Up,
			(0, < 0) => Direction.Down,
			_ => null,
		};

		if (direction == null)
		{
			GD.PushWarning("Can't calculate direction from movement");
			return Direction.Right;
		}

		return (Direction)direction;
	}

	public Position Duplicate()
	{
		return new Position
		{
			X = this.X,
			Y = this.Y,
		};
	}
}
