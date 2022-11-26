using Godot;

public interface ICardType
{
    public Texture2D Texture { get; }
    
    public Doors doors { get; }
}