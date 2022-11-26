using Godot;

public class StraightCard : ICardType
{
	public Texture2D Texture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardTypes/Straight.jpg");
	public Texture2D DecalTexture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardDecals/MoosStraight.jpg");
	
	public Doors doors => new Doors
	{
		Top = true,
		Bottom = true
	};
}

public class StraightCardFactory : ICardTypeFactory
{
	public int Probability => 1;
	
	public ICardType BuildCardType() {
		return new StraightCard();
	}
}
