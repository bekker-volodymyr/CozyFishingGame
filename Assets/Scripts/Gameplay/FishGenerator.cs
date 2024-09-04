using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator
{
    [SerializeField] private FishStorageSO FishStorageSO;

    public static Fish GenerateFish(float distance, FishStorageSO fishes)
    {
        // Define variables to store cumulative rarity and weight probabilities
        float cumulativeRarityProbability = 0;
        float cumulativeWeightProbability = 0;

        // Calculate total rarity and weight of all fishes within the distance
        foreach (FishSO fishSO in fishes.fishes)
        {
            float rarityWeight = Mathf.Lerp(0.5f, 1f, distance); // Adjust rarity weight based on distance
            float weightWeight = Mathf.Lerp(0.5f, 1f, distance); // Adjust weight weight based on distance

            cumulativeRarityProbability += rarityWeight * fishSO.Rarity;
            cumulativeWeightProbability += weightWeight * (fishSO.MaxWeight - fishSO.MinWeight);
        }

        // Generate a random value for rarity and weight within the cumulative probability ranges
        float randomRarity = Random.Range(0f, cumulativeRarityProbability);
        float randomWeight = Random.Range(0f, cumulativeWeightProbability);

        // Iterate through fishes to find the selected fish based on rarity
        FishSO selectedFish = null;
        foreach (FishSO fishSO in fishes.fishes)
        {
            float rarityWeight = Mathf.Lerp(0.5f, 1f, distance); // Adjust rarity weight based on distance
            float weightWeight = Mathf.Lerp(0.5f, 1f, distance); // Adjust weight weight based on distance

            float adjustedRarity = rarityWeight * fishSO.Rarity;
            float adjustedWeightRange = weightWeight * (fishSO.MaxWeight - fishSO.MinWeight);

            // Check if the random rarity value falls within the current fish's probability range
            if (randomRarity < adjustedRarity)
            {
                selectedFish = fishSO;
                break;
            }
            else
            {
                // Subtract the current fish's rarity probability from the random value
                randomRarity -= adjustedRarity;
            }
        }

        // If no fish was selected based on rarity, select one based on weight
        if (selectedFish == null)
        {
            foreach (FishSO fishSO in fishes.fishes)
            {
                float weightWeight = Mathf.Lerp(0.5f, 1f, distance); // Adjust weight weight based on distance

                float adjustedWeightRange = weightWeight * (fishSO.MaxWeight - fishSO.MinWeight);

                // Check if the random weight value falls within the current fish's weight range
                if (randomWeight < adjustedWeightRange)
                {
                    selectedFish = fishSO;
                    break;
                }
                else
                {
                    // Subtract the current fish's weight range from the random value
                    randomWeight -= adjustedWeightRange;
                }
            }
        }

        // Generate a random weight for the selected fish within its specified range
        float weight = Random.Range(selectedFish.MinWeight, selectedFish.MaxWeight);

        Fish generatedFish = new Fish() { FishType = selectedFish, Weight = weight };

        // Now you have the selected fish and its weight
        Debug.Log("Generated fish: " + generatedFish.FishType.Title + ", Weight: " + generatedFish.Weight);

        return generatedFish;
    }
}
