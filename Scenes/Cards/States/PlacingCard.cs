using Godot;

public partial class PlacingCard : Node3D
{
	[Export] private int rayLength = 40;
	[Export] private double rotationAnimationDuration = 0.1;

	public int Orientation { get; private set; }
	
	public Position MapPosition { get; set; }

	public Card Card { get; private set; }

	public PlacingCard()
	{
		this.Name = "PlacingCard";
	}

	public void FollowMouse(Camera3D camera, InputEventMouseMotion inputEvent)
	{
		this.Position = camera.ProjectPosition(inputEvent.GlobalPosition, this.rayLength);
	}

	public void RotateRight()
	{
		this.Orientation -= 1;
		while (this.Orientation < 0)
		{
			this.Orientation += 4;
		}
		this.UpdateRotation();
	}

	public void RotateLeft()
	{
		this.Orientation += 1;
		while (this.Orientation > 3)
		{
			this.Orientation -= 4;
		}
		this.UpdateRotation();
	}

	private void UpdateRotation()
	{
		Tween tween = this.CreateTween().SetTrans(Tween.TransitionType.Linear);
		int rotation = this.Orientation * 90;
		Vector3 rotationVector = new Vector3 { y = Mathf.DegToRad(rotation) };
		tween.TweenProperty(this, "rotation", rotationVector, this.rotationAnimationDuration);
	}

	public void SetCard(Card card)
	{
		this.AddChild(card);
		this.Card = card;
	}
}
