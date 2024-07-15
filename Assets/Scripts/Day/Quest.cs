using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestSO")]
public class Quest : ScriptableObject
{
    //TODO: 퀘스트 등장 조건

    //대화 내용
    private string simpleTxt;

    [SerializeField] private int startIndex;
    public int StartIndex => startIndex;
    [SerializeField] private int endIndex;
    public int EndIndex => endIndex;
    public int scriptLength => (endIndex - startIndex + 1);

    [SerializeField] private ItemSO[] needItems;
    [SerializeField] private List<ItemSO> rewardItems;

    [SerializeField] private Sprite charSprite;
    [SerializeField] private Sprite simpleCharSprite;

    [SerializeField] private int durationTime;

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


    public Quest(int start, int end)
    {
        startIndex = start;
        endIndex = end;
    }
}
