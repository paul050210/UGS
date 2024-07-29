using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviourSingleton<QuestManager>
{
    private List<DayInfo> days = new List<DayInfo>();
    private int dayCount = 2;
    private int questIndex = 0;
    private int acceptIndex = -1;

    private List<Quest> enableQuests= new List<Quest>();
    public List<Quest> EnableQuests => enableQuests;

    private List<KeyValuePair<Quest, int>> acceptQuests = new List<KeyValuePair<Quest, int>>();

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
        for(int i = 0; i<acceptQuests.Count; i++)
        {
            if(acceptQuests[i].Value == GameManager.Instance.CurrentDay && i > acceptIndex)
            {
                acceptIndex = i;
                return acceptQuests[i].Key;
            }
        }

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

    public void AddAcceptQuest(Quest q)
    {
        KeyValuePair<Quest, int> pair = new KeyValuePair<Quest, int>(q, GameManager.Instance.CurrentDay + q.DurationTime);
        acceptQuests.Add(pair);
    }

    public void SetQuestIndex(int index)
    {
        questIndex = index;
    }

    public void ResetIndex()
    {
        questIndex = 0;
        acceptIndex = -1;
    }

    public void AddQuestIndex()
    {
        questIndex++;
    }
}
