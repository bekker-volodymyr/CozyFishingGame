using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FishGeneratorTests
{
    [Test]
    public void WhenCalledGenratesFish()
    {
        // ARRANGE
        float distance = 0.5f;
        FishStorageSO fishes = Resources.Load("FishStorageSO") as FishStorageSO;

        // ACT
        Fish generatedFish = FishGenerator.GenerateFish(distance, fishes);

        // ASSERT
        Assert.AreNotEqual(null, generatedFish);
    }

    [Test]
    public void WhenCalledGeneratesFishesWeightInRightBounds()
    {
        // ARRANGE
        float distance = 0.5f;
        FishStorageSO fishes = Resources.Load("FishStorageSO") as FishStorageSO;

        // ACT
        Fish generatedFish = FishGenerator.GenerateFish(distance, fishes);

        // ASSERT
        bool isGreater = generatedFish.Weight > generatedFish.FishType.MinWeight;
        bool isLess = generatedFish.Weight < generatedFish.FishType.MaxWeight;
        Assert.IsTrue(isGreater && isLess);
    }
}
