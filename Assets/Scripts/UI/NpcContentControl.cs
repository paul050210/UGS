using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcContentControl : MonoBehaviourSingleton<NpcContentControl>
{
    private NpcContentUI[] contents;
    private RectTransform rectTransform;
    private int lastIndex = 0;


    private void OnEnable()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        if (contents == null)
        {
            contents = GetComponentsInChildren<NpcContentUI>();
        }
        Invoke("ResetContents", 0.01f);
    }

    public void EnableContent(Sprite img, string text, List<DefaultTable.Data> datas)
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        if (contents == null)
        {
            contents = GetComponentsInChildren<NpcContentUI>();
        }
        contents[lastIndex].gameObject.SetActive(true);
        contents[lastIndex].NpcContentInit(img, text, datas);
    }


    public void SetContentSize(float height)
    {
        rectTransform.sizeDelta = new Vector2(0f, height);
    }

    public void ResetContents()
    {
        for (int i = 0; i<contents.Length; i++)
        {
            contents[i].NpcContentReset();
            contents[i].gameObject.SetActive(false);
        }

        for(int i = 0; i<QuestManager.Instance.EnableQuests.Count; i++)
        {
            Debug.Log(contents.Length);
            contents[i].gameObject.SetActive(true);
            Quest q = QuestManager.Instance.EnableQuests[i];
            contents[i].NpcContentInit(q.SimpleCharSprite, q.SimpleTxt, q.GetText(0));
            lastIndex = i + 1;
        }
        SetContentSize(lastIndex * 100f);
    }
}
