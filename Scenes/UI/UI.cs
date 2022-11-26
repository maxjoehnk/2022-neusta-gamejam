using Godot;

using System;
using System.Collections.Generic;

public partial class UI : Control
{
	private readonly PackedScene playerIndicatorScene;
	private Control PlayersRoot => GetNode<Control>("Players");

	private List<Player> players = new List<Player>();

	public List<Player> Players
	{
		get => this.players;
		set
		{
			this.players = value;
			UpdatePlayers();
		}
	}

	public UI()
	{
		this.playerIndicatorScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/PlayerIndicator.tscn");
	}

	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		this.UpdatePlayers();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _Process(double delta)
	{
	}

	private void UpdatePlayers()
	{
		foreach (Node child in PlayersRoot.GetChildren())
		{
			this.PlayersRoot.RemoveChild(child);
		}

		foreach (Player player in this.players)
		{
			PlayerIndicator playerIndicator = this.playerIndicatorScene.Instantiate<PlayerIndicator>();
			playerIndicator.Player = player;
			this.PlayersRoot.AddChild(playerIndicator);
		}
	}
}
