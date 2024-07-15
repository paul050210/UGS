using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UGS.Editor.GoogleDriveExplorerGUI;


public class SaveManager : MonoBehaviourSingleton<SaveManager>
{
    private string GameSettingDataFileName = "GameSettingData.json";
    private string ItemDataFileName = "ItemData.json";
    private string DayDataFileName = "DayData.json";

    public GameData gameSettingData = new GameData();
    public Dictionary<Item, int> itemMap = new Dictionary<Item, int>();
    

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
    }

    public void SaveItemData()
    {
        string ToJsonData = DictionaryJsonUtility.ToJson(itemMap, true);
        string filePath = Application.dataPath + "/" + ItemDataFileName;

        File.WriteAllText(filePath, ToJsonData);
    }

    public int LoadDay()
    {
        int day = 0;
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
