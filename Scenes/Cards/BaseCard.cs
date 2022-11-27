using System;

using Godot;

public partial class BaseCard : Node3D, ICard
{
	private readonly CardOverlay cardOverlay;
	
	public MeshInstance3D Mesh => GetNode<MeshInstance3D>("Mesh");
	
	public Card Card { get; private set; }

	private Doors Doors => Card.Effect?.ModifyDoors(this.Card.Type.doors) ?? this.Card.Type.doors;

	public BaseCard()
	{
		this.Card = new Card();
		this.cardOverlay = new CardOverlay(this);
	}

	public override void _Ready()
	{
		this.cardOverlay.ApplyTypeTexture();
		this.cardOverlay.ApplyDecalTexture();
		this.cardOverlay.ApplyEffectTexture();
		this.cardOverlay.ApplyModifierTexture();
		this.Card.Effect?.Ready(this);
	}

	public override void _Process(double delta)
	{
		this.Card.Effect?.Process(this, delta);
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

	public CardResult OnEnter(GameState gameState, Direction direction)
	{
		CardResult cardResult = new CardResult();

		CardResult effectResult = this.Card.Effect?.OnEnter(gameState, direction);
		CardResult modifierResult = this.Card.Modifier?.OnEnter(gameState, direction);

		return cardResult.Join(effectResult).Join(modifierResult);
	}

	public void SetCard(Card card)
	{
		this.Card = card;
		this.cardOverlay.ApplyTypeTexture();
		this.cardOverlay.ApplyDecalTexture();
		this.cardOverlay.ApplyEffectTexture();
		this.cardOverlay.ApplyModifierTexture();
	}
}
