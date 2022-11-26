using System;

using Godot;

public partial class Card : Node3D
{
	private MeshInstance3D Mesh => GetNode<MeshInstance3D>("Mesh");
	
	public ICardType CardType { get; private set; }

	private Doors Doors => this.CardType.doors;

	private StandardMaterial3D CardTypeMaterial
	{
		get => (StandardMaterial3D)this.Mesh.MaterialOverlay;
		set => this.Mesh.MaterialOverlay = value;
	}

	private StandardMaterial3D CardModifierMaterial
	{
		get => (StandardMaterial3D)this.CardTypeMaterial.NextPass;
		set => this.CardTypeMaterial.NextPass = value;
	}

	/// <summary>
	/// Returns whether a player can leave this card into the given <paramref name="direction"/>.
	/// </summary>
	public bool CanLeaveTo(Direction direction)
	{
		return direction switch
		{
			Direction.Up => this.Doors.Top,
			Direction.Down => this.Doors.Bottom,
			Direction.Left => this.Doors.Left,
			Direction.Right => this.Doors.Right,
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
			Direction.Up => this.Doors.Bottom,
			Direction.Down => this.Doors.Top,
			Direction.Left => this.Doors.Right,
			Direction.Right => this.Doors.Left,
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};
	}

	public void SetCardType(ICardType cardType)
	{
		this.CardType = cardType;
		StandardMaterial3D material = (StandardMaterial3D)this.CardTypeMaterial.Duplicate(true);
		material.AlbedoTexture = cardType.Texture;
		this.Mesh.MaterialOverlay = material;
	}
}
