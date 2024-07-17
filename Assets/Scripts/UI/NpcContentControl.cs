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
        //ResetContents();
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
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        if (contents == null)
        {
            contents = GetComponentsInChildren<NpcContentUI>();
        }

        for (int i = 0; i < contents.Length; i++)
        {
            contents[i].gameObject.SetActive(false);
        }
        SetContentSize(0f);
    }
}
