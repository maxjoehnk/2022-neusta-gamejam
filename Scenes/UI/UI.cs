using Godot;
using System.Collections.Generic;

public partial class UI : Control
{
	private readonly PackedScene playerIndicatorScene;
	private Control PlayersRoot => GetNode<Control>("Players");
	private AvailableCardsIndicator CardsIndicator => GetNode<AvailableCardsIndicator>("AvailableCardsIndicator");

	private List<Player> Players => this.GameState?.Players ?? new List<Player>();

	public GameState GameState { get; set; }

	public UI()
	{
		this.playerIndicatorScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/PlayerIndicator.tscn");
	}

	public void Update()
	{
		this.UpdatePlayers();
		this.UpdateAvailableCards();
	}

	private void UpdatePlayers()
	{
		foreach (Node child in PlayersRoot.GetChildren())
		{
			this.PlayersRoot.RemoveChild(child);
		}

		foreach (Player player in this.Players)
		{
			PlayerIndicator playerIndicator = this.playerIndicatorScene.Instantiate<PlayerIndicator>();
			playerIndicator.Player = player;
			this.PlayersRoot.AddChild(playerIndicator);
		}
	}

	public void UpdateAvailableCards()
	{
		this.CardsIndicator.CardsAvailable = this.GameState.Turn.CardsAvailable;
		this.CardsIndicator.CardsLeft = this.GameState.Turn.CardsLeft;
	}
}
