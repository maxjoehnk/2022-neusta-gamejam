using System.Collections.Generic;

using Godot;

public partial class Player : Node3D
{
    private Tween tween;
    private PropertyTweener runningTweenAnimation;

    [Export] private double rotationAnimationDuration = 0.2;
    [Export] private double movementAnimationDuration = 0.2;
    [Export] private double jumpAnimationDuration = 0.2;
    [Export] private double jumpToAnimationDelay = 0.5;

    private Node3D Cone => GetNode<Node3D>("Cone");
    private Node3D EScooter => GetNode<Node3D>("EScooter");
    private Node3D Hat => GetNode<Node3D>("Hat");

    public int PlayerNr { get; set; }
    public bool Active { get; set; }

    public int Coins { get; set; }

    public Position Spawn { get; set; }
    public Position MapPosition { get; set; }

    public Character Character { get; set; }
    public List<Card> Cards { get; set; }

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

    public Direction MoveTo(Position position)
    {
        Direction direction = this.MapPosition.GetDirection(position);
        this.MapPosition = position;
        this.tween?.Kill();
        if (this.tween?.IsRunning() != true)
        {
            this.tween = this.CreateTween().SetTrans(Tween.TransitionType.Linear);
        }
        Vector3 rotation = direction.ToRotation();
        tween.TweenProperty(this, "rotation", rotation, rotationAnimationDuration);

        Vector3 translation = MapToGlobalPosition();
        this.runningTweenAnimation = this.tween.TweenProperty(this, "position", translation, movementAnimationDuration);

        return direction;
    }

    public void MoveTogether(Position position, Tween movementTween, double duration)
    {
        this.MapPosition = position;

        Vector3 translation = MapToGlobalPosition();
        this.runningTweenAnimation = movementTween.TweenProperty(this, "position", translation, duration);
    }

    public void JumpTo(Position position)
    {
        this.MapPosition = position;
        Vector3 translation = MapToGlobalPosition();
        this.runningTweenAnimation = this.tween.TweenProperty(this, "position", translation, jumpAnimationDuration).SetDelay(this.jumpToAnimationDelay);
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
        if (this.tween is { } runningTween)
        {
            runningTween.Kill();
        }
        this.tween = tween;

        PropertyTweener animateMovement = tween.TweenProperty(this, "position", translation, this.movementAnimationDuration);
        this.runningTweenAnimation = animateMovement;
        
        return animateMovement;
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

    public void ReplaceCard(Card oldCard, Card newCard)
    {
        int cardIndex = this.Cards.FindIndex(c => c == oldCard);
        this.Cards[cardIndex] = newCard;
    }

    public void ResetToSpawn()
    {
        if (this.runningTweenAnimation != null)
        {
            this.runningTweenAnimation.Finished += this.MoveToSpawn;
        }
        else
        {
            this.MoveToSpawn();
        }
    }

    private void MoveToSpawn()
    {
        this.MapPosition = this.Spawn;
        this.Position = this.MapToGlobalPosition();
    }
}