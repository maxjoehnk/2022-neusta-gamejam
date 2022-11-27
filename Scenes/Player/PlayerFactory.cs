using Godot;

public class PlayerFactory
{
    private static readonly Character[] Characters = {
        Character.EScooter,
        Character.Hat,
        Character.Cone
    };

    private readonly PackedScene playerScene;

    public PlayerFactory()
    {
        this.playerScene = ResourceLoader.Load<PackedScene>("res://Scenes/Player/Player.tscn");
    }

    public Player CreatePlayer(int i, Position spawn)
    {
        Player player = this.playerScene.Instantiate<Player>();
        player.Spawn = spawn;
        player.MapPosition = spawn;
        player.Character = Characters[i];
        player.Name = $"Player {i}";

        return player;
    }
}