using Godot;

public partial class CornerCard : Card
{
	public CornerCard() : base(new Doors
	{
		Top = true,
		Right = true
	})
	{
	}
}

public class CornerCardFactory : ICardFactory
{
	readonly PackedScene scene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/CornerCard.tscn");
		
	public int Probability => 1;
	
	public Card CreateCard()
	{
		return this.scene.Instantiate<CornerCard>();
	}
}
