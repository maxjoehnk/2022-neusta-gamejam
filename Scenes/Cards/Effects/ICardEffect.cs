using Godot;

public interface ICardEffect
{
    public Texture2D Texture { get; }
    
    public Doors ModifyDoors(Doors doors);

    public CardResult OnEnter(GameState gameState);

    public void Ready(BaseCard card)
    {
    }

    public void Process(BaseCard card, double delta) {}

    public Node3D CreateMarker()
    {
        return null;
    }
}