public interface ICardEffectFactory : IHaveProbability
{
    public ICardEffect BuildCardEffect();
}