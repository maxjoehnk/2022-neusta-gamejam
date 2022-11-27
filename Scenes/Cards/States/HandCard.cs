using Godot;

public partial class HandCard : Node3D
{
    [Export] private double animationDuration = 0.1;
    [Export] private Vector3 highlightOffset = new Vector3(0, -5, -5);

    public HandCard()
    {
        Card = new Card();
    }

    private MeshInstance3D Mesh => GetNode<MeshInstance3D>("Mesh");
    
	private StandardMaterial3D CardTypeMaterial
	{
		get => (StandardMaterial3D)this.Mesh.MaterialOverlay;
		set => this.Mesh.MaterialOverlay = value;
	}

	private StandardMaterial3D CardEffectMaterial
	{
		get => (StandardMaterial3D)this.CardTypeMaterial.NextPass;
		set => this.CardTypeMaterial.NextPass = value;
	}

	private StandardMaterial3D CardDecalMaterial
	{
		get => (StandardMaterial3D)this.CardEffectMaterial.NextPass;
		set => this.CardEffectMaterial.NextPass = value;
	}

    public Card Card { get; private set; }

    public bool Highlight { get; set; }

    public bool Selected { get; set; }

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
		this.ApplyTypeTexture();
		this.ApplyDecalTexture();
		this.ApplyEffectTexture();
	}

	private void ApplyEffectTexture()
	{
		StandardMaterial3D material = (StandardMaterial3D)this.CardEffectMaterial.Duplicate(true);
		material.AlbedoTexture = this.Card.CardEffect?.Texture;
		this.CardEffectMaterial = material;
	}

	private void ApplyDecalTexture()
	{
		StandardMaterial3D material = (StandardMaterial3D)this.CardDecalMaterial.Duplicate(true);
		material.AlbedoTexture = this.Card.CardType?.DecalTexture;
		this.CardDecalMaterial = material;
	}

	private void ApplyTypeTexture()
	{
		StandardMaterial3D material = (StandardMaterial3D)this.CardTypeMaterial.Duplicate(true);
		material.AlbedoTexture = this.Card.CardType?.Texture;
		this.CardTypeMaterial = material;
	}
}