using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FishStorageSO fishStorage;

    [SerializeField] private Sprite lockedImage;

    [SerializeField] private GameObject fishListParent;
    [SerializeField] private GameObject fishButtonPrefab;

    private void Start()
    {
        foreach(var fish in fishStorage.fishes)
        {
            // Debug.Log(fish.Title);

            GameObject newButton = Instantiate(fishButtonPrefab);
            newButton.GetComponent<FishButton>().InitButton(fish);
            newButton.transform.SetParent(fishListParent.transform, false);
        }
    }
}
