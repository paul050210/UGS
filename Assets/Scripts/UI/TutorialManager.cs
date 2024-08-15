using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public UnityEvent onDisableButtons;

    public void DisableButton()
    {
        GetComponent<Button>().interactable = false;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
