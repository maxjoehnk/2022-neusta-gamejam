public interface ICardModifierFactory : IHaveProbability
{
    public ICardModifier BuildCardModifier();
}