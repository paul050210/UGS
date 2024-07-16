using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestContentUI : MonoBehaviour
{
    [SerializeField] private Text thisTxt;

    public void SetText(string text, bool isLeft)
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
    }
}
