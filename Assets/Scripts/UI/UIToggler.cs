using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggler : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    public void ToggleUI()
    {
        menuPanel.SetActive(!menuPanel.activeInHierarchy);
    }
}
