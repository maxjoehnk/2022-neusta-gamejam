public interface ICardFactory
{
    public int Probability { get; }
    
    public Card CreateCard();
}