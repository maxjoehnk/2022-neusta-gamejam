using System;

public record CardResult
{
    public bool EndTurn { get; set; }
    
    public int ModifyCardsRemaining { get; set; }

    public int? RemainingMoves { get; set; }

    public CardResult Join(CardResult other)
    {
        if (other == null)
        {
            return this;
        }
        
        int? remainingMoves;
        if (this.RemainingMoves != null && other.RemainingMoves != null)
        {
            remainingMoves = Math.Min((int)this.RemainingMoves, (int)other.RemainingMoves);
        }
        else
        {
            remainingMoves = this.RemainingMoves ?? other.RemainingMoves;
        }
        
        return new CardResult
        {
            EndTurn = this.EndTurn || other.EndTurn,
            ModifyCardsRemaining = this.ModifyCardsRemaining + other.ModifyCardsRemaining,
            RemainingMoves = remainingMoves,
        };
    }
}