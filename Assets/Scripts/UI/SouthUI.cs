using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SouthUI : MonoBehaviour
{
    [SerializeField] private Button doorButton;
    [SerializeField] private Image fadeImage;

    private void Start()
    {
        doorButton.onClick.AddListener(OnClickDoor); 
    }

    private void OnClickDoor()
    {
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
            GameManager.Instance.MoveToNextDay();
        });
    }
}

