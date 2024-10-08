using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private int currentDay;
    public int CurrentDay => currentDay;
    
    [HideInInspector] public UnityEvent OnDayChanged;
    [SerializeField] private QuestMainUI questMain;

    private void Awake()
    {
        currentDay = SaveManager.Instance.LoadDay();
        SaveManager.Instance.LoadGameSettingData();
        SaveManager.Instance.LoadItemData();
        //SaveManager.Instance.SaveItemData();
        SaveManager.Instance.LoadItemDicData();
        QuestManager.Instance.SetDay();
        SaveManager.Instance.LoadQuestInfo();
    }

    public void MoveToNextDay()
    {
        currentDay++;
        QuestManager.Instance.ResetIndex();
        OnDayChanged.Invoke();
        questMain.ResetQuestUI();
        //saveday
    }
}
