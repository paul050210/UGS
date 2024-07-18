using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum State
{
    Quest,
    Inventory,
    Setting,
    TradeInventory // TradeInventory �߰�
};

public class TabletUI : MonoBehaviour
{
    [SerializeField] private Button tabletButton;
    [SerializeField] private Animator tabletAnimator;
    [SerializeField] private List<Button> stateButtons;
    [SerializeField] private List<GameObject> tabletCanvas;
    private State currentState = State.Quest;

    public UnityEvent onDisable;

    private string currentScene;

    private void Start()
    {
        // ���� �� �̸� ��������
        currentScene = SceneManager.GetActiveScene().name;

        // tabletButton Ŭ�� �̺�Ʈ ����
        tabletButton.onClick.AddListener(() => SetAnimator());

        // ���� ��ư�鿡 ���� Ŭ�� �̺�Ʈ ����
        for (int i = 0; i < stateButtons.Count; i++)
        {
            int index = i; // ���� ������ ĸó
            stateButtons[index].onClick.AddListener(() => SwitchState(index));
        }

        UpdateStateButtons();
    }

    private void SetAnimator(bool isFree = true)
    {
        bool isOn = tabletAnimator.GetBool("IsOn");
        tabletAnimator.SetBool("IsOn", !isOn);
        tabletAnimator.SetTrigger("ButtonClick");

        tabletCanvas[(int)currentState].SetActive(!isOn);
        if (isOn)
        {
            onDisable.Invoke();
        }

        if (!isFree) return;
        foreach (var button in stateButtons)
        {
            button.gameObject.SetActive(!isOn);
        }
    }

    public void TurnOnTablet(State state)
    {
        currentState = state;
        SetAnimator(true);
    }

    public bool IsTabletOn()
    {
        return tabletAnimator.GetBool("IsOn");
    }

    private void SwitchState(int index)
    {
        if (index == (int)State.Inventory && currentScene == "westScene")
        {
            currentState = State.TradeInventory;
        }
        else
        {
            currentState = (State)index;
        }

        for (int i = 0; i < tabletCanvas.Count; i++)
        {
            tabletCanvas[i].SetActive(i == (int)currentState);
        }
    }

    private void UpdateStateButtons()
    {
        for (int i = 0; i < stateButtons.Count; i++)
        {
            if (i == (int)State.Inventory)
            {
                stateButtons[i].gameObject.SetActive(currentScene != "westScene");
            }
            else if (i == (int)State.TradeInventory)
            {
                stateButtons[i].gameObject.SetActive(currentScene == "westScene");
            }
            else
            {
                stateButtons[i].gameObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        currentState = State.Quest;
        UpdateStateButtons(); // ���� Ȱ��ȭ�� �� ��ư ���� ������Ʈ
    }
}
