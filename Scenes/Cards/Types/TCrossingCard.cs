using Godot;

public class TCrossingCard : ICardType
{
	public Texture2D Texture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/TCrossing.jpg");
	
	public Doors doors => new Doors
	{
		Left = true,
		Right = true,
		Bottom = true
	};
}

public class TCrossingCardFactory : ICardTypeFactory
{
	public int Probability => 1;
	
	public ICardType BuildCardType()
	{
		return new TCrossingCard();
	}
}
