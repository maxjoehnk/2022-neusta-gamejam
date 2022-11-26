using System.Collections.Generic;
using System.Linq;

using Godot;

public class WeightedRandomizer
{
    public static T Next<T>(List<T> items, int padding = 0) where T : IHaveProbability
    {
        List<AccumulatedProbability<T>> accumulatedProbabilities = new List<AccumulatedProbability<T>>(items.Count);
        int accumulator = 0;
        foreach (T item in items)
        {
            accumulator += item.Probability;
            AccumulatedProbability<T> probability = new AccumulatedProbability<T>
            {
                Item = item,
                Accumulated = accumulator
            };
            accumulatedProbabilities.Add(probability);
        }

        accumulator += padding;

        int i = GD.RandRange(0, accumulator);

        AccumulatedProbability<T> selectedProbability = accumulatedProbabilities
            .OrderBy(p => p.Accumulated)
            .FirstOrDefault(p => p.Accumulated >= i);

        if (selectedProbability == null)
        {
            return default;
        }
        
        return selectedProbability.Item;
    }
}

public interface IHaveProbability
{
    public int Probability { get; }
}

class AccumulatedProbability<T> where T : IHaveProbability
{
    public T Item { get; set; }

    public int Accumulated { get; set; }
}