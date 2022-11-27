using Godot;

public static class NodeExtensions
{
    public static void ClearChildren(this Node node)
    {
        foreach (Node child in node.GetChildren())
        {
            node.RemoveChild(child);
            child.QueueFree();
        }
    }
}