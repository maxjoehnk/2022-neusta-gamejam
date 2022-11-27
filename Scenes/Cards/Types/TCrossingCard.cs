using Godot;

public class TCrossingCard : ICardType
{
	public Texture2D Texture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardTypes/TCrossing.jpg");
	public Texture2D DecalTexture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardDecals/MoosTCrossing.jpg");
	
	public Doors doors => new Doors
	{
		Top = true,
		Right = true,
		Bottom = true
	};
}

public class TCrossingCardFactory : ICardTypeFactory
{
	public int Probability => 2;
	
	public ICardType BuildCardType()
	{
		return new TCrossingCard();
	}
}
