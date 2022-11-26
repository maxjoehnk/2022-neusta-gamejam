using System;

using Godot;

public abstract partial class Card : MeshInstance3D
{
	private readonly Doors doors;

	public Card(Doors doors)
	{
		this.doors = doors;
	}

	/// <summary>
	/// Returns whether a player can leave this card into the given <paramref name="direction"/>.
	/// </summary>
	public bool CanLeaveTo(Direction direction)
	{
		return direction switch
		{
			Direction.Up => this.doors.Top,
			Direction.Down => this.doors.Bottom,
			Direction.Left => this.doors.Left,
			Direction.Right => this.doors.Right,
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};
	}

	/// <summary>
	/// Returns whether a player can enter this card from the given <paramref name="direction"/>.
	/// </summary>
	public bool CanEnterFrom(Direction direction)
	{
		return direction switch
		{
			Direction.Up => this.doors.Bottom,
			Direction.Down => this.doors.Top,
			Direction.Left => this.doors.Right,
			Direction.Right => this.doors.Left,
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};
	}
}
