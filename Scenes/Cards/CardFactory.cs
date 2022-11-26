using System;
using System.Collections.Generic;

using Godot;

public class CardFactory
{
	private readonly PackedScene cardScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/Card.tscn");
	private readonly PackedScene deskCardScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/States/DeskCard.tscn");
	private readonly PackedScene placingCardScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/States/PlacingCard.tscn");

	private readonly List<ICardTypeFactory> cardFactories = new List<ICardTypeFactory>
	{
		new CornerCardFactory(),
		new StraightCardFactory(),
		new TCrossingCardFactory(),
		new CrossingCardFactory(),
	};

	private readonly Random random = new Random();

	public Card CreateCard()
	{
		Card card = this.cardScene.Instantiate<Card>();
		int factoryIndex = this.random.Next(0, this.cardFactories.Count);
		ICardType cardType = this.cardFactories[factoryIndex].BuildCardType();
		card.SetCardType(cardType);

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

	public DeskCard FromHand(PlacingCard card)
	{
		Card duplicate = (Card)card.Card.Duplicate();
		duplicate.SetCardType(card.Card.CardType);
		DeskCard deskCard = this.CreateDeskCard(card.MapPosition.X, card.MapPosition.Y, duplicate);
		deskCard.Orientation = card.Orientation;

		return deskCard;
	}

	public PlacingCard CreatePlacingCard(Card card)
	{
		PlacingCard placingCard = this.placingCardScene.Instantiate<PlacingCard>();
		placingCard.SetCard(card);

		return placingCard;
	}
}
