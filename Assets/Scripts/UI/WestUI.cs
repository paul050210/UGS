using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WestUI : MonoBehaviour
{
    [SerializeField] private Button mapButton;
    [SerializeField] private GameObject mapPanel;

    private void Start()
    {
        mapButton.onClick.AddListener(OpenMap);
    }

    private void OpenMap()
    {
        mapPanel.SetActive(true);
    }
}
