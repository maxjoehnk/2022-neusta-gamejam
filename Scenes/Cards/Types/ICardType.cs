using Godot;

public interface ICardType
{
    public Texture2D Texture { get; }
    
    public Texture2D DecalTexture { get; }
    
    public Doors doors { get; }
}