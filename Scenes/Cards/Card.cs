using System;

using Godot;

public partial class Card : Node3D
{
	private MeshInstance3D Mesh => GetNode<MeshInstance3D>("Mesh");
	
	public ICardType CardType { get; private set; }
	public ICardEffect CardEffect { get; private set; }

	private Doors Doors => CardEffect?.ModifyDoors(this.CardType.doors) ?? this.CardType.doors;

	private StandardMaterial3D CardTypeMaterial
	{
		get => (StandardMaterial3D)this.Mesh.MaterialOverlay;
		set => this.Mesh.MaterialOverlay = value;
	}

	private StandardMaterial3D CardEffectMaterial
	{
		get => (StandardMaterial3D)this.CardTypeMaterial.NextPass;
		set => this.CardTypeMaterial.NextPass = value;
	}

	private StandardMaterial3D CardDecalMaterial
	{
		get => (StandardMaterial3D)this.CardEffectMaterial.NextPass;
		set => this.CardEffectMaterial.NextPass = value;
	}

	public override void _Ready()
	{
		this.ApplyTypeTexture();
		this.ApplyDecalTexture();
		this.ApplyEffectTexture();
		this.CardEffect?.Ready(this);
	}

	public override void _Process(double delta)
	{
		this.CardEffect?.Process(this, delta);
	}

	/// <summary>
	/// Returns whether a player can leave this card into the given <paramref name="side"/>.
	/// </summary>
	public bool HasDoor(Side side)
	{
		return side switch
		{
			Side.Top => this.Doors.Top,
			Side.Bottom => this.Doors.Bottom,
			Side.Left => this.Doors.Left,
			Side.Right => this.Doors.Right,
			_ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
		};
	}

	public void OnEnter(Player player, Map map)
	{
		this.CardEffect?.OnEnter(player, map);
	}

	public void SetCardType(ICardType cardType)
	{
		this.CardType = cardType;
		this.ApplyTypeTexture();
		this.ApplyDecalTexture();
	}

	public void SetCardEffect(ICardEffect cardEffect)
	{
		this.CardEffect = cardEffect;
		ApplyEffectTexture();
	}

	private void ApplyEffectTexture()
	{
		StandardMaterial3D material = (StandardMaterial3D)this.CardEffectMaterial.Duplicate(true);
		material.AlbedoTexture = this.CardEffect?.Texture;
		this.CardEffectMaterial = material;
	}

	private void ApplyDecalTexture()
	{
		StandardMaterial3D material = (StandardMaterial3D)this.CardDecalMaterial.Duplicate(true);
		material.AlbedoTexture = this.CardType?.DecalTexture;
		this.CardDecalMaterial = material;
	}

	private void ApplyTypeTexture()
	{
		StandardMaterial3D material = (StandardMaterial3D)this.CardTypeMaterial.Duplicate(true);
		material.AlbedoTexture = this.CardType?.Texture;
		this.CardTypeMaterial = material;
	}

}
