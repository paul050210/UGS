using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviourSingleton<QuestManager>
{
    private List<DayInfo> days = new List<DayInfo>();
    private int dayCount = 1;
    private int questIndex = 0;

    private List<Quest> enableQuests= new List<Quest>();
    public List<Quest> EnableQuests => enableQuests;

    private void Start()
    {
        DayInfo day;
        for(int i = 0; i< dayCount; i++)
        {
            day = Resources.Load<DayInfo>($"SO/Day/day{i + 1}");
            days.Add(day);
        }

    }

    public Quest GetQuest()
    {
        if(GameManager.Instance.CurrentDay - 1 >= days.Count)
        {
            Debug.LogWarning("Day없음");
            return null;
        }
        if (questIndex >= days[GameManager.Instance.CurrentDay - 1].Quests.Count)
            return null;
        return days[GameManager.Instance.CurrentDay - 1].Quests[questIndex];
    }

    public void AddEnableQuest(Quest q)
    {
        if(enableQuests.Contains(q))
        {
            Debug.Log("중복있음");
            return;
        }
        enableQuests.Add(q);
    }

    public void RemoveEnableQuest(Quest q) 
    {
        if(!enableQuests.Contains(q))
        {
            Debug.LogWarning("퀘스트를 보유하고 있지 않은데 제거하려고함");
            return;
        }
        enableQuests.Remove(q);
    }

    public void SetQuestIndex(int index)
    {
        questIndex = index;
    }

    public void AddQuestIndex()
    {
        questIndex++;
    }
}
