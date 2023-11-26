using System;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class JsonDataService : IDataService
{
    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;
        
     
            try
            {
                if (File.Exists(path))
                {
                    Debug.Log("Data exists. Deleting old file and writing a new one");
                    File.Delete(path);
                }
                else
                {
                    Debug.Log("Writing File to the first time!");
                }
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string jsonData = JsonConvert.SerializeObject(Data, Formatting.Indented, settings);

                using (FileStream stream = File.Create(path))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(jsonData);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Unable to save data due to: {e.Message}{e.StackTrace}");
                return false;
            }
    }
    
    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        try
        {
            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                T loadedData = JsonConvert.DeserializeObject<T>(jsonData);
                return loadedData;
            }
            else
            {
                Debug.LogWarning("No data found at the specified path.");
                return default(T); // Return default value if no data is found
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to load data due to: {e.Message}{e.StackTrace}");
            return default(T); // Return default value in case of an exception
        }
    }

}
