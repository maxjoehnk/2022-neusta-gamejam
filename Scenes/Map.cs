using System.Collections.Generic;
using System.Linq;

using Godot;

public class Map
{
    private int maxX = 0;
    private int maxY = 0;
    private List<List<DeskCard>> cards = new List<List<DeskCard>>();

    public void SetCards(List<DeskCard> cardList)
    {
        maxX = cardList.Max(card => card.MapPosition.X);
        maxY = cardList.Max(card => card.MapPosition.Y);
        this.cards = Enumerable.Range(0, maxX + 1).Select(_ => Enumerable.Range(0, this.maxY + 1).Select(_ => null as DeskCard).ToList()).ToList();
        foreach (DeskCard card in cardList)
        {
            this.cards[card.MapPosition.X][card.MapPosition.Y] = card;
        }
    }
    
    public bool CanMoveTo(Position from, Position to)
    {
        if (to.X < 0 || to.Y < 0)
        {
            return false;
        }

        if (to.X > maxX || to.Y > maxY)
        {
            return false;
        }

        Direction? direction = from.GetDirection(to);
        
        GD.Print($"{from} => {to}: {direction}");

        if (direction == null)
        {
            return false;
        }
        
        return cards[to.X][to.Y].CanEnterFrom((Direction)direction) &&
               cards[from.X][from.Y].CanLeaveTo((Direction)direction);
    }
}