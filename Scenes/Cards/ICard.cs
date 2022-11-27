using Godot;

public interface ICard
{
    public MeshInstance3D Mesh { get; }
    
    public Card Card { get; }
}