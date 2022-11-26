using Godot;

public partial class StraightCard : Card
{
	public StraightCard() : base(new Doors
	{
		Left = true,
		Right = true
	})
	{
	}
}

public class StraightCardFactory : ICardFactory
{
	readonly PackedScene scene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/StraightCard.tscn");
		
	public int Probability => 1;
	
	public Card CreateCard()
	{
		return this.scene.Instantiate<StraightCard>();
	}
}
