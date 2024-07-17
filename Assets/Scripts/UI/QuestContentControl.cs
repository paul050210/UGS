using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestContentControl : MonoBehaviourSingleton<QuestContentControl>
{
    private QuestContentUI[] contents;
    private RectTransform rectTransform;

    private void Start()
    {
        contents = GetComponentsInChildren<QuestContentUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        ResetContents();
    }

    public void SetContents(List<DefaultTable.Data> datas)
    {
        if(rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        if(contents == null) 
        {
            contents = GetComponentsInChildren<QuestContentUI>();
        }
        rectTransform.sizeDelta = new Vector2(0f, (datas.Count + 1) * 100f);
        for(int i = 0; i<datas.Count; i++) 
        {
            string txt = datas[i].strValue;
            bool isLeft = datas[i].name != "A";
            contents[i].SetText(txt, isLeft);
        }
    }

    public void SetContentSize(float height)
    {
        rectTransform.sizeDelta = new Vector2(0f, height);
    }

    public void ResetContents()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        if (contents == null)
        {
            contents = GetComponentsInChildren<QuestContentUI>();
        }

        for (int i = 0; i<contents.Length; i++)
        {
            contents[i].SetText(" ", true);
        }
        SetContentSize(0f);
    }
}
