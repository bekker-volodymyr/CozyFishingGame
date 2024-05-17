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

    public void InitUI(int fishCought, float coinsEarned)
    {
        this.fishCought.text = fishCought.ToString();
        this.coinsEarned.text = coinsEarned.ToString();

        ResetDescription();

        InitButtons();
    }

    public void UpdateStats(int fishCought, float coinsEarned)
    {
        this.fishCought.text = fishCought.ToString();
        this.coinsEarned.text = coinsEarned.ToString();
    }

    private void InitButtons()
    {
        foreach (var fish in fishStorage.fishes)
        {
            GameObject newButton = Instantiate(fishButtonPrefab);
            FishButton btn = newButton.GetComponent<FishButton>();
            btn.InitButton(fish);
            btn.FishClickEvent += OnFishClick;
            
            newButton.transform.SetParent(fishListParent.transform, false);
        }
    }

    private void ResetDescription()
    {
        title.text = lockedFishTitle;
        description.text = lockedFishDescription;
        image.sprite = lockedImage;
    }

    private void OnFishClick(FishSO fish)
    {
        if (fish.isUnlocked)
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
