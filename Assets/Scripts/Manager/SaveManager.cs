using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class SaveManager : MonoBehaviourSingleton<SaveManager>
{
    private string GameSettingDataFileName = "GameSettingData.json";
    private string ItemDataFileName = "ItemData.json";
    private string ItemDicDataFileName = "ItemDicData.json";
    private string DayDataFileName = "DayData.json";

    public GameData gameSettingData = new GameData();
    public Dictionary<Item, int> itemMap = new Dictionary<Item, int>();
    public Dictionary<Item, bool> itemDicMap = new Dictionary<Item, bool>();

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
            itemMap = DictionaryJsonUtility.FromJson<Item, int>(FromJsonData);
            
        }
        else
        {
            ItemManager.Instance.ResetMap();
        }
    }

    public void SaveItemData()
    {
        string ToJsonData = DictionaryJsonUtility.ToJson(itemMap, true);
        string filePath = Application.dataPath + "/" + ItemDataFileName;

        File.WriteAllText(filePath, ToJsonData);
    }

    public void LoadItemDicData()
    {
        string filePath = Application.dataPath + "/" + ItemDicDataFileName;

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            itemDicMap = DictionaryJsonUtility.FromJson<Item, bool>(FromJsonData);
        }
        else
        {
            ItemManager.Instance.ResetItemDic();
        }
    }

    public void SaveItemDicData()
    {
        string ToJsonData = DictionaryJsonUtility.ToJson(itemDicMap, true);
        string filePath = Application.dataPath + "/" + ItemDicDataFileName;

        File.WriteAllText(filePath, ToJsonData);
    }

    public int LoadDay()
    {
        int day = 1;
        string filePath = Application.dataPath +"/" + DayDataFileName;   

        if(File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            day = JsonUtility.FromJson<Day>(FromJsonData).dayInt;
        }
        return day;
    }

    public void SaveDay(Day day)
    {
        string ToJsonData = JsonUtility.ToJson(day);
        Debug.Log($"ToJsonData{ToJsonData}");
        string filepath = Application.dataPath + "/" + DayDataFileName;

        File.WriteAllText(filepath, ToJsonData);
    }
}
