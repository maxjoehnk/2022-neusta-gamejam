using System;
using System.Collections.Generic;

using Godot;

public class CardFactory
{
    private readonly PackedScene deskCardScene;
    private readonly PackedScene handCardScene;

    private readonly List<ICardFactory> cardFactories = new List<ICardFactory>
    {
        new CornerCardFactory(),
        new StraightCardFactory(),
        new TCrossingCardFactory(),
        new CrossingCardFactory(),
    };
    private readonly Random random = new Random();

    public CardFactory()
    {
        this.deskCardScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/DeskCard.tscn");
        this.handCardScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/HandCard.tscn");
    }
    
    public Card CreateCard()
    {
        int factoryIndex = this.random.Next(0, this.cardFactories.Count);
        Card card = this.cardFactories[factoryIndex].CreateCard();
        
        return card;
    }

    public DeskCard CreateDeskCard(int x, int y, Card card)
    {
        DeskCard deskCard = this.deskCardScene.Instantiate<DeskCard>();
        deskCard.SetCard(card);
        deskCard.MapPosition = new Position { X = x, Y = y };
        deskCard.Orientation = this.random.Next(0, 4);
        deskCard.Name = $"Card ({x}, {y})";

        return deskCard;
    }

    public HandCard CreateHandCard(Card card)
    {
        HandCard handCard = this.handCardScene.Instantiate<HandCard>();
        handCard.SetCard(card);

        return handCard;
    }
}