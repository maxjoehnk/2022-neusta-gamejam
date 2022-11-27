using Godot;

public class GlueModifier : ICardModifier
{
    public Texture2D Texture => 
        ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardModifiers/Glue.jpg");
    
    public CardResult OnEnter(GameState gameState, Direction direction)
    {
        return new CardResult
        {
            RemainingMoves = 0,
        };
    }
}

public class GlueModifierFactory : ICardModifierFactory
{
    public int Probability => 5;
    
    public ICardModifier BuildCardModifier()
    {
        return new GlueModifier();
    }
}