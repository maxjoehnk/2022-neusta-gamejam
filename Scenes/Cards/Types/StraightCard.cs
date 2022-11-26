using Godot;

public class StraightCard : ICardType
{
	public Texture2D Texture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Straight.jpg");
	
	public Doors doors => new Doors
	{
		Left = true,
		Right = true
	};
}

public class StraightCardFactory : ICardTypeFactory
{
	public int Probability => 1;
	
	public ICardType BuildCardType() {
		return new StraightCard();
	}
}
