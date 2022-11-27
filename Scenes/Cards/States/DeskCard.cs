using Godot;

public partial class DeskCard : Node3D
{
	[Export] private double MoveAnimationDuration = 0.1;
	
	private bool despawn;
	private Node3D cardEffectMarker;
	private Node3D cardModifierMarker;

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

	public void MoveTo(Position targetPosition, Tween tween)
	{
		GD.Print($"Move from {MapPosition} to {targetPosition}");
		this.MapPosition = targetPosition;
		UpdateMetadataPosition();
		Vector3 position = MapPositionToGlobalPosition();
		PropertyTweener tweener = tween.TweenProperty(this, "position", position, this.MoveAnimationDuration);
		tweener.Finished += this.Exit;
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
		if (this.cardModifierMarker != null)
		{
			this.RemoveChild(this.cardModifierMarker);
			this.cardModifierMarker.QueueFree();
		}
		this.AddChild(card);
		this.BaseCard = card;
		this.UpdateDescription();
		this.cardEffectMarker = card.Card.Effect?.CreateMarker();
		if (this.cardEffectMarker != null)
		{
			this.AddChild(this.cardEffectMarker);
		}
		this.cardModifierMarker = card.Card.Modifier?.CreateMarker();
		if (this.cardModifierMarker != null)
		{
			this.AddChild(this.cardModifierMarker);
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
		this.EditorDescription = $"Card ({BaseCard.Card.Type}, {BaseCard.Card.Effect}, {BaseCard.Card.Modifier})";
	}
}
