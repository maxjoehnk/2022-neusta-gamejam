using System;

public record Turn
{
    public int PlayerIndex { get; set; }

    public int CardsAvailable { get; set; }
    public int CardsLeft { get; set; }

    public int MovementLeft { get; set; }

    public void ModifyAvailableCards(int cards)
    {
        if (cards > 0)
        {
            this.CardsLeft += cards;
            this.CardsAvailable += cards;
        }else if (cards < 0)
        {
            this.CardsLeft = Math.Max(this.CardsLeft + cards, 0);
        }
    }
}