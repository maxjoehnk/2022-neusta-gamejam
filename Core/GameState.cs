using System.Collections.Generic;
using System.Linq;

public class GameState
{
    public Map Map { get; set; }

    public List<Player> Players { get; set; }

    public Player ActivePlayer => Players[Turn.PlayerIndex];

    public Turn Turn { get; set; }

    public GameState()
    {
        this.Map = new Map();
        this.Players = new List<Player>();
        this.Turn = new Turn();
    }

    public void NextTurn()
    {
        this.Turn.PlayerIndex++;
        if (this.Turn.PlayerIndex >= this.Players.Count)
        {
            this.Turn.PlayerIndex = 0;
        }
    }

    public bool HasSomeoneWon()
    {
        return this.Players.Any(p => p.Coins == Constants.PointsToWin);
    }
}