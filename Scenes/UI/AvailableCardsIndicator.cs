using Godot;

public partial class AvailableCardsIndicator : Control
{
	private PackedScene cardIndicatorScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/CardIndicator.tscn");
	
	private FlowContainer Cards => GetNode<FlowContainer>("Cards");
	private ColorRect LineBelow => GetNode<ColorRect>("LineBelow");

	public int CardsAvailable { get; set; }
	public int CardsLeft { get; set; }

	public override void _Process(double delta)
	{
		this.ResetIndicators();
		this.AddMissingIndicators();
		this.UpdateIndicatorStates();
		this.UpdateUnderLineSize();
	}

	private void UpdateUnderLineSize()
	{
		this.LineBelow.Size = new Vector2(this.Cards.Size.x + 4, this.LineBelow.Size.y);
	}

	private void ResetIndicators()
	{
		if (this.CardsAvailable >= this.Cards.GetChildCount())
		{
			return;
		}
		foreach (Node child in this.Cards.GetChildren())
		{
			this.Cards.RemoveChild(child);
		}

		for (int i = 0; i < this.CardsAvailable; i++)
		{
			AddCardIndicator();
		}
	}

	private void AddMissingIndicators()
	{
		if (this.CardsAvailable > this.Cards.GetChildCount())
		{
			int remaining = this.CardsAvailable - this.Cards.GetChildCount();
			for (int i = 0; i < remaining; i++)
			{
				this.AddCardIndicator();
			}
		}
	}

	private void AddCardIndicator()
	{
		CardIndicator indicator = this.cardIndicatorScene.Instantiate<CardIndicator>();
		this.Cards.AddChild(indicator);
	}

	private void UpdateIndicatorStates()
	{
		for (int i = 0; i < this.CardsAvailable; i++)
		{
			CardIndicator indicator = this.Cards.GetChild<CardIndicator>(i);
			indicator.Available = i < this.CardsLeft;
		}
	}
}
