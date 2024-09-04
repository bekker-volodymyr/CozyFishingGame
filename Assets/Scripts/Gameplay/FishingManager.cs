using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class FishingManager : MonoBehaviour
{
    [Space]
    [SerializeField] private FishStorageSO fishes;

    #region State Machine

    public StateMachine StateMachine { get;set; }
    public IdleState IdleState { get; set; }
    public CastState CastState { get; set; }
    public WaitState WaitState { get; set; }
    public CatchState CatchState { get; set; }

    #endregion

    #region Cast

    [Header("Casting Objects")]
    [SerializeField] public Image distanceIndicator;

    #endregion

    #region Generated Fish

    private Fish _generatedFish = null;

    #endregion

    #region Catch

    [Header("Catching Objects")]
    [SerializeField] public GameObject CatchingUI;
    [SerializeField] private GameObject bouncingPanel;
    [SerializeField] public Image SuccessIndicator;
    [field: SerializeField] public GameObject FishIndicator { get; set; }
    [field: SerializeField] public GameObject RodIndicator { get; set; }
    [SerializeField] public RodScript rodScript;
    [SerializeField] private FishScript fishScript;

    [Space]
    [SerializeField] private CatchedFish _catchedFishObject;

    #endregion

    #region Fisher Components

    [Header("Fisher Components")]
    [SerializeField] private GameObject fisher;
    [SerializeField] private Animator fisherAnimator;
    [SerializeField] private SpriteRenderer fisherSpriteRenderer;
    [SerializeField] private FisherScript fisherScript;

    #endregion

    #region Menu

    [Header("Menu Components")]
    [SerializeField]  private UIManager uiManager;

    #endregion

    #region Save/Load

    private PlayerData playerData;

    #endregion

    #region Audio

    [Header("Audio Objects")]
    [SerializeField] private AudioSource ambianceAudiouSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip rodCastSound;
    [SerializeField] private AudioClip rodOutSound;
    [SerializeField] private AudioClip successCatch;
    [SerializeField] private AudioClip lostCatch;
    [SerializeField] private AudioClip PullSound;

    #endregion

    #region Text Notifications

    [Header("Notifications")]
    [SerializeField] private TextNotification _textNotification;

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
        _generatedFish = FishGenerator.GenerateFish(distance, fishes);
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

        _generatedFish = null;
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

        RectTransform bouncingPanelRT = bouncingPanel.GetComponent<RectTransform>();

        float panelHeight = bouncingPanelRT.rect.height;
        float topY = panelHeight / 2;

        RectTransform rodRT = rodScript.GetComponent<RectTransform>();

        rodRT.anchoredPosition = new Vector3(0f, 0f + rodRT.rect.height / 2f, 0f);

        fishScript.SetValues(_generatedFish.FishType.Speed, topY, panelHeight * 0.1f);
        fishScript.InitJump();
    }

    public void CatchFish()
    {
        Debug.Log($"{_generatedFish.FishType}");
        if (!playerData.unlockedFishes.Contains(_generatedFish.FishType))
        {
            playerData.unlockedFishes.Add(_generatedFish.FishType);
        }
        playerData.fishCought++;
        playerData.coinsEarned += _generatedFish.Weight * _generatedFish.FishType.PriceCoef;
        PlayerPrefsManager.SavePlayerData(playerData);

        uiManager.UpdateStats(playerData);

        _catchedFishObject.OnCatch(_generatedFish.FishType.Image);
    }

    private void OnCastAnimationEnds()
    {
        SetAnimatorState(2);
    }

    public void TextTrigger(TextType text)
    {
        _textNotification.ShowText(text);
    }

    public void PlaySound(SoundType sound)
    {
        switch(sound)
        {
            case SoundType.SuccessCatch:
                sfxAudioSource.clip = successCatch; break;
            case SoundType.LostCatch:
                sfxAudioSource.clip = lostCatch; break;
            case SoundType.RodCast:
                sfxAudioSource.clip = rodCastSound; break;
            case SoundType.RodBack:
                sfxAudioSource.clip = rodOutSound; break;
            case SoundType.PullSound:
                sfxAudioSource.clip = PullSound; break;
            default:
                Debug.Log($"Unknown sound type: {sound}");
                break;
        }

        sfxAudioSource.Play(); 
    }

    public void SetVolume(float value)
    {
        ambianceAudiouSource.volume = value;
        sfxAudioSource.volume = value;
    }
}
