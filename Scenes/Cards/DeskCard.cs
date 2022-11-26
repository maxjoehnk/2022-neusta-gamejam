using Godot;

public partial class DeskCard : Node3D
{
	private Card card;

	public Position MapPosition { get; set; }

	public int Orientation { get; set; }
	
	public bool Hovering { get; set; }

	public override void _Process(double delta)
	{
		UpdatePosition();
	}

	private void UpdatePosition()
	{
		Transform3D transform3D = this.Transform;
		transform3D.origin.x = this.MapPosition.X * Constants.CardSize;
		transform3D.origin.z = this.MapPosition.Y * Constants.CardSize;
		this.Transform = transform3D;
		int rotation = this.Orientation * 90;
		this.Rotation = new Vector3 { y = Mathf.DegToRad(rotation) };
	}

	public void SetCard(Card card)
	{
		this.AddChild(card);
		this.card = card;
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
}
