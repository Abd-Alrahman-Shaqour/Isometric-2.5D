using System;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private IDataService _dataService = new JsonDataService();
    private bool _encryptionEnabled;
    private long _saveTime;
    
    public void SerializeJson(PlayerStats playerStats)
    {
        long startTime = DateTime.Now.Ticks;
        if(_dataService.SaveData("/player-stats.json",playerStats,_encryptionEnabled))
        {
            _saveTime = DateTime.Now.Ticks - startTime;
            Debug.Log($"Save Time {(_saveTime / TimeSpan.TicksPerMillisecond).ToString():NA}ma");
            Debug.Log(playerStats);
        }
        else
        {
            Debug.LogError("Could not save file!");
        }
    }
    public PlayerStats DeserializeJson()
    {
        string filePath = "/player-stats.json";

        // Check if the file exists
        if (File.Exists(Application.persistentDataPath + filePath))
        {
            // If the file exists, load the data
            long startTime = DateTime.Now.Ticks;
            PlayerStats loadedData = _dataService.LoadData<PlayerStats>(filePath, _encryptionEnabled);

            if (loadedData != null)
            {
                _saveTime = DateTime.Now.Ticks - startTime;
                Debug.Log($"Load Time {(_saveTime / TimeSpan.TicksPerMillisecond).ToString():NA}ms");
                Debug.Log(loadedData);

                return loadedData;
            }
            else
            {
                Debug.LogError("Error loading file!");
            }
        }
        else
        {
            // If the file does not exist, create a new PlayerStats object
            PlayerStats newPlayerStats = new PlayerStats();

            // You might want to do something with the newPlayerStats object here

            // Save the newPlayerStats object to the JSON file
            _dataService.SaveData(filePath, newPlayerStats, _encryptionEnabled);

            Debug.Log("New player-stats.json file created!");

            return newPlayerStats;
        }

        return null;
    }
}
