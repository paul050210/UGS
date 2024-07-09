using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EastUI : MonoBehaviour
{
    [SerializeField] private Button mergeButton;
    [SerializeField] private Button decomButton;



    private Camera cam;
    private TabletUI tabletUI;
    private float fov = 60f;
    private float camCloseDuration = 0.3f;
    private Vector3 camvec = new Vector3(0, -1.6f, 5f);

    private void Start()
    {
        cam = Camera.main;
        tabletUI = FindObjectOfType<TabletUI>();
        tabletUI.onDisable.AddListener(() => 
        {
            if (fov == 60f) return;
            fov = 60f;
            camvec = new Vector3(0f, 1f, 0f);
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
            cam.gameObject.transform.DOMove(camvec, camCloseDuration).SetEase(Ease.Linear);
        });
        mergeButton.onClick.AddListener(() => OnClickButton(0));
        decomButton.onClick.AddListener(() => OnClickButton(1));
    }


    private void OnClickButton(int i)
    {
        if (Mathf.Approximately(cam.fieldOfView, 60f))
        {
            fov = 30f;
            if(i == 0)
                camvec = new Vector3(0f, -1.6f, 5f);
            else
                camvec = new Vector3(0f, -1.6f, -3f);
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
                tabletUI.TurnOnTablet(State.Inventory);
        });
    }
}
