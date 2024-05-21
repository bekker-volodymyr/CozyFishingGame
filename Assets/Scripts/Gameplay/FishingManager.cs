using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FishingManager : MonoBehaviour
{
    [SerializeField] private FishStorageSO fishes;

    #region Stats

    private int fishCought = 0;
    private float coinsEarned = 0;

    #endregion

    #region State Machine

    public StateMachine StateMachine { get;set; }
    public IdleState IdleState { get; set; }
    public CastState CastState { get; set; }
    public WaitState WaitState { get; set; }
    public CatchState CatchState { get; set; }

    #endregion

    #region Cast

    [Space]
    [SerializeField] public Image distanceIndicator;

    #endregion

    #region Generated Fish

    private FishSO catchedFish = null;
    private float catchedFishWeight = 0;

    #endregion

    #region Catch

    [Space]
    [SerializeField] public GameObject CatchingUI;
    [SerializeField] public Image SuccessIndicator;
    [field: SerializeField] public GameObject FishIndicator { get; set; }
    [field: SerializeField] public GameObject RodIndicator { get; set; }
    [SerializeField] public RodScript rodScript;
    [SerializeField] private FishScript fishScript;
    [Space]
    [SerializeField] private Animator pullTextAnimator;
    [Space]
    [SerializeField] private Animator catchedFishAnimator;
    [SerializeField] private GameObject catchedFishGO;
    [SerializeField] private SpriteRenderer catchedFishSprite;
    #endregion

    #region Fisher Components

    [Space]
    [SerializeField] private GameObject fisher;
    [SerializeField] private Animator fisherAnimator;
    [SerializeField] private SpriteRenderer fisherSpriteRenderer;
    [SerializeField] private FisherScript fisherScript;

    #endregion

    #region Menu

    [Space]
    [SerializeField]  private UIManager uiManager;

    #endregion

    #region Save/Load

    private PlayerData playerData;

    #endregion

    private void Start()
    {
        // PlayerPrefs.DeleteAll();

        fisherScript.castAnimationEnds += OnCastAnimationEnds;

        StateMachine = new StateMachine();

        IdleState = new IdleState(StateMachine, this);
        CastState = new CastState(StateMachine, this);
        WaitState = new WaitState(StateMachine, this);
        CatchState = new CatchState(StateMachine, this);

        StateMachine.Initialize(IdleState);

        playerData = PlayerPrefsManager.LoadPlayerData();

        if(playerData == null)
        {
            playerData = new PlayerData();
            playerData.fishCought = 0;
            playerData.coinsEarned = 0;
            playerData.unlockedFishes = new List<FishSO>();
        }

        uiManager.InitUI(playerData);
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

        catchedFish = selectedFish;
        catchedFishWeight = weight;

        // Now you have the selected fish and its weight
        Debug.Log("Generated fish: " + selectedFish.Title + ", Weight: " + weight);
    }

    public void SetAnimatorState(int state)
    {
        fisherAnimator.SetInteger("State", state);
    }

    public void SetIdleState()
    {
        SetAnimatorState(0);
        distanceIndicator.transform.parent.gameObject.SetActive(false);
        CatchingUI.SetActive(false);

        catchedFish = null;
        catchedFishWeight = 0;
    }

    public void SetCastState()
    {
        distanceIndicator.transform.parent.gameObject.SetActive(true);
        distanceIndicator.fillAmount = 0;
    }

    public void SetWaitState()
    {
        SetAnimatorState(1);
    }

    public void SetCatchState()
    {
        CatchingUI.SetActive(true);
        SuccessIndicator.fillAmount = 0f;

        FishIndicator.GetComponent<RectTransform>().localPosition = new Vector3(0f, -336f, 0f);
        RodIndicator.GetComponent<RectTransform>().localPosition = new Vector3(0f, -336f, 0f);

        fishScript.SetValues(catchedFish.Rarity * 50f + 150f);
        fishScript.InitJump();
    }

    public void CatchFish()
    {
        playerData.unlockedFishes.Add(catchedFish);
        playerData.fishCought++;
        playerData.coinsEarned += catchedFishWeight * catchedFish.PriceCoef;
        PlayerPrefsManager.SavePlayerData(playerData);

        uiManager.UpdateStats(playerData);

        catchedFishSprite.sprite = catchedFish.Image;
        catchedFishGO.SetActive(true);
        catchedFishAnimator.SetTrigger("Catched");
    }

    private void OnCastAnimationEnds()
    {
        SetAnimatorState(2);
    }

    public void PullTextTrigger()
    {
        pullTextAnimator.SetTrigger("ShowUp");
    }
}
