public interface ICardTypeFactory : IHaveProbability
{
    public ICardType BuildCardType();
}