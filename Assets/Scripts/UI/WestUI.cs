using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WestUI : MonoBehaviour
{
    [SerializeField] private Button mapButton;
    [SerializeField] private Button mapCloseButton;
    [SerializeField] private Button dictionaryButton;
    [SerializeField] private Button dictionaryCloseButton;
    [SerializeField] private Image leftBtn;
    [SerializeField] private Image rightBtn;
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject dictionaryPanel;

    private void Start()
    {
        mapButton.onClick.AddListener(OpenMap);
        mapCloseButton.onClick.AddListener(CloseMap);
        dictionaryButton.onClick.AddListener(() => 
        { 
            dictionaryPanel.SetActive(true);
            dictionaryCloseButton.gameObject.SetActive(true);
        });
        dictionaryCloseButton.onClick.AddListener(() => 
        {
            dictionaryPanel.SetActive(false);
            dictionaryCloseButton.gameObject.SetActive(false);
        });
    }

    private void OpenMap()
    {
        mapPanel.SetActive(true);
        mapCloseButton.gameObject.SetActive(true);
        leftBtn.raycastTarget = false;
        rightBtn.raycastTarget = false;
    }

    private void CloseMap()
    {
        if(Mathf.Approximately(Camera.main.fieldOfView, 30f))
        {
            TradeNpcControl.Instance.TurnOffAll();
            Camera.main.DOFieldOfView(60f, 0.3f).SetEase(Ease.Linear);
            Camera.main.gameObject.transform.DOMove(new Vector3(0f, 1f, 0f), 0.3f).SetEase(Ease.Linear);
        }
        else if(Mathf.Approximately(Camera.main.fieldOfView, 60f))
        {
            mapPanel.SetActive(false);
            mapCloseButton.gameObject.SetActive(false);
            leftBtn.raycastTarget = true;
            rightBtn.raycastTarget = true;
        }
    }
}
