using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SouthUI : MonoBehaviour
{
    [SerializeField] private Button doorButton;

    private void Start()
    {
        doorButton.onClick.AddListener(OnClickDoor);
    }

    private void OnClickDoor()
    {
        GameManager.Instance.MoveToNextDay();
    }
}
