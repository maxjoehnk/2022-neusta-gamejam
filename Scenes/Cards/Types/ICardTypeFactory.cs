public interface ICardTypeFactory
{
    public int Probability { get; }

    public ICardType BuildCardType();
}