using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Quest
{
    //퀘스트 등장 조건(추가해야됨)

    //대화 내용(아마도 스트링 배열로 할꺼 아님)
    private string[] scripts;
    private string simpleTxt;

    private int startIndex;
    public int StartIndex => startIndex;
    private int endIndex;
    public int EndIndex => endIndex;

    private Item[] needItems;
    private List<Item> rewardItems;

    private int durationTime;

    public List<Item> DoneQuest(Item[] items)
    {
        if (items.Length != needItems.Length) return null;
        Item[] inputArr = items.OrderBy(i => i.itemName).ToArray();
        Item[] thisArr = needItems;
        thisArr = thisArr.OrderBy(i => i.itemName).ToArray();

        if (Enumerable.SequenceEqual(inputArr, thisArr))
            return rewardItems;

        return null;
    }



    public Quest(int start, int end)
    {
        startIndex = start;
        endIndex = end;
    }
}
