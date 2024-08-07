using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestContentUI : MonoBehaviour
{
    [SerializeField] private Text thisTxt;

    public void SetText(string text, bool isLeft, int index)
    {
        thisTxt.text = text;
        if(isLeft)
        {
            thisTxt.alignment = TextAnchor.MiddleLeft;
        }
        else
        {
            thisTxt.alignment = TextAnchor.MiddleRight;
        }
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f - (100f * index));
    }
}
