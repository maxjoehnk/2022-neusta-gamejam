using System;
using System.Collections.Generic;
using System.Linq;

using Godot;

public class Map
{
	private int maxX = 0;
	private int maxY = 0;
	private List<List<DeskCard>> cards = new List<List<DeskCard>>();

	public IEnumerable<DeskCard> Cards => cards.SelectMany(row => row);

	public List<Position> Spawns => new List<Position>
	{
		new Position(0, 0),
		new Position(0, this.maxY),
		new Position(this.maxX, this.maxY),
		new Position(this.maxX, 0)
	};
	
	public void SetCards(List<DeskCard> cardList)
	{
		maxX = cardList.Max(card => card.MapPosition.X);
		maxY = cardList.Max(card => card.MapPosition.Y);
		this.cards = Enumerable.Range(0, maxX + 1).Select(_ => Enumerable.Range(0, this.maxY + 1).Select(_ => null as DeskCard).ToList()).ToList();
		foreach (DeskCard card in cardList)
		{
			this.cards[card.MapPosition.X][card.MapPosition.Y] = card;
		}
	}
	
	public bool CanMoveTo(Position from, Position to)
	{
		if (to.X < 0 || to.Y < 0)
		{
			return false;
		}

		if (to.X > maxX || to.Y > maxY)
		{
			return false;
		}

		Direction? direction = from.GetDirection(to);
		
		GD.Print($"{from} => {to}: {direction}");

		if (direction == null)
		{
			return false;
		}

		Side enter = ((Direction)direction).Enter();
		bool canEnterFrom = this.cards[to.X][to.Y].HasDoor(enter);
		Side leave = ((Direction)direction).Leave();
		bool canLeaveTo = this.cards[from.X][from.Y].HasDoor(leave);
		GD.Print($"{direction}\n {enter} {cards[to.X][to.Y].BaseCard.Card.Type} {canEnterFrom}\n {leave} {cards[from.X][from.Y].BaseCard.Card.Type} {canLeaveTo}");
		
		return canEnterFrom && canLeaveTo;
	}

	public void PushCard(PlacingCard placingCard, DeskCard deskCard, List<Player> players, Tween movementTween)
	{
		Position placement = new Position
		{
			X = Math.Clamp(placingCard.MapPosition.X, 0, this.maxX),
			Y = Math.Clamp(placingCard.MapPosition.Y, 0, this.maxY),
		};
		deskCard.MoveTo(placement, movementTween);
		Direction direction = GetPushInDirection(placement);
		if (direction.IsVertical())
		{
			int x = placement.X;
			int modifier = direction == Direction.Up ? 1 : -1;
			int start = direction == Direction.Up ? this.maxY : 0;
			for (int y = start; y <= this.maxY && y >= 0; y -= modifier)
			{
				DeskCard card = this.cards[x][y];
				int targetY = y + modifier;
				Position target = new Position(x, targetY);
				foreach (Player player in players.Where(p => p.MapPosition == card.MapPosition))
				{
					player.MoveTogether(target, movementTween.Parallel(), 0.1);
				}
				card.MoveTo(target, movementTween.Parallel());
				if (targetY < 0 || targetY > maxY)
				{
					card.Despawn();
				}
				else
				{
					this.cards[x][targetY] = card;
				}
			}
		}
		if (direction.IsHorizontal())
		{
			int y = placement.Y;
			int modifier = direction == Direction.Left ? 1 : -1;
			int start = direction == Direction.Left ? this.maxY : 0;
			for (int x = start; x <= this.maxX && x >= 0; x -= modifier)
			{
				DeskCard card = this.cards[x][y];
				int targetX = x + modifier;
				Position target = new Position(targetX, y);
				foreach (Player player in players.Where(p => p.MapPosition == card.MapPosition))
				{
					player.MoveTogether(target, movementTween.Parallel(), 0.1);
				}
				card.MoveTo(target, movementTween.Parallel());
				if (targetX < 0 || targetX > maxX)
				{
					card.Despawn();
				}
				else
				{
					this.cards[targetX][y] = card;
				}
			}
		}
		this.cards[placement.X][placement.Y] = deskCard;
	}

	private Direction GetPushInDirection(Position position)
	{
		return (position.X, position.Y) switch
		{
			(0, _) => Direction.Left,
			(_, 0) => Direction.Up,
			var (x, _) when x == this.maxX => Direction.Right,
			var (_, y) when y == this.maxY => Direction.Down,
		};
	}

	public Position OutsideBoundaries(Position position)
	{
		int xMin = -1;
		int xMax = this.maxX + 1;
		int yMin = -1;
		int yMax = this.maxY + 1;
		int rawX = Math.Clamp(position.X, xMin, xMax);
		int rawY = Math.Clamp(position.Y, yMin, yMax);
		int x = rawX;
		int y = rawY;
		if (rawY != yMin && rawY != yMax)
		{
			x = x - xMin < xMax - x ? xMin : xMax;
		}
		if (rawX != xMin && rawX != xMax)
		{
			y = y - yMin < yMax - y ? yMin : yMax;
		}

		return new Position
		{
			X = x,
			Y = y,
		};
	}

	public BaseCard GetCard(Position position)
	{
		return this.cards[position.X][position.Y].BaseCard;
	}

	public Position GetLastCardInDirection(Position position, Direction direction)
	{
		List<DeskCard> rowOrColumn = direction.IsVertical()
			? this.cards[position.X]
			: this.cards.Select(row => row[position.Y]).ToList();
		int xOrY = direction.IsVertical() ? position.Y : position.X;

		List<DeskCard> cards = direction is Direction.Left or Direction.Up
			? rowOrColumn.Take(xOrY + 1).Reverse().ToList()
			: rowOrColumn.Skip(xOrY).ToList();
		
		for (int i = 0; i < cards.Count; i++)
		{
			DeskCard startCard = cards[i];
			DeskCard nextCard = cards[i + 1];

			if (nextCard == null)
			{
				return startCard.MapPosition;
			}

			if (!startCard.HasDoor(direction.Leave()))
			{
				return startCard.MapPosition;
			}

			if (!nextCard.HasDoor(direction.Enter()))
			{
				return startCard.MapPosition;
			}
		}

		return position;
	}
}
