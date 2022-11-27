using System.Collections.Generic;
using System.Linq;

using Godot;

public partial class WinScreen : Panel
{
	private PackedScene playerListingScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/PlayerIndicator.tscn");
	private Container PlayerList => GetNode<Container>("Container/PlayerList");

	private Button ExitButton => GetNode<Button>("Buttons/Exit");
	private Button RestartButton => GetNode<Button>("Buttons/Restart");

	[Signal]
	public delegate void RestartGameEventHandler();
	
	public GameState GameState { get; set; }

	public override void _Ready()
	{
		this.PlayerList.ClearChildren();
		this.ExitButton.Connect("pressed", new Callable(this, nameof(this.OnExit)));
		this.RestartButton.Connect("pressed", new Callable(this, nameof(this.OnRestart)));
	}
	
	public void UpdatePlayers()
	{
		this.PlayerList.ClearChildren();
		IEnumerable<Player> players = this.GameState.Players.OrderByDescending(p => p.Coins);
		foreach (Player player in players)
		{
			player.Active = false;
			PlayerIndicator playerIndicator = this.playerListingScene.Instantiate<PlayerIndicator>();
			playerIndicator.Player = player;
				
			this.PlayerList.AddChild(playerIndicator);
		}
	}

	private void OnExit()
	{
		GetTree().Quit();
	}

	private void OnRestart()
	{
		EmitSignal(nameof(RestartGame));
	}
}
