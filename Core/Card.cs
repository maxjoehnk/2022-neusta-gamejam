public class Card
{
    public ICardType Type { get; set; }

    public ICardEffect Effect { get; set; }
    
    public ICardModifier Modifier { get; set; }
};