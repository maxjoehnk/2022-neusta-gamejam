using Godot;

public partial class CrossingCard : Card
{
	public CrossingCard() : base(new Doors
	{
		Left = true,
		Right = true,
		Top = true,
		Bottom = true
	})
	{
	}
}

public class CrossingCardFactory : ICardFactory
{
	readonly PackedScene scene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/CrossingCard.tscn");
		
	public int Probability => 1;
	
	public Card CreateCard()
	{
		return this.scene.Instantiate<CrossingCard>();
	}
}
