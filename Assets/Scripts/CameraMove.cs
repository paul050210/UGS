using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    private Vector3 dir = Vector3.zero;
    private bool isMoving = false;
    private float moveDelay = 0.7f;
    [SerializeField] private GameObject[] canvases;
    private int temp;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            if (isMoving) return;
            isMoving = true;
            temp = Mathf.FloorToInt(dir.y / 90f);
            dir.y += 90f;
            if (dir.y == 360) dir.y = 0f;
            canvases[Mathf.FloorToInt(dir.y / 90f)].SetActive(true);
            transform.DORotate(dir, moveDelay).SetEase(Ease.Linear).OnComplete(() => 
            { 
                isMoving = false;
                canvases[temp].SetActive(false);
            });
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isMoving) return;
            isMoving = true;
            temp = Mathf.FloorToInt(dir.y / 90f);
            dir.y -= 90f;
            if (dir.y < 0) dir.y = 270f;
            canvases[Mathf.FloorToInt(dir.y / 90f)].SetActive(true);
            transform.DORotate(dir, moveDelay).SetEase(Ease.Linear).OnComplete(() => 
            { 
                isMoving = false;
                canvases[temp].SetActive(false);
            });
        }
    }
}
