using Godot;

public partial class TCrossingCard : Card
{
	public TCrossingCard() : base(new Doors
	{
		Left = true,
		Right = true,
		Bottom = true
	})
	{
	}
}

public class TCrossingCardFactory : ICardFactory
{
	readonly PackedScene scene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/TCrossingCard.tscn");
		
	public int Probability => 1;
	
	public Card CreateCard()
	{
		return this.scene.Instantiate<TCrossingCard>();
	}
}
