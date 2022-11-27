using System;
using System.Collections.Generic;

using Godot;

public class CardFactory
{
	private readonly PackedScene cardScene = ResourceLoader.Load<PackedScene>("res://Scenes/Cards/BaseCard.tscn");
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
		new CoinEffectFactory(),
		new PicnicEffectFactory(),
	};

	private readonly List<ICardModifierFactory> cardModifierFactories = new List<ICardModifierFactory>
	{
		new GlueModifierFactory(),
		new IceModifierFactory(),
	};

	private readonly Random random = new Random();

	public Card CreateCard()
	{
		Card card = new Card();

		ICardTypeFactory cardTypeFactory = WeightedRandomizer.Next(this.cardTypeFactories);
		ICardType cardType = cardTypeFactory.BuildCardType();
		card.Type = cardType;

		ICardEffectFactory cardEffectFactory = WeightedRandomizer.Next(this.cardEffectFactories, 100);
		if (cardEffectFactory != null)
		{
			card.Effect = cardEffectFactory.BuildCardEffect();
		}

		ICardModifierFactory cardModifierFactory = WeightedRandomizer.Next(this.cardModifierFactories, 100);
		if (cardModifierFactory != null)
		{
			card.Modifier = cardModifierFactory.BuildCardModifier();
		}

		return card;
	}

	public BaseCard CreateBaseCard()
	{
		return this.CreateBaseCard(this.CreateCard());
	}
	
	public BaseCard CreateBaseCard(Card card)
	{
		BaseCard baseCard = this.cardScene.Instantiate<BaseCard>();
		baseCard.SetCard(card);

		return baseCard;
	}

	public DeskCard CreateDeskCard(int x, int y, BaseCard card)
	{
		DeskCard deskCard = this.deskCardScene.Instantiate<DeskCard>();
		deskCard.SetCard(card);
		deskCard.MapPosition = new Position { X = x, Y = y };
		deskCard.Orientation = this.random.Next(0, 4);

		return deskCard;
	}

	public DeskCard FromHand(PlacingCard placingCard)
	{
		BaseCard card = (BaseCard)placingCard.BaseCard.Duplicate();
		card.SetCard(placingCard.BaseCard.Card);
		DeskCard deskCard = this.CreateDeskCard(placingCard.MapPosition.X, placingCard.MapPosition.Y, card);
		deskCard.Orientation = placingCard.Orientation;

		return deskCard;
	}

	public PlacingCard CreatePlacingCard(BaseCard card)
	{
		PlacingCard placingCard = this.placingCardScene.Instantiate<PlacingCard>();
		placingCard.SetCard(card);

		return placingCard;
	}
}
