using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FishStorageSO fishStorage;

    [Space]
    [SerializeField] private Sprite lockedImage;
    private string lockedFishTitle = "Unknown fish";
    private string lockedFishDescription = "Catch that fish to know more about it";

    [Space]
    [SerializeField] private GameObject fishListParent;
    [SerializeField] private GameObject fishButtonPrefab;

    [Space]
    [SerializeField] private TextMeshProUGUI fishCought;
    [SerializeField] private TextMeshProUGUI coinsEarned;

    [Space]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image image;

    private event Action<List<FishSO>> unlockFishes;

    public void InitUI(PlayerData playerData)
    {
        this.fishCought.text = playerData.fishCought.ToString();
        this.coinsEarned.text = playerData.coinsEarned.ToString();

        ResetDescription();

        InitButtons(playerData.unlockedFishes);
    }

    public void UpdateStats(PlayerData playerData)
    {
        this.fishCought.text = playerData.fishCought.ToString();
        this.coinsEarned.text = playerData.coinsEarned.ToString();

        unlockFishes(playerData.unlockedFishes);
    }

    private void InitButtons(List<FishSO> unlockedFishes)
    {
        foreach (var fish in fishStorage.fishes)
        {
            GameObject newButton = Instantiate(fishButtonPrefab);
            FishButton btn = newButton.GetComponent<FishButton>();
            bool isUnlocked = unlockedFishes.Contains(fish);
            btn.InitButton(fish, isUnlocked);
            btn.FishClickEvent += OnFishClick;

            unlockFishes += btn.OnUnlockEvent;

            newButton.transform.SetParent(fishListParent.transform, false);
        }
    }

    private void ResetDescription()
    {
        title.text = lockedFishTitle;
        description.text = lockedFishDescription;
        image.sprite = lockedImage;
    }

    private void OnFishClick(FishSO fish, bool isUnlocked)
    {
        if (isUnlocked)
        {
            title.text = fish.Title;
            description.text = fish.Description;
            image.sprite = fish.Image;
        }
        else
        {
            ResetDescription();
        }
    }
}
