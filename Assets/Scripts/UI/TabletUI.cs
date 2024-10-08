using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum State
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
    [SerializeField] private List<GameObject> tabeltCanvas;
    [SerializeField] private Image TabletBG;

    private State currentState = State.Quest;
    public State CurrentState => currentState;

    public UnityEvent onDisable;
    
    private void Start()
    {
        tabeltButton.onClick.AddListener(() => SetAnimator());
        for(int i = 0; i<stateButtons.Count; i++)
        {
            switch(i)
            {
                case 0:
                    stateButtons[i].onClick.AddListener(() => 
                    { 
                        currentState = State.Quest;
                        tabeltCanvas[0].SetActive(true);
                        tabeltCanvas[1].SetActive(false);
                        tabeltCanvas[2].SetActive(false);    
                    });
                    break;
                case 1:
                    stateButtons[i].onClick.AddListener(() => 
                    { 
                        currentState = State.Inventory;
                        tabeltCanvas[0].SetActive(false);
                        tabeltCanvas[1].SetActive(true);
                        tabeltCanvas[2].SetActive(false);
                    });
                    break;
                case 2:
                    stateButtons[i].onClick.AddListener(() => 
                    { 
                        currentState = State.Setting;
                        tabeltCanvas[0].SetActive(false);
                        tabeltCanvas[1].SetActive(false);
                        tabeltCanvas[2].SetActive(true);
                    });
                    break;
                default:
                    break;
            }
            
        }
    }

    private void SetAnimator(bool isFree = true)
    {
        bool isOn = tabeltAnimator.GetBool("IsOn");
        tabeltAnimator.SetBool("IsOn", !isOn);
        tabeltAnimator.SetTrigger("ButtonClick");

        TabletBG.gameObject.SetActive(!isOn);

        tabeltCanvas[(int)currentState].SetActive(!isOn);
        if (isOn)
        {
            onDisable?.Invoke();
        }

        if (!isFree) return;
        for (int i = 0; i < stateButtons.Count; i++)
        {
            stateButtons[i].gameObject.SetActive(!isOn);
        }
    }


    public void TurnOnTablet(State state)
    {
        currentState = state;
        SetAnimator(true);
    }

    public bool IsTabletOn()
    {
        return tabeltAnimator.GetBool("IsOn");
    }
}
