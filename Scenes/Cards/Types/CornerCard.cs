using Godot;

public class CornerCard : ICardType
{
	public Texture2D Texture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardTypes/Corner.jpg");
	
	public Texture2D DecalTexture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardDecals/MoosCorner.jpg");
    
	public Doors doors => new Doors
	{
		Top = true,
		Right = true
	};
}

public class CornerCardFactory : ICardTypeFactory
{
	public int Probability => 2;
	
	public ICardType BuildCardType()
	{
		return new CornerCard();
	}
}
