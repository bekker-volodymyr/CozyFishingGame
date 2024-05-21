using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager
{
    public static void SavePlayerData(PlayerData data)
    {
        // Convert the class instance to a JSON string
        string jsonData = JsonUtility.ToJson(data);
        // Save the JSON string to PlayerPrefs
        PlayerPrefs.SetString("PlayerData", jsonData);
        PlayerPrefs.Save();
    }

    public static PlayerData LoadPlayerData()
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            string jsonData = PlayerPrefs.GetString("PlayerData");
            return JsonUtility.FromJson<PlayerData>(jsonData);

            Debug.Log(jsonData);
        }
        else
        {
            return null;
        }
    }
}
