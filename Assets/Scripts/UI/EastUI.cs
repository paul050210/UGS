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
    private float camCloseDuration = 0.5f;
    private Vector3 camvec = new Vector3(0, -1.6f, 5f);

    private void Start()
    {
        cam = Camera.main;
        tabletUI = FindObjectOfType<TabletUI>();
        mergeButton.onClick.AddListener(OnClickMerge);
        decomButton.onClick.AddListener(OnClickDecom);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (Mathf.Approximately(cam.fieldOfView, 60f))
            {
                fov = 30f;
                camvec = new Vector3(0f, -1.6f, 5f);
            }
            else if (Mathf.Approximately(cam.fieldOfView, 30f))
            {
                fov = 60f;
                camvec = new Vector3(0f, 1f, 0f);
            }
            else
                return;
                
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
            cam.gameObject.transform.DOMove(camvec, camCloseDuration).SetEase(Ease.Linear);
        }
    }

    private void OnClickMerge()
    {
        if (Mathf.Approximately(cam.fieldOfView, 60f))
        {
            fov = 30f;
            camvec = new Vector3(0f, -1.6f, 5f);
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

    private void OnClickDecom()
    {

    }
}
