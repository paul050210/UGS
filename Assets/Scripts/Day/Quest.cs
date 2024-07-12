using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Quest
{
    //����Ʈ ���� ����(�߰��ؾߵ�)

    //��ȭ ����(�Ƹ��� ��Ʈ�� �迭�� �Ҳ� �ƴ�)
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
