using Godot;

public class CardOverlay
{
	private ICard card;

	private MeshInstance3D Mesh => this.card.Mesh;
	private Card Card => this.card.Card;

	public CardOverlay(ICard card)
	{
		this.card = card;
	}
    
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

	private StandardMaterial3D CardModifierMaterial
	{
		get => (StandardMaterial3D)this.CardDecalMaterial.NextPass;
		set => this.CardDecalMaterial.NextPass = value;
	}

	public void ApplyTypeTexture()
	{
		StandardMaterial3D material = (StandardMaterial3D)this.CardTypeMaterial.Duplicate(true);
		material.AlbedoTexture = this.Card.Type?.Texture;
		this.CardTypeMaterial = material;
	}

	public void ApplyDecalTexture()
	{
		StandardMaterial3D material = (StandardMaterial3D)this.CardDecalMaterial.Duplicate(true);
		material.AlbedoTexture = this.Card.Type?.DecalTexture;
		this.CardDecalMaterial = material;
	}

	public void ApplyEffectTexture()
	{
		StandardMaterial3D material = (StandardMaterial3D)this.CardEffectMaterial.Duplicate(true);
		material.AlbedoTexture = this.Card.Effect?.Texture;
		this.CardEffectMaterial = material;
	}

	public void ApplyModifierTexture()
	{
		StandardMaterial3D material = (StandardMaterial3D)this.CardModifierMaterial.Duplicate(true);
		material.AlbedoTexture = this.Card.Modifier?.Texture;
		this.CardModifierMaterial = material;
	}
}