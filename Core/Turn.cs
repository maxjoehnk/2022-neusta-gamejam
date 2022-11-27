public record Turn
{
    public int PlayerIndex { get; set; }

    public int CardsLeft { get; set; }

    public int MovementLeft { get; set; }
}