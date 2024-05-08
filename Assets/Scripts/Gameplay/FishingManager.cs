using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
{
    [SerializeField] private FishStorageSO fishes;

    #region State Machine
    public StateMachine StateMachine { get;set; }
    public IdleState IdleState { get; set; }
    public CastingRodState CastingRodState { get; set; }
    public WaitForFishState WaitForFishState { get; set; }
    public CatchingFishState CatchingFishState { get; set; }
    #endregion

    #region Casting Rod

    // [SerializeField] public Slider distanceIndicator;
    [SerializeField] public Image distanceIndicator;

    #endregion

    #region Generated Fish
    private FishSO catchedFish = null;
    private float catchedFishWeight = 0;
    #endregion

    #region Catching Fish
    [SerializeField] public GameObject CatchingUI;
    [SerializeField] public Image SuccessIndicator;
    [field: SerializeField] public GameObject FishIndicator { get; set; }
    [field: SerializeField] public GameObject RodIndicator { get; set; }
    #endregion

    #region Animations
    [SerializeField] private Animator fisherAnimator;
    #endregion

    private void Start()
    { 
        StateMachine = new StateMachine();

        IdleState = new IdleState(StateMachine, this);
        CastingRodState = new CastingRodState(StateMachine, this);
        WaitForFishState = new WaitForFishState(StateMachine, this);
        CatchingFishState = new CatchingFishState(StateMachine, this);

        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }

    private void LateUpdate()
    {
        StateMachine.CurrentState.FrameLateUpdate();
    }

    public void GenerateFish(float distance)
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

        // Now you have the selected fish and its weight
        Debug.Log("Generated fish: " + selectedFish.Title + ", Weight: " + weight);
    }

    public void SetAnimatorState(int state)
    {
        fisherAnimator.SetInteger("State", state);
    }
}
