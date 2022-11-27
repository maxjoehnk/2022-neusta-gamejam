using System.Collections.Generic;

using Godot;

public class PicnicEffect : ICardEffect
{
    private List<Player> players = new List<Player>();
    
    public Texture2D Texture => 
        ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardEffects/picnic.jpg");
    
    public Doors ModifyDoors(Doors doors)
    {
        return doors;
    }

    public CardResult OnEnter(GameState gameState, Direction direction)
    {
        if (this.players.Contains(gameState.ActivePlayer))
        {
            return new CardResult();
        }
        return new CardResult
        {
            ModifyCardsRemaining = 1
        };
    }
}

public class PicnicEffectFactory : ICardEffectFactory
{
    public int Probability => 5;

    public ICardEffect BuildCardEffect()
    {
        return new PicnicEffect();
    }
}
