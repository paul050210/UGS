using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletUI : MonoBehaviour
{
    [SerializeField] private Button tabeltButton;
    [SerializeField] private Animator tabelAnimator;
    
    private void Start()
    {
        tabeltButton.onClick.AddListener(SetAnimator);
    }

    private void SetAnimator()
    {
        bool isOn = tabelAnimator.GetBool("IsOn");
        tabelAnimator.SetBool("IsOn", !isOn);
    }
    
}
