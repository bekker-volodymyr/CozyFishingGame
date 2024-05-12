using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishesListController : MonoBehaviour
{
    [SerializeField] private FishStorageSO fishes;

    [SerializeField] private GameObject listParent;
    [SerializeField] private GameObject fishButtonPrefab;

    private void Start()
    {
        for(int i = 0; i < fishes.fishes.Count; i++)
        {
            var fishButton = Instantiate(fishButtonPrefab);
            fishButton.transform.SetParent(listParent.transform, false);
        }
    }
}
