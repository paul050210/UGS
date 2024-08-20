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
    private string QuestInfoDataFileName = "QuestInfo.json";

    public GameData gameSettingData = new GameData();
    public Dictionary<Item, int> itemMap = new Dictionary<Item, int>();
    public Dictionary<Item, bool> itemDicMap = new Dictionary<Item, bool>();
    public List<QuestInfo> questList = new List<QuestInfo>();

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
    public void SaveQuestInfo()
    {
        // questList가 비어있는지 확인
        if (QuestManager.Instance.questList == null || QuestManager.Instance.questList.Count == 0)
        {
            Debug.LogWarning("Quest list is empty or null.");
            return;
        }

        // 리스트를 JSON 문자열로 변환
        string json = JsonUtility.ToJson(new QuestListWrapper(QuestManager.Instance.questList), true);

        Debug.Log($"ToJsonData: {json}");
        string filepath = Path.Combine(Application.dataPath, QuestInfoDataFileName);

        // JSON 데이터를 파일로 저장
        File.WriteAllText(filepath, json);
    }
    public void LoadQuestInfo()
    {
        string filePath = Path.Combine(Application.dataPath, QuestInfoDataFileName);

        if (File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            QuestListWrapper wrapper = JsonUtility.FromJson<QuestListWrapper>(fromJsonData);

            if (wrapper != null)
            {
                questList = wrapper.questList;
                Debug.Log("Quest info loaded successfully.");
            }
            else
            {
                Debug.LogWarning("Failed to load quest info: wrapper is null.");
            }
        }
        else
        {
            Debug.LogWarning($"Quest info file not found at {filePath}");
        }

        if(questList.Count>0)
        {
            for(int i = 0; i < questList.Count; i++)
            {
                Quest quest = QuestManager.Instance.GetQuest2(questList[i].day, questList[i].index);

                quest.isAceepted = true;
                quest.questState = QuestState.Accept;
                QuestManager.Instance.AddEnableQuest(quest);
                //QuestContentControl.Instance.SetContents(quest);

                if (questList[i].isAccept)
                {
                    //curDatas = goingQuest.GetText(1);
                    QuestManager.Instance.AddAcceptQuest(quest);
                    QuestManager.Instance.AddQuestInfo(true);
                }
                else
                {
                    //curDatas = goingQuest.GetText(2);
                    QuestManager.Instance.RemoveEnableQuest(quest);
                    quest.isAceepted = false;
                    quest.questState = QuestState.Refuse;
                    QuestManager.Instance.AddEnableQuest(quest);
                    QuestManager.Instance.AddQuestInfo(false);
                }
            }

            if(GameManager.Instance.CurrentDay == questList[questList.Count-1].day)
            {
                QuestManager.Instance.SetQuestIndex(questList[questList.Count-1].index);
            }
            else
            {
                QuestManager.Instance.SetQuestIndex(0);
            }
        }
    }

    [System.Serializable]
    public class QuestListWrapper
    {
        public List<QuestInfo> questList;

        public QuestListWrapper(List<QuestInfo> list)
        {
            questList = list;
        }
    }
}
