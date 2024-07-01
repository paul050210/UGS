using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

enum State
{
    Quest,
    Inventory,
    Setting
};
public class TabletUI : MonoBehaviour
{
    [SerializeField] private Button tabeltButton;
    [SerializeField] private Animator tabeltAnimator;
    [SerializeField] private List<Button> stateButtons;
    [SerializeField] private List<Canvas> tabeltCanvas;
    private State currentState = State.Quest;
    
    private void Start()
    {
        tabeltButton.onClick.AddListener(SetAnimator);
        for(int i = 0; i<stateButtons.Count; i++)
        {
            switch(i)
            {
                case 0:
                    stateButtons[i].onClick.AddListener(() => 
                    { 
                        currentState = State.Quest;
                        tabeltCanvas[0].gameObject.SetActive(true);
                        tabeltCanvas[1].gameObject.SetActive(false);
                        tabeltCanvas[2].gameObject.SetActive(false);    
                    });
                    break;
                case 1:
                    stateButtons[i].onClick.AddListener(() => 
                    { 
                        currentState = State.Inventory;
                        tabeltCanvas[0].gameObject.SetActive(false);
                        tabeltCanvas[1].gameObject.SetActive(true);
                        tabeltCanvas[2].gameObject.SetActive(false);
                    });
                    break;
                case 2:
                    stateButtons[i].onClick.AddListener(() => 
                    { 
                        currentState = State.Setting;
                        tabeltCanvas[0].gameObject.SetActive(false);
                        tabeltCanvas[1].gameObject.SetActive(false);
                        tabeltCanvas[2].gameObject.SetActive(true);
                    });
                    break;
                default:
                    break;
            }
            
        }
    }

    private void SetAnimator()
    {
        bool isOn = tabeltAnimator.GetBool("IsOn");
        tabeltAnimator.SetBool("IsOn", !isOn);
        currentState = State.Quest;

        if(isOn)
        {
            tabeltCanvas[0].gameObject.SetActive(false);
            tabeltCanvas[1].gameObject.SetActive(false);
            tabeltCanvas[2].gameObject.SetActive(false);
        }
        else
        {
            tabeltCanvas[0].gameObject.SetActive(true);
        }

        for(int i = 0; i<stateButtons.Count; i++)
        {
            stateButtons[i].gameObject.SetActive(!isOn);
        }
    }
    
}
