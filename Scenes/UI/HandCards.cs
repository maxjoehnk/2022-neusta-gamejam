using Godot;

using System.Linq;

using Godot.Collections;

public partial class HandCards : Node3D
{
    private HandCard Card1 => GetNode<HandCard>("Card1");
    private HandCard Card2 => GetNode<HandCard>("Card2");
    private HandCard Card3 => GetNode<HandCard>("Card3");
    private HandCard Card4 => GetNode<HandCard>("Card4");
    private HandCard Card5 => GetNode<HandCard>("Card5");

    private HandCard[] Cards => new[]
    {
        Card1,
        Card2,
        Card3,
        Card4,
        Card5,
    };

    public Player Player { get; set; }

    public override void _Process(double delta)
    {
        foreach ((Card card, HandCard hand) value in Player.Cards.Zip(Cards))
        {
            value.hand.SetCard(value.card);
        }
    }

    public void RayCast(Vector3 from, Vector3 to)
    {
        foreach (HandCard card in Cards)
        {
            card.Highlight = false;
        }
        Dictionary intersectRay = GetWorld3d().DirectSpaceState.IntersectRay(new PhysicsRayQueryParameters3D
        {
            From = from,
            To = to,
            CollisionMask = 8,
        });
        if (intersectRay.ContainsKey("collider") && intersectRay["collider"].Obj is HandCard)
        {
            HandCard selectedCard = intersectRay["collider"].Obj switch
            {
                (var card1) when card1 == Card1 => Card1,
                (var card2) when card2 == Card2 => Card2,
                (var card3) when card3 == Card3 => Card3,
                (var card4) when card4 == Card4 => Card4,
                (var card5) when card5 == Card5 => Card5,
                _ => null,
            };
            if (selectedCard != null)
            {
                selectedCard.Highlight = true;
            }
        }
    }

    public Card SelectCard()
    {
        return this.Cards.FirstOrDefault(c => c.Highlight)?.Card;
    }
}