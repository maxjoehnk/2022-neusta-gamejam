using System.Linq;

using Godot;

public class TeleportEffect : ICardEffect
{
	public Texture2D Texture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardEffects/portal.jpg");
    
    public Doors ModifyDoors(Doors doors)
    {
        return doors;
    }

    public void OnEnter(Player player, Map map)
    {
        DeskCard targetCard = map.Cards
            .Where(card => card.BaseCard.CardEffect is TeleportEffect)
            .FirstOrDefault(card => card.BaseCard.CardEffect != this);

        if (targetCard != null)
        {
            player.JumpTo(targetCard.MapPosition);
        }
    }
}

public class TeleportEffectFactory : ICardEffectFactory
{
    public int Probability => 2;
    
    public ICardEffect BuildCardEffect()
    {
        return new TeleportEffect();
    }
}