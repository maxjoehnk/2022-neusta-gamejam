	using System;

using Godot;
using System.Collections.Generic;

using Godot.Collections;

public partial class Game : Node3D
{
	private const int RayLength = 1000;
	private const int Rows = 10;
	private const int Columns = 10;
	
	private Node3D CardsRoot => GetNode<Node3D>("Cards");
	private Node3D PlayersRoot => GetNode<Node3D>("Players");
	private UI UiRoot => GetNode<UI>("UI");
	private Camera3D Camera => GetNode<Camera3D>("Camera");
	private Node3D PlacementIndicator => GetNode<Node3D>("PlacementIndicator");
	private AudioStreamPlayer StoneMovingSound => GetNode<AudioStreamPlayer>("Sounds/StoneMoving");
	
	private CardFactory cardFactory;
	private PlayerFactory playerFactory;

	private readonly Map map = new Map();
	private readonly List<Player> players = new List<Player>();

	private Player ActivePlayer => this.players[this.turn];

	private int turn;

	private PlacingCard placingCard;
	
	public override void _Ready()
	{
		this.cardFactory = new CardFactory();
		this.playerFactory = new PlayerFactory();
		this.Reset();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("next"))
		{
			this.NextTurn();
		}

		if (Input.IsActionJustPressed("reset"))
		{
			this.Reset();
		}

		this.RotateCard();
		this.PlaceCard();
		this.PlayerMovement();
		this.UpdateActivePlayer();
	}

	private void PlaceCard()
	{
		if (Input.IsActionJustPressed("place"))
		{
			DeskCard deskCard = this.cardFactory.FromHand(this.placingCard);
			this.CardsRoot.AddChild(deskCard);
			this.map.PushCard(this.placingCard, deskCard);
			this.placingCard.QueueFree();
			this.placingCard = null;
			this.PlacementIndicator.Hide();
			deskCard.PlayEnterSound();
			this.StoneMovingSound.Play();
		}
	}

	private void RotateCard()
	{
		if (Input.IsActionJustPressed("rotate_right") && !Input.IsActionJustPressed("rotate_left"))
		{
			this.placingCard?.RotateRight();
		}

		if (Input.IsActionJustPressed("rotate_left"))
		{
			this.placingCard?.RotateLeft();
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion motion)
		{
			this.placingCard?.FollowMouse(this.Camera, motion);
			Vector3 from = this.Camera.ProjectRayOrigin(motion.GlobalPosition);
			Vector3 to = from + Camera.ProjectRayNormal(motion.GlobalPosition) * RayLength;

			Dictionary intersectRay = GetWorld3d().DirectSpaceState.IntersectRay(new PhysicsRayQueryParameters3D
			{
				From = from,
				To = to,
			});
			if (intersectRay.ContainsKey("collider"))
			{
				Vector3 position = (Vector3)intersectRay["position"];
				int x = (int)Math.Round(position.x / Constants.CardSize);
				int y = (int)Math.Round(position.z / Constants.CardSize);
				Position mapPosition = new Position(x, y);
				Position boundaryPosition = this.map.OutsideBoundaries(mapPosition);
				PlacingCard card = this.placingCard;
				if (card != null)
				{
					card.MapPosition = boundaryPosition;
					this.PlacementIndicator.Position = new Vector3(boundaryPosition.X, 0, boundaryPosition.Y) * Constants.CardSize;
					this.PlacementIndicator.Visible = true;
				}
				else
				{
					this.PlacementIndicator.Visible = false;
				}
			}
		}
	}

	private void Reset()
	{
		if (this.placingCard != null)
		{
			this.RemoveChild(this.placingCard);
		}
		this.turn = 0;
		foreach (Node child in this.CardsRoot.GetChildren())
		{
			this.CardsRoot.RemoveChild(child);
		}
		foreach (Node child in this.PlayersRoot.GetChildren())
		{
			this.PlayersRoot.RemoveChild(child);
		}
		this.players.Clear();
		this.FillCards();
		this.FillPlayers();
		DrawCard();
	}

	private void DrawCard()
	{
		Card card = this.cardFactory.CreateCard();
		this.placingCard = this.cardFactory.CreatePlacingCard(card);
		this.AddChild(this.placingCard);
		this.PlacementIndicator.Show();
	}

	private void FillPlayers()
	{
		for (int i = 0; i < 2; i++)
		{
			Player player = this.playerFactory.CreatePlayer(i);
			this.players.Add(player);
			this.PlayersRoot.AddChild(player);
		}

		this.UiRoot.Players = this.players;
	}

	private void FillCards()
	{
		List<DeskCard> cards = new List<DeskCard>();
		for (int x = 0; x < Columns; x++)
		{
			for (int y = 0; y < Rows; y++)
			{
				Card card = this.cardFactory.CreateCard();
				DeskCard deskCard = this.cardFactory.CreateDeskCard(x, y, card);
				
				cards.Add(deskCard);
				this.CardsRoot.AddChild(deskCard);
			}
		}
		this.map.SetCards(cards);
	}

	private void PlayerMovement()
	{
		Position position = this.ActivePlayer.MapPosition.Duplicate();
		if (Input.IsActionJustPressed("left"))
		{
			position.X -= 1;
		}
		if (Input.IsActionJustPressed("right"))
		{
			position.X += 1;
		}
		if (Input.IsActionJustPressed("up"))
		{
			position.Y -= 1;
		}
		if (Input.IsActionJustPressed("down"))
		{
			position.Y += 1;
		}

		if (position == this.ActivePlayer.MapPosition)
		{
			return;
		}

		if (!this.map.CanMoveTo(this.ActivePlayer.MapPosition, position))
		{
			return;
		}

		this.ActivePlayer.MapPosition = position;
		this.ActivePlayer.UpdatePosition();
	}

	private void NextTurn()
	{
		this.turn++;
		if (this.turn >= this.players.Count)
		{
			this.turn = 0;
		}
		this.DrawCard();
	}

	private void UpdateActivePlayer()
	{
		for (int i = 0; i < this.players.Count; i++)
		{
			Player player = this.players[i];
			player.Active = this.turn == i;
		}
	}
}
