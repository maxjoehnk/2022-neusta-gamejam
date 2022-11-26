using Godot;

public partial class DeskCard : Node3D
{
	[Export] private double MoveAnimationDuration = 0.1;
	
	private bool despawn;
	
	private Card card;

	private AudioStreamPlayer3D EnterSound => GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");

	public Position MapPosition { get; set; }

	public int Orientation { get; set; }
	
	public bool Hovering { get; set; }

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

	public void SetCard(Card card)
	{
		this.AddChild(card);
		this.card = card;
		this.EditorDescription = card.GetType().Name;
	}

	public bool CanLeaveTo(Direction direction)
	{
		Direction actualDirection = direction.Rotate(this.Orientation);
		return this.card.CanLeaveTo(actualDirection);
	}

	public bool CanEnterFrom(Direction direction)
	{
		Direction actualDirection = direction.Rotate(this.Orientation);
		return this.card.CanEnterFrom(actualDirection);
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
}
