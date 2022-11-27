using Godot;

public partial class PlayerIndicator : VFlowContainer
{
	private readonly PackedScene coinScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/coin.tscn");
	private readonly Texture2D coinActive = ResourceLoader.Load<Texture2D>("res://Assets/Textures/UI/coin_icon.png");
	private readonly Texture2D coinInactive = ResourceLoader.Load<Texture2D>("res://Assets/Textures/UI/CoinInactive.png");
	
	private const int ActiveSize = 128;
	private const int InactiveSize = 64;

	public Player Player { get; set; }
	private Panel Panel => GetNode<Panel>("Panel");
	// private MeshInstance2D Character => GetNode<MeshInstance2D>("Panel/MeshInstance2D");
	private Label Text => GetNode<Label>("Stats/Label");
	private VFlowContainer CoinContainer => GetNode<VFlowContainer>("Stats/Coins");

	private int PanelSize => Player.Active ? ActiveSize : InactiveSize;

	public override void _Ready()
	{
		this.CoinContainer.ClearChildren();
		for (int i = 0; i < Constants.PointsToWin; i++)
		{
			TextureRect coin = this.coinScene.Instantiate<TextureRect>();
			coin.Texture = this.coinInactive;
			this.CoinContainer.AddChild(coin);
		}
	}

	public override void _Process(double delta)
	{
		this.Text.Text = this.Player.Name;
		Vector2 size = new Vector2 { x = PanelSize, y = PanelSize };
		this.Panel.Size = size;
		this.Panel.CustomMinimumSize = size;
		this.ResetSize();
		
		for (int i = 0; i < this.Player.Coins; i++)
		{
			this.CoinContainer.GetChild<TextureRect>(i).Texture = this.coinActive;
		}
	}
}
