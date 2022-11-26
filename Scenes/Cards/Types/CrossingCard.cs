using Godot;

public class CrossingCard : ICardType
{
	public Texture2D Texture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardTypes/Crossing.jpg");
	
	public Texture2D DecalTexture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardDecals/MoosCrossing.jpg");
	
	public Doors doors => new Doors
	{
		Left = true,
		Right = true,
		Top = true,
		Bottom = true
	};
}

public class CrossingCardFactory : ICardTypeFactory
{
	
	public int Probability => 1;
	
	public ICardType BuildCardType()
	{
		return new CrossingCard();
	}
}
