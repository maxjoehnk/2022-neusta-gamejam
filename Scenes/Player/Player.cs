using Godot;

public partial class Player : MeshInstance3D
{
	[Export] private double movementAnimationDuration = 0.2;
	
	private StandardMaterial3D Material => (StandardMaterial3D)this.MaterialOverride;

	public int PlayerNr { get; set; }
	public bool Active { get; set; }

	public Position MapPosition { get; set; }

	public Color Color { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.MaterialOverride = (Material)this.Material.Duplicate();
		SetPosition();
	}

	private void SetPosition()
	{
		Transform3D transform3D = this.Transform;
		transform3D.origin.x = this.MapPosition.X * Constants.CardSize;
		transform3D.origin.z = this.MapPosition.Y * Constants.CardSize;
		this.Transform = transform3D;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateColor();
	}

	private void UpdateColor()
	{
		this.Material.AlbedoColor = this.Color;
	}

	public void UpdatePosition()
	{
		Tween tween = this.CreateTween().SetTrans(Tween.TransitionType.Linear);
		Vector3 translation = new Vector3
		{
			x = this.MapPosition.X * Constants.CardSize,
			z = this.MapPosition.Y * Constants.CardSize,
			y = 0,
		};
		tween.TweenProperty(this, "position", translation, movementAnimationDuration).FromCurrent();
	}

	public override string ToString()
	{
		return $"Player {{ Name = {Name}, Position = {MapPosition}, Active = {Active} }}";
	}
}
