using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviourSingleton<QuestManager>
{
    Quest sampleQ;

    private void Start()
    {
        sampleQ = new Quest(0, 10);
        Debug.Log(GetQuest(sampleQ)[0].strValue);
    }

    public List<DefaultTable.Data> GetQuest(Quest q)
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
