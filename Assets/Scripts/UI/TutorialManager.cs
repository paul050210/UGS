using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Reflection;
using UnityEngine.Device;
using DG.Tweening;


public enum Buttons
{
    StartButton,
    WindowButton,
    CounterButton,
    MergeButton,
    DecomButton,
    DoorButton,
    MapButton,
    DictionaryButton,

}

public enum ButtonState
{
    NotActivated,
    Activated,
    Clicked
}


public class TutorialManager : MonoBehaviour
{
    private Dictionary<Buttons, Button> buttonDictionary = new Dictionary<Buttons, Button>();

    [SerializeField] private Button startButton;
    [SerializeField] private Button WindowButton;
    [SerializeField] private Button CounterButton;
    [SerializeField] private Button MergeButton;
    [SerializeField] private Button DecomButton;
    [SerializeField] private Button DoorButton;
    [SerializeField] private Button MapButton;
    [SerializeField] private Button DictionaryButton;

    [SerializeField] private Text nameTxt;
    [SerializeField] private Text mainTxt;

    [SerializeField] public Image blackScreen; 
    [SerializeField] public Image streetImg;
    [SerializeField] public Image HawonImg;
    [SerializeField] public Image HayoungImg;
    [SerializeField] public Image ChanaImg;
    [SerializeField] public Image JackImg;
    [SerializeField] public Image TradeManImg;


    [SerializeField] private RectTransform nameTextBoxTransform; 
    [SerializeField] public RectTransform mainTextBoxTransform; 

    private TabletUI tabletUI;
    private TabletUI titleScene;
    private NorthUI north;
    private InventoryUI inventoryUI;
    private CameraMove cameraMove;


    private bool isTypingDone = false; 
    private bool starNextClicked = false; // �ؽ�Ʈ �ɰ� ����
    public bool lockActivate = false;

    public float fadeDuration = 1.0f; // ���̵� �ƿ� �ð�
    public float moveDuration = 0.3f; // �̵� �ð�
    public int currentIndex = 0; // @(�����) ī��Ʈ �ε���
    private int index = 0;
    private bool allClicked = false;
    private int totalItems = 8; // �⹰�� �� ����, �ʿ信 �°� �����ϼ���.
    private int screenNumber = 0;
    

    void Start()
    {
        lockActivate = true; 
        //lockcamera()??

        startButton.onClick.AddListener(() => StartCoroutine(TutorialBeginning()));

        // ����: ���� ��Ʈ���� �о�� �ؽ�Ʈ
        List<string> textLines = new List<string>
        {
            "Hello",
            "@FirstSpecialCase",
            "World",
            "@SecondSpecialCase",
            "This is Unity",
            "@ThirdSpecialCase",
            "Done"
        };
        
        // ����: StartButton�� ��Ȱ��ȭ
        SetButtonState(Buttons.StartButton, false);

        // ����: StartButton�� Ȱ��ȭ
        SetButtonState(Buttons.StartButton, true);
    }

    void Update()
    {
        // ����: ���̾�α� �������϶� �º� left right ��Ȱ��ȭ,
        // 3<= index <= n�̰� ���̾�α� ���� ���ϰ� ������ Ȱ��ȭ
    }


    private IEnumerator TutorialBeginning()
    {
        cameraMove.SetScreen(1);
        blackScreen.gameObject.SetActive(true);
        streetImg.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);

        yield return StartCoroutine(MoveTextBoxes());

        float elapsedTime = 0f;
        Color startColor = blackScreen.color;
        Color endColor = new Color(0, 0, 0, 0);

        while (elapsedTime < fadeDuration)
        {
            blackScreen.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blackScreen.color = endColor;
        blackScreen.gameObject.SetActive(false);


        yield return StartCoroutine(FadeInCoroutine(HawonImg, fadeDuration));
    }

