using System;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private IDataService _dataService = new JsonDataService();
    private bool _encryptionEnabled;
    private long _saveTime;

    // public void SaveGame( PlayerStats playerStats)
    // {
    //     _playerStats = playerStats;
    //     SerializeJson();
    // }
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
}
