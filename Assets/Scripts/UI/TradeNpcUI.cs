using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TradeNpcUI : MonoBehaviour
{
    [SerializeField] private TradeUI tradeUI;
    [SerializeField] private TradeInfo tradeInfo;
    [SerializeField] private Button moveButton;
    [SerializeField] private Vector3 camVec = Vector3.zero;
    [SerializeField] private Image fadeImage;

    [SerializeField] private string nameStr;
    [SerializeField] private string descStr;
    private Button button;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        button = GetComponent<Button>();
        button.onClick.AddListener(TurnOnTrade);
        moveButton.onClick.AddListener(MoveToTrade);
    }

    private void TurnOnTrade()
    {
        TradeNpcControl.Instance.TurnOffAll();
        cam.DOFieldOfView(30f, 0.3f).SetEase(Ease.Linear);
        cam.gameObject.transform.DOMove(camVec, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            transform.GetChild(0).GetComponent<Text>().text = nameStr;
            transform.GetChild(1).GetComponent<Text>().text = descStr;

        });
    }

    private void MoveToTrade()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(FadeOut());
        });
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.5f);
        fadeImage.DOFade(0, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false);
            ItemManager.Instance.canSelect = true;
            tradeUI.TurnOn(tradeInfo);
        });
    }

    public void TurnOffTrade()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    
}