    private IEnumerator MoveTextBoxes()
    {
        Vector3 nameTextBoxStartPos = new Vector3(0, -300, 0);
        Vector3 nameTextBoxEndPos = nameTextBoxTransform.anchoredPosition;

        Vector3 mainTextBoxStartPos = new Vector3(0, -400, 0);
        Vector3 mainTextBoxEndPos = mainTextBoxTransform.anchoredPosition;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            nameTextBoxTransform.anchoredPosition = Vector3.Lerp(nameTextBoxStartPos, nameTextBoxEndPos, elapsedTime / moveDuration);
            mainTextBoxTransform.anchoredPosition = Vector3.Lerp(mainTextBoxStartPos, mainTextBoxEndPos, elapsedTime / moveDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        nameTextBoxTransform.anchoredPosition = nameTextBoxEndPos;
        mainTextBoxTransform.anchoredPosition = mainTextBoxEndPos;
    }





    public void FadeIn(Image imageObject, float duration)
    {
        StartCoroutine(FadeInCoroutine(imageObject.GetComponent<Image>(), duration));
    }

    private IEnumerator FadeInCoroutine(Image image, float duration)
    {
        Color color = image.color;
        color.a = 0f;
        image.color = color;
        image.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        image.color = color;
    }

    private Dictionary<int, Action> indexActions = new Dictionary<int, Action>
    {
        /*
         * { 0, () => FunctionA() },
        { 1, () => FunctionB() },
        { 2, () => FunctionC() },
        // �ʿ��� ��ŭ �߰�
        */
    };

    private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>()
    {
        { Buttons.StartButton, ButtonState.NotActivated },
        { Buttons.WindowButton, ButtonState.NotActivated },
        { Buttons.CounterButton, ButtonState.NotActivated },
        { Buttons.MergeButton, ButtonState.NotActivated },
        { Buttons.DecomButton, ButtonState.NotActivated },
        { Buttons.DoorButton, ButtonState.NotActivated },
        { Buttons.MapButton, ButtonState.NotActivated },
        { Buttons.DictionaryButton, ButtonState.NotActivated }
    };


//bool allclicked==1 �Ǹ� ȭ�� = 1 ����, ��ȭ ����
//ȭ��=0�̰� bool allclicked==1�̸� ���� ��ư Ŭ������ �ʾƵ� npc ��ȭ ����



    private void FunctionA()
    {
        Debug.Log("Function A executed!");
        // ���⿡ ��� ����
    }

    private void FunctionB()
    {
        Debug.Log("Function B executed!");
        // ���⿡ ��� ����
    }

    private void FunctionC()
    {
        Debug.Log("Function C executed!");
        // ���⿡ ��� ����
    }




    //��ư�� �ܰ�: clicked, notactivated, activated
    //Clicked bool �ο�� �̿��ؼ� allclicked �����
    //Notactivated->activated ���ؼ� index Ȱ��


    private void SetButtonState(Buttons buttonEnum, bool isActive)
    {
        if (buttonStates.TryGetValue(buttonEnum, out ButtonState state))
        {
            if (isActive && state == ButtonState.NotActivated)
            {
                buttonStates[buttonEnum] = ButtonState.Activated;
                Console.WriteLine($"{buttonEnum} is now activated.");
            }
        }

    // ��� ��ư�� Clicked �������� Ȯ���Ͽ� allClicked ����
        allClicked = true;
        foreach (var buttonState in buttonStates.Values)
        {
            if (buttonState != ButtonState.Clicked)
            {
                allClicked = false;
                break;
            }
        }

        if (allClicked)
        {
            Console.WriteLine("allClicked is now true.");
        }
    }
    private void HandleButtonClick(Buttons buttonEnum)
    {
        if (buttonStates.TryGetValue(buttonEnum, out ButtonState state))
        {
            if (state == ButtonState.Activated)
            {
                buttonStates[buttonEnum] = ButtonState.Clicked;


                // ���°� ����Ǿ�����, �ٽ� allClicked Ȯ��
                SetButtonState(buttonEnum, true);
            }
            else if (state == ButtonState.Clicked)
            {
                Console.WriteLine($"{buttonEnum} has already been clicked.");
            }
            else
            {
                Console.WriteLine($"{buttonEnum} is not yet activated.");
            }
        }
    }
    private void SetScreen(int screenNumber)
    {
        for (int i = 0; i < screenNumber; i++)
        {
            cameraMove.Turn(0);
        }
    }


    private IEnumerator TypeTextEffect(string text)
    {
        mainTxt.text = string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        float delay = Mathf.Clamp(0.25f - (SaveManager.Instance.gameSettingData.textSpeed * 0.2f), 0.01f, 0.25f);

        isTypingDone = false;
        starNextClicked = false;
        lockActivate = true;
        cameraMove.LockCamera(lockActivate); 

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '*')
            {
                i++;
                yield return new WaitUntil(() => starNextClicked);

                starNextClicked = false;
                isTypingDone = false;

                if (i < text.Length)
                {
                    stringBuilder.Append(text[i]);
                }
            }
            else if (text[i] == 's' && i + 1 < text.Length && text[i + 1] == 's')
            {
                bool isStart = i + 2 < text.Length && text[i + 2] == 's';
                if (isStart)
                {
                    stringBuilder.Append("<size=200%>");
                    i += 2; 
                }
                else
                {
                    stringBuilder.Append("</size>");
                }
                continue; 
            }
            else if (text[i] == 'b' && i + 1 < text.Length && text[i + 1] == 'b')
            {
                bool isStart = i + 2 < text.Length && text[i + 2] == 'b';
                if (isStart)
                {
                    stringBuilder.Append("<size=200%>");
                    i += 2; 
                }
                else
                {
                    stringBuilder.Append("</size>");
                }
                continue; 
            }
            else if (text[i] == '@')
            {
                currentIndex++;
                Debug.Log($"Index incremented to {currentIndex}");

                if (indexActions.TryGetValue(currentIndex, out Action action))
                {
                    action?.Invoke();
                }
            }
            else
            {
                stringBuilder.Append(text[i]);
            }

            mainTxt.text = stringBuilder.ToString();

            if (isTypingDone)
            {
                mainTxt.text = text;
                break;
            }

            yield return new WaitForSeconds(delay);
        }

        isTypingDone = true;
    }



}













