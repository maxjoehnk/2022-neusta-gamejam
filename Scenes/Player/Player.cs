using Godot;

public partial class Player : Node3D
{
    private Tween tween;

    [Export] private double rotationAnimationDuration = 0.2;
    [Export] private double movementAnimationDuration = 0.2;
    [Export] private double jumpAnimationDuration = 0.2;
    [Export] private double jumpToAnimationDelay = 0.5;

    // private StandardMaterial3D Material => (StandardMaterial3D)this.MaterialOverride;

    private Node3D Cone => GetNode<Node3D>("Cone");
    private Node3D EScooter => GetNode<Node3D>("EScooter");
    private Node3D Hat => GetNode<Node3D>("Hat");

    public int PlayerNr { get; set; }
    public bool Active { get; set; }

    public Position MapPosition { get; set; }

    public Character Character { get; set; }

    public override void _Ready()
    {
        SetPosition();
        if (Character == Character.Cone)
        {
            this.Cone.Show();
        }

        if (Character == Character.EScooter)
        {
            this.EScooter.Show();
        }
        if (Character == Character.Hat)
        {
            this.Hat.Show();
        }
    }

    public void MoveTo(Position position)
    {
        Direction? direction = this.MapPosition.GetDirection(position);
        this.MapPosition = position;
        this.tween?.Kill();
        this.tween = this.CreateTween().SetTrans(Tween.TransitionType.Linear);
        if (direction != null)
        {
            Vector3 rotation = ((Direction)direction).ToRotation();
            tween.TweenProperty(this, "rotation", rotation, rotationAnimationDuration);
        }

        Vector3 translation = MapToGlobalPosition();
        this.tween.TweenProperty(this, "position", translation, movementAnimationDuration);
    }

    public void JumpTo(Position position)
    {
        this.MapPosition = position;
        Vector3 translation = MapToGlobalPosition();
        this.tween.TweenProperty(this, "position", translation, jumpAnimationDuration).SetDelay(this.jumpToAnimationDelay);
    }

    private void SetPosition()
    {
        Vector3 translation = new Vector3
        {
            x = this.MapPosition.X * Constants.CardSize,
            z = this.MapPosition.Y * Constants.CardSize,
            y = 0,
        };
        this.Position = translation;
    }

    private PropertyTweener AnimateMovement(Tween tween)
    {
        Vector3 translation = MapToGlobalPosition();

        return tween.TweenProperty(this, "position", translation, movementAnimationDuration);
    }

    private Vector3 MapToGlobalPosition()
    {
        Vector3 translation = new Vector3
        {
            x = this.MapPosition.X * Constants.CardSize,
            z = this.MapPosition.Y * Constants.CardSize,
            y = 0,
        };
        return translation;
    }

    public override string ToString()
    {
        return $"Player {{ Name = {Name}, Position = {MapPosition}, Active = {Active} }}";
    }
}