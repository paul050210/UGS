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



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            if (isMoving) return;
            isMoving = true;
            canvases[Mathf.FloorToInt(dir.y / 90f) % 4].SetActive(false);
            dir.y += 90f;
            if (dir.y < 0) dir.y = 270f;
            transform.DORotate(dir, moveDelay).SetEase(Ease.Linear).OnComplete(() => 
            { 
                isMoving = false;
                canvases[Mathf.FloorToInt(dir.y / 90f) % 4].SetActive(true);
            });
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isMoving) return;
            isMoving = true;
            canvases[Mathf.FloorToInt(dir.y / 90f) % 4].SetActive(false);
            dir.y -= 90f;
            if (dir.y < 0) dir.y = 270f;
            transform.DORotate(dir, moveDelay).SetEase(Ease.Linear).OnComplete(() => 
            { 
                isMoving = false;
                canvases[Mathf.FloorToInt(dir.y / 90f) % 4].SetActive(true);
            });
        }
    }
}
