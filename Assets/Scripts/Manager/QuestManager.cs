using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviourSingleton<QuestManager>
{
    private List<DayInfo> days = new List<DayInfo>();
    private int dayCount = 1;
    private int questIndex = 0;

    private void Start()
    {
        DayInfo day;
        for(int i = 0; i< dayCount; i++)
        {
            day = Resources.Load<DayInfo>($"SO/Day/day{i + 1}");
            days.Add(day);
        }

    }

    public List<DefaultTable.Data> GetQuestText()
    {
        Quest q = days[GameManager.Instance.CurrentDay - 1].Quests[questIndex];
        var datas = DefaultTable.Data.GetList();
        List<DefaultTable.Data> returnList = new List<DefaultTable.Data>();
        for(int i = q.StartIndex; i<=q.EndIndex; i++)
        {
            returnList.Add(datas[i]);
        }
        return returnList;
    }

    public List<DefaultTable.Data> GetAcceptText()
    {
        Quest q = days[GameManager.Instance.CurrentDay - 1].Quests[questIndex];
        var datas = DefaultTable.Data.GetList();
        List<DefaultTable.Data> returnList = new List<DefaultTable.Data>();
        for (int i = q.AcceptStart; i <= q.AcceptEnd; i++)
        {
            returnList.Add(datas[i]);
        }
        return returnList;
    }

    public List<DefaultTable.Data> GetRefuseText()
    {
        Quest q = days[GameManager.Instance.CurrentDay - 1].Quests[questIndex];
        var datas = DefaultTable.Data.GetList();
        List<DefaultTable.Data> returnList = new List<DefaultTable.Data>();
        for (int i = q.RefuseStart; i <= q.RefuseEnd; i++)
        {
            returnList.Add(datas[i]);
        }
        return returnList;
    }

    public Sprite GetQuestImg()
    {
        Quest q = days[GameManager.Instance.CurrentDay - 1].Quests[questIndex];
        return q.CharSprite;
    }

    public void SetQuestIndex(int index)
    {
        questIndex = index;
    }
}
