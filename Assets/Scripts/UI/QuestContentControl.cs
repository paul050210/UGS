using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestContentControl : MonoBehaviour
{
    private QuestContentUI[] contents;
    private RectTransform rectTransform;

    private void Start()
    {
        contents = GetComponentsInChildren<QuestContentUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetContents(List<DefaultTable.Data> datas)
    {
        if(rectTransform == null)
        {
            Debug.Log("no rect");
            rectTransform = GetComponent<RectTransform>();
        }
        if(contents == null) 
        {
            Debug.Log("no contents");
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
}
