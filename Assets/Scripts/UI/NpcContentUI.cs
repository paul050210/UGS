using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class NpcContentUI : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image charImg;
    [SerializeField] private Text contentText;
    [SerializeField] private Button button;
    private List<DefaultTable.Data> dataList;

    
    public void NpcContentInit(Sprite img, string text, List<DefaultTable.Data> datas, bool accept)
    {
        charImg.sprite = img;
        contentText.text = text;
        dataList = datas;
        button.onClick.AddListener(() => QuestContentControl.Instance.SetContents(dataList));
        if(!accept)
        {
            buttonImage.color = new Color(0f, 0f, 0f, 200f / 255f);
        }
        else
        {
            buttonImage.color = new Color(255f, 255f, 255f, 1f);
        }
    }

    public void NpcContentReset()
    {
        charImg.sprite = null;
        contentText.text = null;
        dataList = null;
        button.onClick.RemoveAllListeners();
    }
}
