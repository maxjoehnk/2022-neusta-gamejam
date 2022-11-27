using Godot;

public interface ICardEffect
{
    public Texture2D Texture { get; }
    
    public Doors ModifyDoors(Doors doors);

    public void OnEnter(Player player, Map map);
}