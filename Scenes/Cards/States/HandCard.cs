using Godot;

public partial class HandCard : Node3D, ICard
{
    private readonly CardOverlay cardOverlay;

    [Export] private double animationDuration = 0.1;
    [Export] private Vector3 highlightOffset = new Vector3(0, -5, -5);

    public MeshInstance3D Mesh => GetNode<MeshInstance3D>("Mesh");

    public Card Card { get; private set; }

    public bool Highlight { get; set; }

    public bool Selected { get; set; }

    public HandCard()
    {
        Card = new Card();
		this.cardOverlay = new CardOverlay(this);
    }

    public override void _Process(double delta)
    {
        Vector3 position = (Highlight || Selected) ? this.highlightOffset : Vector3.Zero;
        this.CreateTween().TweenProperty(this.Mesh, "position", position,
        this.animationDuration);
    }

	public void SetCard(Card card)
	{
		if (this.Card == card)
		{
			return;
		}
		this.Card = card;
		this.cardOverlay.ApplyTypeTexture();
		this.cardOverlay.ApplyDecalTexture();
		this.cardOverlay.ApplyEffectTexture();
		this.cardOverlay.ApplyModifierTexture();
	}
}