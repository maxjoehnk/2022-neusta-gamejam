using Godot;

public class CoinEffect : ICardEffect
{
    private readonly PackedScene coinMarkerScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/Effects/Coin.tscn");
    
    private bool hasCoin = true;
    private Node3D coinMarker;

    public Texture2D Texture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardEffects/coin.png");
    
    public Doors ModifyDoors(Doors doors)
    {
        return doors;
    }

    public CardResult OnEnter(Player player, Map map)
    {
        if (this.hasCoin)
        {
            player.Coins += 1;
            this.hasCoin = false;

            return new CardResult
            {
                EndTurn = true
            };
        }

        return new CardResult();
    }

    public void Ready(BaseCard card)
    {
    }

    public void Process(BaseCard card, double delta)
    {
        if (this.hasCoin)
        {
            this.coinMarker?.Show();
        }
        else
        {
            this.coinMarker?.Hide();
        }
    }

    public Node3D CreateMarker()
    {
        Node3D coinMarker = this.coinMarkerScene.Instantiate<Node3D>();
        coinMarker.GetNode<AnimationPlayer>("AnimationPlayer").Autoplay = "Rotate";
        this.coinMarker = coinMarker;
        
        return coinMarker;
    }
}

public class CoinEffectFactory : ICardEffectFactory
{
    public int Probability => 5;
    
    public ICardEffect BuildCardEffect()
    {
        return new CoinEffect();
    }
}