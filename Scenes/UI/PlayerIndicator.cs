using Godot;

public partial class PlayerIndicator : VFlowContainer
{
	private const int ActiveSize = 128;
	private const int InactiveSize = 64;

	public Player Player { get; set; }
	private Panel Panel => GetNode<Panel>("Panel");
	// private MeshInstance2D Character => GetNode<MeshInstance2D>("Panel/MeshInstance2D");
	private Label Text => GetNode<Label>("Label");

	private int PanelSize => Player.Active ? ActiveSize : InactiveSize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Text.Text = this.Player.Name;
		this.Panel.Size = new Vector2 { x = PanelSize, y = PanelSize };
		this.Panel.CustomMinimumSize = this.Panel.Size;
		this.ResetSize();
	}
}
