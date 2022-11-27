using System.Linq;

using Godot;

public class TeleportEffect : ICardEffect
{
    public Texture2D Texture =>
        ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardEffects/portal.jpg");

    public Doors ModifyDoors(Doors doors)
    {
        return doors;
    }

    public CardResult OnEnter(GameState gameState, Direction direction)
    {
        DeskCard targetCard = gameState.Map.Cards
            .Where(card => card.BaseCard.Card.Effect is TeleportEffect)
            .FirstOrDefault(card => card.BaseCard.Card.Effect != this);

        if (targetCard != null)
        {
            gameState.ActivePlayer.JumpTo(targetCard.MapPosition);

            return new CardResult
            {
                EndTurn = true
            };
        }

        return new CardResult();
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