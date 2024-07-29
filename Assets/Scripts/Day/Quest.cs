using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    Default,
    Accept,
    Refuse,
    Done
}

[CreateAssetMenu(menuName = "QuestSO")]
public class Quest : ScriptableObject
{
    //TODO: ����Ʈ ���� ����

    //��ȭ ����
    [Header("����Ʈ ���")]
    [SerializeField] private string simpleTxt;
    public string SimpleTxt => simpleTxt;

    [Header("�⺻ ��ȭ ����")]
    [SerializeField] private int startIndex;
    public int StartIndex => startIndex;
    [SerializeField] private int endIndex;
    public int EndIndex => endIndex;

    [Header("������ ��ȭ ����")]
    [SerializeField] private int acceptStart;
    public int AcceptStart => acceptStart;
    [SerializeField] private int acceptEnd;
    public int AcceptEnd => acceptEnd;

    [Header("������ ��ȭ ����")]
    [SerializeField] private int refuseStart;
    public int RefuseStart => refuseStart;
    [SerializeField] private int refuseEnd;
    public int RefuseEnd => refuseEnd;

    public int scriptLength => (endIndex - startIndex + 1);

    [SerializeField] private ItemSO[] needItems;
    [SerializeField] private ItemSO halfItem;
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
    /// ������ ����Ʈ ��ȯ���ִ� �Լ�
    /// </summary>
    /// <param name="type">0�� Quest, 1�� ����, 2�� ����</param>
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
