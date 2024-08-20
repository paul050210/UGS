using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;

public class QuestManager : MonoBehaviourSingleton<QuestManager>
{
    private List<DayInfo> days = new List<DayInfo>();
    private int dayCount = 2;
    private int questIndex = 0;
    private int acceptIndex = -1;

    private List<Quest> enableQuests = new List<Quest>();
    public List<Quest> EnableQuests => enableQuests;

    private List<KeyValuePair<Quest, int>> acceptQuests = new List<KeyValuePair<Quest, int>>();

    [SerializeField]
    public List<QuestInfo> questList = new List<QuestInfo>();

    public void SetDay()
    {
        DayInfo day;
        for (int i = 0; i < dayCount; i++)
        {
            day = Resources.Load<DayInfo>($"SO/Day/day{i + 1}");
            days.Add(day);
        }

    }

    public Quest GetQuest()
    {
        //받은가 있다면 퀘스트의 약속 기간인지 체크 맞으면 해당 퀘스트 반환
        for (int i = 0; i < acceptQuests.Count; i++)
        {
            if (acceptQuests[i].Value == GameManager.Instance.CurrentDay && i > acceptIndex)
            {
                acceptIndex = i;
                return acceptQuests[i].Key;
            }
        }
        //Day Date 체크
        if (GameManager.Instance.CurrentDay - 1 >= days.Count)
        {
            Debug.LogWarning("Day없음");
            return null;
        }
        //뭔가 오류 체크
        if (questIndex >= days[GameManager.Instance.CurrentDay - 1].Quests.Count)
            return null;
        //해당 날짜의 퀘스트를 반환한다...
        days[GameManager.Instance.CurrentDay - 1].Quests[questIndex].questState = QuestState.Default;
        return days[GameManager.Instance.CurrentDay - 1].Quests[questIndex];
    }
    public Quest GetQuest2(int theDay, int index)
    {
        questIndex = index;
        //해당 날짜의 퀘스트를 반환한다...
        days[theDay - 1].Quests[index].questState = QuestState.Default;
        return days[theDay - 1].Quests[index];
    }

    public void AddEnableQuest(Quest q)
    {
        if (enableQuests.Contains(q))
        {
            Debug.Log("중복있음");
            return;
        }
        enableQuests.Add(q);
        q.reject = 0;
    }

    public void RemoveEnableQuest(Quest q)
    {
        if (!enableQuests.Contains(q))
        {
            Debug.LogWarning("퀘스트를 보유하고 있지 않은데 제거하려고함");
            return;
        }
        enableQuests.Remove(q);
        q.reject = 1;
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
    public void AddQuestInfo(bool isAccept)
    {
        QuestInfo questinfo = new QuestInfo();
        questinfo.SetData(GameManager.Instance.CurrentDay, questIndex, isAccept);
        QuestManager.Instance.questList.Add(questinfo);
    }
    public void SaveQuestIndex()
    {
        foreach (Quest quest in EnableQuests)
        {
            PlayerPrefs.SetInt("quest" + questIndex, quest.reject);
        }
        PlayerPrefs.SetInt("length", EnableQuests.Count);

    }

    public void LoadQuestIndex()
    {
        for(int i = 0; i<PlayerPrefs.GetInt("length"); i++)
        {
            int num = PlayerPrefs.GetInt("quest" + i);
            //acceptQuests[num].Value
        }
    }
}
