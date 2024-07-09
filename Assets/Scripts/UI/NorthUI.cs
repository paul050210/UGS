using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class NorthUI : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private GameObject blackBox1;
    [SerializeField] private GameObject blackBox2;

    private Camera cam;
    private TabletUI tabletUI;
    private float fov = 60f;
    private float camY = 1f;
    private float camCloseDuration = 0.3f;



    private void Start()
    {
        cam = Camera.main;
        btn.onClick.AddListener(ZoomAndOut);
    }

    private void ZoomAndOut()
    {
        if(fov == 60f)
        {
            fov = 40f;
            camY = 2.5f;
        }
        else
        {
            fov = 60f;
            camY = 1f;
            blackBox1.SetActive(false);
            blackBox2.SetActive(false);
        }
        cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
        cam.gameObject.transform.DOMoveY(camY, camCloseDuration).SetEase(Ease.Linear).OnComplete(() => 
        {
            if (fov != 40) return;
            blackBox1.SetActive(true); 
            blackBox2.SetActive(true);
        });
    }
}
