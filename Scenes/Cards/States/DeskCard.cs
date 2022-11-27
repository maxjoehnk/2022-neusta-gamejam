using Godot;

public partial class DeskCard : Node3D
{
	[Export] private double MoveAnimationDuration = 0.1;
	
	private bool despawn;
	private Node3D cardEffectMarker;

	public BaseCard BaseCard { get; private set; }

	private AudioStreamPlayer3D EnterSound => GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");

	public Position MapPosition { get; set; }

	public int Orientation { get; set; }
	
	public bool Hovering { get; set; }

	public DeskCard()
	{
		this.MapPosition = new Position();
	}

	public override void _Ready()
	{
		ApplyPosition();
	}

	public void MoveTo(int x, int y)
	{
		this.MoveTo(new Position(x, y));
	}

	public void MoveTo(Position targetPosition)
	{
		GD.Print($"Move from {MapPosition} to {targetPosition}");
		this.MapPosition = targetPosition;
		UpdateMetadataPosition();
		Vector3 position = MapPositionToGlobalPosition();
		Tween tween = this.CreateTween();
		tween.TweenProperty(this, "position", position, MoveAnimationDuration);
		tween.TweenCallback(new Callable(this, nameof(Exit)));
	}

	private void ApplyPosition()
	{
		UpdateMetadataPosition();
		Vector3 position = MapPositionToGlobalPosition();
		this.Position = position;
		int rotation = this.Orientation * 90;
		this.Rotation = new Vector3 { y = Mathf.DegToRad(rotation) };
	}

	private void UpdateMetadataPosition()
	{
		this.SetMeta("X", this.MapPosition.X);
		this.SetMeta("Y", this.MapPosition.Y);
	}

	private Vector3 MapPositionToGlobalPosition()
	{
		Vector3 position = this.Position;
		position.x = this.MapPosition.X * Constants.CardSize;
		position.z = this.MapPosition.Y * Constants.CardSize;
		
		return position;
	}

	public void SetCard(BaseCard card)
	{
		if (this.cardEffectMarker != null)
		{
			this.RemoveChild(this.cardEffectMarker);
			this.cardEffectMarker.QueueFree();
		}
		this.AddChild(card);
		this.BaseCard = card;
		this.UpdateDescription();
		this.cardEffectMarker = card.CardEffect?.CreateMarker();
		if (this.cardEffectMarker != null)
		{
			this.AddChild(this.cardEffectMarker);
		}
	}

	public bool HasDoor(Side side)
	{
		Side actualSide = side.Rotate(this.Orientation);
		
		GD.Print($"HasDoor {side} * {Orientation} => {actualSide}");

		return this.BaseCard.HasDoor(actualSide);
	}

	public void Despawn()
	{
		this.despawn = true;
	}

	private void Exit()
	{
		if (this.despawn)
		{
			this.QueueFree();
		}
	}

	public void PlayEnterSound()
	{
		this.EnterSound.Play();
	}

	private void UpdateDescription()
	{
		this.EditorDescription = $"Card ({BaseCard.CardType}, {BaseCard.CardEffect})";
	}
}
