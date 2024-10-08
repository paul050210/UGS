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
        ResetContents();
        rectTransform.sizeDelta = new Vector2(0f, (datas.Count + 1) * 100f);
        float yPos = Mathf.Max(0f, (datas.Count - 5) * 100);
        rectTransform.anchoredPosition = new Vector2(0f, yPos);
        for (int i = 0; i<datas.Count; i++) 
        {
            string txt = datas[i].strValue;
            bool isLeft = datas[i].name != "�Ͽ�";
            contents[i].SetText(txt, isLeft, i);
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
            contents[i].SetText(" ", true, 0);
        }
        SetContentSize(0f);
    }
}
