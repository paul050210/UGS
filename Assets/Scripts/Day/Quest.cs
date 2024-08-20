using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    Default,
    Accept,
    Refuse,
    Done,
    Perfect,
    Half,
    Fail
}

[CreateAssetMenu(menuName = "QuestSO")]
public class Quest : ScriptableObject
{
    public int reject;

    //TODO: 퀘스트 등장 조건

    //대화 내용
    [Header("퀘스트 요약")]
    [SerializeField] private string simpleTxt;
    public string SimpleTxt => simpleTxt;

    [Header("기본 대화 내용")]
    [SerializeField] private int startIndex;
    public int StartIndex => startIndex;
    [SerializeField] private int endIndex;
    public int EndIndex => endIndex;

    [Header("수락시 대화 내용")]
    [SerializeField] private int acceptStart;
    public int AcceptStart => acceptStart;
    [SerializeField] private int acceptEnd;
    public int AcceptEnd => acceptEnd;

    [Header("거절시 대화 내용")]
    [SerializeField] private int refuseStart;
    public int RefuseStart => refuseStart;
    [SerializeField] private int refuseEnd;
    public int RefuseEnd => refuseEnd;

    public int scriptLength => (endIndex - startIndex + 1);

    [SerializeField] private ItemSO[] needItems;
    [SerializeField] private ItemSO halfItem;
    public ItemSO HalfItem => halfItem;
    [SerializeField] private List<ItemSO> rewardItems;

    [SerializeField] private Sprite charSprite;
    public Sprite CharSprite => charSprite;
    [SerializeField] private Sprite simpleCharSprite;
    public Sprite SimpleCharSprite => simpleCharSprite;

    [SerializeField] private int durationTime;
    public int DurationTime => durationTime;

    [HideInInspector]
    public bool isAceepted = false;
    [HideInInspector]
    public QuestState questState = QuestState.Default;

    public List<Item> DoneQuest(Item[] items)
    {
        if (items.Length != needItems.Length) return null;
        Item[] inputArr = items.OrderBy(i => i.itemName).ToArray();
        Item[] thisArr = new Item[needItems.Length];
        for (int i = 0; i < needItems.Length; i++)
        {
            thisArr[i] = needItems[i].item;
        }
        thisArr = thisArr.OrderBy(i => i.itemName).ToArray();

        List<Item> returnList = new List<Item>();
        foreach(var itemSO in rewardItems)
        {
            returnList.Add(itemSO.item);
        }


        if (Enumerable.SequenceEqual(inputArr, thisArr))
            return returnList;

        return null;
    }

    public bool CanDoQuest()
    {
        return true;
    }

    /// <summary>
    /// 데이터 리스트 반환해주는 함수
    /// </summary>
    /// <param name="type">0은 Quest, 1은 수락, 2는 거절, 3은 퀘스트 완료할때, 4는 완벽성공, 5는 중간성공, 6은 실패</param>
    /// <returns></returns>
    public List<DefaultTable.Data> GetText(int type)
    {
        int start;
        int end;
        switch(type)
        {
            case 0:
                start = StartIndex;
                end = endIndex;
                break;
            case 1:
                start = acceptStart;
                end = acceptEnd;
                break;
            case 2:
                start = refuseStart;
                end = refuseEnd;
                break;
            case 3:
                start = refuseEnd+1;
                end = refuseEnd+1;
                break;
            case 4:
                start = refuseEnd+2;
                end = refuseEnd + 2;
                break;
            case 5:
                start = refuseEnd+3;
                end = refuseEnd+3;
                break;
            case 6:
                start = refuseEnd +4;
                end = refuseEnd+4;
                break;
            default:
                start = StartIndex;
                end = endIndex;
                break;
        }
        var datas = DefaultTable.Data.GetList();
        List<DefaultTable.Data> returnList = new List<DefaultTable.Data>();
        for (int i = start; i <= end; i++)
        {
            returnList.Add(datas[i]);
        }
        return returnList;
    }

    public List<DefaultTable.Data> GetText()
    {
        var datas = DefaultTable.Data.GetList();
        List<DefaultTable.Data> returnList = new List<DefaultTable.Data>();
        int start = StartIndex;
        int end = endIndex;
        
        for (int i = start; i <= end; i++)
        {
            returnList.Add(datas[i]);
        }

        if (questState == QuestState.Accept || questState == QuestState.Refuse)
        {
            if(isAceepted)
            {
                start = acceptStart;
                end = acceptEnd;
            }
            else
            {
                start = refuseStart;
                end = refuseEnd;
            }
            for (int i = start; i <= end; i++)
            {
                returnList.Add(datas[i]);
            }
        }
        else if(questState == QuestState.Done || questState == QuestState.Perfect || questState == QuestState.Half || questState == QuestState.Fail)
        {
            start = refuseEnd + 1;
            end = refuseEnd + 1;
            for (int i = start; i <= end; i++)
            {
                returnList.Add(datas[i]);
            }

            if(questState == QuestState.Perfect)
            {
                start = refuseEnd + 2;
                end = refuseEnd + 2;
            }
            else if(questState == QuestState.Half)
            {
                start = refuseEnd + 3;
                end = refuseEnd + 3;
            }
            else if(questState == QuestState.Fail)
            {
                start = refuseEnd + 4;
                end = refuseEnd + 4;
            }
            for (int i = start; i <= end; i++)
            {
                returnList.Add(datas[i]);
            }
        }
        else
        {
            return null;
        }

        return returnList;
    }

    public override int GetHashCode()
    {
        return startIndex.GetHashCode() + endIndex.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        Quest q = obj as Quest;
        return q != null && (q.StartIndex == this.startIndex) && (q.EndIndex == this.endIndex);
    }
}
