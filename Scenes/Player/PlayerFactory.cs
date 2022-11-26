using Godot;

public class PlayerFactory
{
    private static readonly Color[] Colors = {
        new Color { r = 1, g = 0, b = 0, },
        new Color { r = 0, g = 1, b = 0, },
        new Color { r = 0, g = 0, b = 1, },
    };

    private readonly PackedScene playerScene;

    public PlayerFactory()
    {
        this.playerScene = ResourceLoader.Load<PackedScene>("res://Scenes/Player/Player.tscn");
    }

    public Player CreatePlayer(int i)
    {
        Player player = this.playerScene.Instantiate<Player>();
        player.MapPosition = new Position { X = 0, Y = i };
        player.Color = Colors[i];
        player.Name = $"Player {i}";

        return player;
    }
}