using Godot;

public interface ICardModifier
{
    public Texture2D Texture { get; }
    
    public CardResult OnEnter(GameState gameState, Direction direction);
}