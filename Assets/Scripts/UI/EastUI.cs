using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EastUI : MonoBehaviour
{
    [SerializeField] private Button mergeButton;
    [SerializeField] private Button decomButton;
    [SerializeField] private ItemMerge itemMerge;
    [SerializeField] private Button backButton;


    private Camera cam;
    private TabletUI tabletUI;
    private float fov = 60f;
    private float camCloseDuration = 0.3f;
    private Vector3 camvec = new Vector3(0, -1.6f, 5f);

    private void Start()
    {
        cam = Camera.main;
        tabletUI = FindObjectOfType<TabletUI>();
        backButton.onClick.AddListener(() =>
        {
            if (fov == 60f) return;
            tabletUI.TurnOnTablet(State.Inventory);
            fov = 60f;
            itemMerge.SetMergeState(ItemMergeState.None);
            camvec = new Vector3(0f, 1f, 0f);
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
            cam.gameObject.transform.DOMove(camvec, camCloseDuration).SetEase(Ease.Linear);
            ItemManager.Instance.canSelect = false;
            backButton.gameObject.SetActive(false);
        });
        tabletUI.onDisable.AddListener(() => 
        {
            if (fov == 60f) return;
            fov = 60f;
            itemMerge.SetMergeState(ItemMergeState.None);
            camvec = new Vector3(0f, 1f, 0f);
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
            cam.gameObject.transform.DOMove(camvec, camCloseDuration).SetEase(Ease.Linear);
            ItemManager.Instance.canSelect = false;
            backButton.gameObject.SetActive(false);
        });
        mergeButton.onClick.AddListener(() => OnClickButton(0));
        decomButton.onClick.AddListener(() => OnClickButton(1));
    }


    private void OnClickButton(int i)
    {
        if (Mathf.Approximately(cam.fieldOfView, 60f))
        {
            fov = 30f;
            ItemManager.Instance.canSelect = true;
            if (i == 0)
            {
                camvec = new Vector3(0f, 0.8f, 5f);
                itemMerge.SetMergeState(ItemMergeState.Merge);
            }
            else
            {
                camvec = new Vector3(0f, 0.8f, -2.4f);
                itemMerge.SetMergeState(ItemMergeState.Decom);
            }
        }
        else if (Mathf.Approximately(cam.fieldOfView, 30f))
        {
            fov = 60f;
            camvec = new Vector3(0f, 1f, 0f);
        }
        else
            return;

        cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
        cam.gameObject.transform.DOMove(camvec, camCloseDuration).SetEase(Ease.Linear).OnComplete(() => 
        {
            if (fov == 30f)
            {
                tabletUI.TurnOnTablet(State.Inventory);
                backButton.gameObject.SetActive(true);
            }
        });
    }
}
