using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcContentUI : MonoBehaviour
{
    [SerializeField] private Image charImg;
    [SerializeField] private Text contentText;
    [SerializeField] private Button button;
    private List<DefaultTable.Data> dataList;

    
    public void NpcContentInit(Sprite img, string text, List<DefaultTable.Data> datas)
    {
        charImg.sprite = img;
        contentText.text = text;
        dataList = datas;
        button.onClick.AddListener(() => QuestContentControl.Instance.SetContents(dataList));
    }

    public void NpcContentReset()
    {
        charImg.sprite = null;
        contentText.text = null;
        dataList = null;
        button.onClick.RemoveAllListeners();
    }
}
