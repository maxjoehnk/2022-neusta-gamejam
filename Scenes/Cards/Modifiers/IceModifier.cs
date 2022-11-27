using Godot;

public class IceModifier : ICardModifier
{
    public Texture2D Texture => 
        ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardModifiers/Ice.jpg");
    
    public CardResult OnEnter(GameState gameState, Direction direction)
    {
        Position position = gameState.Map.GetLastCardInDirection(gameState.ActivePlayer.MapPosition, direction);
        gameState.ActivePlayer.MoveTo(position);
        
        return new CardResult
        {
            RemainingMoves = 0,
        };
    }
}

public class IceModifierFactory : ICardModifierFactory
{
    public int Probability => 5;
    
    public ICardModifier BuildCardModifier()
    {
        return new IceModifier();
    }
}