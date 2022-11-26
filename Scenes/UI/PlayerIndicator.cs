using Godot;

public partial class PlayerIndicator : VFlowContainer
{
	private readonly PackedScene coinScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/coin.tscn");
	
	private const int ActiveSize = 128;
	private const int InactiveSize = 64;

	public Player Player { get; set; }
	private Panel Panel => GetNode<Panel>("Panel");
	// private MeshInstance2D Character => GetNode<MeshInstance2D>("Panel/MeshInstance2D");
	private Label Text => GetNode<Label>("Stats/Label");
	private VFlowContainer CoinContainer => GetNode<VFlowContainer>("Stats/Coins");

	private int PanelSize => Player.Active ? ActiveSize : InactiveSize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Text.Text = this.Player.Name;
		Vector2 size = new Vector2 { x = PanelSize, y = PanelSize };
		this.Panel.Size = size;
		this.Panel.CustomMinimumSize = size;
		this.ResetSize();
		if (this.CoinContainer.GetChildren().Count != this.Player.Coins)
		{
			foreach (Node child in this.CoinContainer.GetChildren())
			{
				this.CoinContainer.RemoveChild(child);
			}

			for (int i = 0; i < this.Player.Coins; i++)
			{
				this.CoinContainer.AddChild(this.coinScene.Instantiate());
			}
		}
	}
}
