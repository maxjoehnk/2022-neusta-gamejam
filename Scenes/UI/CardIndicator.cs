using Godot;

public partial class CardIndicator : TextureRect
{
	private Texture2D availableTexture = ResourceLoader.Load<Texture2D>("res://Assets/Textures/UI/CardAvailable.png");
	private Texture2D usedTexture = ResourceLoader.Load<Texture2D>("res://Assets/Textures/UI/CardUsed.png");

	public bool Available { get; set; }

	public override void _Process(double delta)
	{
		this.SetMeta("Available", this.Available);
		this.Texture = Available ? this.availableTexture : this.usedTexture;
	}
}
