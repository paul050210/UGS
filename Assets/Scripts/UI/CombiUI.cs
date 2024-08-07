using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombiUI : MonoBehaviour
{
    [SerializeField] private Image[] itemImages;
    [SerializeField] private Text[] texts;
    [SerializeField] private Sprite defaultImage;

    public void SetCombi(ItemSO[] baseItems, ItemSO resultItem)
    {
        for(int i = 0; i <= baseItems.Length; i++) 
        {
            itemImages[i].gameObject.SetActive(true);
            if (i == baseItems.Length)
            {
                if (SaveManager.Instance.itemDicMap[resultItem.item])
                    itemImages[i].sprite = resultItem.sprite;
                else
                    itemImages[i].sprite = defaultImage;
            }
            else
            {
                if (SaveManager.Instance.itemDicMap[baseItems[i].item])
                    itemImages[i].sprite = baseItems[i].sprite;
                else
                    itemImages[i].sprite = defaultImage;
            }
        }

        for(int i = 0; i < baseItems.Length-1; i++)
        {
            texts[i].gameObject.SetActive(true);
            texts[i].text = "+";
        }
        texts[baseItems.Length - 1].gameObject.SetActive(true);
        texts[baseItems.Length-1].text = "=";
    }

    public void RestCombi()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            itemImages[i].gameObject.SetActive(false);
        }
        for(int i = 0; i<texts.Length; i++)
        {
            texts[i].gameObject.SetActive(false);
        }
    }
}
