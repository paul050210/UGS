using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ItemList
{
    public Dictionary<Item, int> items;
}

public class SaveManager : MonoBehaviourSingleton<SaveManager>
{
    private string GameSettingDataFileName = "GameSettingData.json";
    private string ItemDataFileName = "ItemData.json";

    public GameData gameSettingData = new GameData();
    public Dictionary<Item, int> itemMap = new Dictionary<Item, int>();
    public ItemList itemList;

    public void LoadGameSettingData()
    {
        string filePath = Application.persistentDataPath + "/" + GameSettingDataFileName;

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            gameSettingData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
    }

    public void SaveGameSettingData()
    {
        string ToJsonData = JsonUtility.ToJson(gameSettingData, true);
        string filePath = Application.persistentDataPath + "/" + GameSettingDataFileName;

        File.WriteAllText(filePath, ToJsonData);
    }

    public void LoadItemData()
    {
        string filePath = Application.dataPath + "/" + ItemDataFileName; 
        
        if (File.Exists(filePath)) 
        {
            string FromJsonData = File.ReadAllText(filePath);
            Debug.Log(FromJsonData);
            itemList = new ItemList();
            itemList.items = DictionaryJsonUtility.FromJson<Item, int>(FromJsonData);
            
        }
    }

    public void SaveItemData()
    {
        string ToJsonData = DictionaryJsonUtility.ToJson(itemMap, true);
        string filePath = Application.dataPath + "/" + ItemDataFileName;

        File.WriteAllText(filePath, ToJsonData);
    }
}
