using Godot;
using System;

public partial class TitleScreen : Control
{
	private Button ExitButton => GetNode<Button>("Buttons/Exit");
	private Button StartButton => GetNode<Button>("Buttons/Start");
	
	public override void _Ready()
	{
		this.ExitButton.Pressed += this.OnExit;
		this.StartButton.Pressed += this.OnStart;
	}

	private void OnExit()
	{
		GetTree().Quit();
	}

	private void OnStart()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Game.tscn");
	}
}
