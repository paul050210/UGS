using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviourSingleton<QuestManager>
{
    private List<DayInfo> days = new List<DayInfo>();
    private int dayCount = 1;

    Quest currentQ;

    private void Start()
    {
        DayInfo day;
        for(int i = 0; i< dayCount; i++)
        {
            day = Resources.Load<DayInfo>($"SO/Day/day{i + 1}");
            days.Add(day);
        }
        currentQ = days[GameManager.Instance.CurrentDay - 1].Quests[0];

        Debug.Log(GetQuestText(currentQ)[0].strValue);
    }

    public List<DefaultTable.Data> GetQuestText(Quest q)
    {
        var datas = DefaultTable.Data.GetList();
        List<DefaultTable.Data> returnList = new List<DefaultTable.Data>();
        for(int i = q.StartIndex; i<=q.EndIndex; i++)
        {
            returnList.Add(datas[i]);
        }
        return returnList;
    }
}
