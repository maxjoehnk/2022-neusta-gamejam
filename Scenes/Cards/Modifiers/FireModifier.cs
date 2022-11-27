using Godot;

public class FireModifier : ICardModifier
{
    private PackedScene fireScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/Modifiers/FireModifier.tscn");
    public Texture2D Texture => 
        ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardModifiers/Fire.jpg");
    
    public CardResult OnEnter(GameState gameState, Direction direction)
    {
        return new CardResult
        {
            Kill = true
        };
    }

    public Node3D CreateMarker()
    {
        return this.fireScene.Instantiate<Node3D>();
    }
}

public class FireModifierFactory : ICardModifierFactory
{
    public int Probability => 3;
    
    public ICardModifier BuildCardModifier()
    {
        return new FireModifier();
    }
}
