using Godot;

public class CoinEffect : ICardEffect
{
    private bool hasCoin = true;
    
	public Texture2D Texture => ResourceLoader.Load<CompressedTexture2D>("res://Assets/Textures/CardEffects/portal.jpg");
    
    public Doors ModifyDoors(Doors doors)
    {
        return doors;
    }

    public void OnEnter(Player player, Map map)
    {
        if (this.hasCoin)
        {
            player.Coins += 1;
            this.hasCoin = false;
        }
    }

    public void Ready(Card card)
    {
        card.GetNode<AnimationPlayer>("Coin/AnimationPlayer").Play("Rotate");
    }

    public void Process(Card card, double delta)
    {
        Node3D coin = card.GetNode<Node3D>("Coin");
        if (this.hasCoin)
        {
            coin.Show();
        }
        else
        {
            coin.Hide();
        }
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