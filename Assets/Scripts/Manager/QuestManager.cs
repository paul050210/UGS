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
        //������ �ִٸ� ����Ʈ�� ��� �Ⱓ���� üũ ������ �ش� ����Ʈ ��ȯ
        for (int i = 0; i < acceptQuests.Count; i++)
        {
            if (acceptQuests[i].Value == GameManager.Instance.CurrentDay && i > acceptIndex)
            {
                acceptIndex = i;
                return acceptQuests[i].Key;
            }
        }
        //Day Date üũ
        if (GameManager.Instance.CurrentDay - 1 >= days.Count)
        {
            Debug.LogWarning("Day����");
            return null;
        }
        //���� ���� üũ
        if (questIndex >= days[GameManager.Instance.CurrentDay - 1].Quests.Count)
            return null;
        //�ش� ��¥�� ����Ʈ�� ��ȯ�Ѵ�...
        days[GameManager.Instance.CurrentDay - 1].Quests[questIndex].questState = QuestState.Default;
        return days[GameManager.Instance.CurrentDay - 1].Quests[questIndex];
    }
    public Quest GetQuest2(int theDay, int index)
    {
        questIndex = index;
        //�ش� ��¥�� ����Ʈ�� ��ȯ�Ѵ�...
        days[theDay - 1].Quests[index].questState = QuestState.Default;
        return days[theDay - 1].Quests[index];
    }

    public void AddEnableQuest(Quest q)
    {
        if (enableQuests.Contains(q))
        {
            Debug.Log("�ߺ�����");
            return;
        }
        enableQuests.Add(q);
        q.reject = 0;
    }

    public void RemoveEnableQuest(Quest q)
    {
        if (!enableQuests.Contains(q))
        {
            Debug.LogWarning("����Ʈ�� �����ϰ� ���� ������ �����Ϸ�����");
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
