public record CardResult
{
    public bool EndTurn { get; set; }
    
    public int ModifyCardsRemaining { get; set; }

    public CardResult Join(CardResult other)
    {
        return new CardResult
        {
            EndTurn = this.EndTurn || other.EndTurn,
            ModifyCardsRemaining = this.ModifyCardsRemaining + other.ModifyCardsRemaining,
        };
    }
}