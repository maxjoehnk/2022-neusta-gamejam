using System;
using System.Collections.Generic;

using Godot;

public class CardFactory
{
	private readonly PackedScene cardScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/Card.tscn");
	private readonly PackedScene deskCardScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/States/DeskCard.tscn");
	private readonly PackedScene placingCardScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/States/PlacingCard.tscn");

	private readonly List<ICardTypeFactory> cardTypeFactories = new List<ICardTypeFactory>
	{
		new CornerCardFactory(),
		new StraightCardFactory(),
		new TCrossingCardFactory(),
		new CrossingCardFactory(),
	};
	
	private readonly List<ICardEffectFactory> cardEffectFactories = new List<ICardEffectFactory>
	{
		new TeleportEffectFactory(),
	};

	private readonly Random random = new Random();

	public Card CreateCard()
	{
		Card card = this.cardScene.Instantiate<Card>();
		ICardTypeFactory cardTypeFactory = WeightedRandomizer.Next(this.cardTypeFactories);
		ICardType cardType = cardTypeFactory.BuildCardType();
		card.SetCardType(cardType);

		ICardEffectFactory cardEffectFactory = WeightedRandomizer.Next(this.cardEffectFactories, 100);
		if (cardEffectFactory != null)
		{
			card.SetCardEffect(cardEffectFactory.BuildCardEffect());
		}

		return card;
	}

	public DeskCard CreateDeskCard(int x, int y, Card card)
	{
		DeskCard deskCard = this.deskCardScene.Instantiate<DeskCard>();
		deskCard.SetCard(card);
		deskCard.MapPosition = new Position { X = x, Y = y };
		deskCard.Orientation = this.random.Next(0, 4);

		return deskCard;
	}

	public DeskCard FromHand(PlacingCard placingCard)
	{
		Card card = (Card)placingCard.Card.Duplicate();
		card.SetCardType(placingCard.Card.CardType);
		card.SetCardEffect(placingCard.Card.CardEffect);
		DeskCard deskCard = this.CreateDeskCard(placingCard.MapPosition.X, placingCard.MapPosition.Y, card);
		deskCard.Orientation = placingCard.Orientation;

		return deskCard;
	}

	public PlacingCard CreatePlacingCard(Card card)
	{
		PlacingCard placingCard = this.placingCardScene.Instantiate<PlacingCard>();
		placingCard.SetCard(card);

		return placingCard;
	}
}
