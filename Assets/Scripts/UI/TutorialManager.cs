using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


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
    [SerializeField] private Text descriptTxt;

    [SerializeField] public Image blackScreen; 
    [SerializeField] public Image streetImg;
    [SerializeField] public Image friendACharImg;

    [SerializeField] private TextMeshProUGUI nameTextBox; // 텍스트 내용과 스타일 조정용
    [SerializeField] private RectTransform nameTextBoxTransform; // UI 위치와 크기 조정용
    [SerializeField] private TextMeshProUGUI mainTextBox; // 텍스트 내용과 스타일 조정용
    [SerializeField] public RectTransform mainTextBoxTransform; 

    private TabletUI tabletUI;
    private NorthUI north;
    private InventoryUI inventoryUI;
    private CameraMove cameraMove;


    private bool isTypingDone = false;
    private bool starnextClicked = false; // 텍스트 쪼갬 단위
    public bool lockActivate = false;

    public float fadeDuration = 1.0f; // 페이드 아웃 시간
    public float moveDuration = 0.3f; // 이동 시간
    public int currentIndex = 0; // @(골뱅이) 카운트 인덱스
    private int moveCamera = 1; // 카메라 이동 인덱스




    void Start()
    {
        lockActivate = true; 

        startButton.onClick.AddListener(() => StartCoroutine(StartTutorialSequence()));

        // 예시: 구글 시트에서 읽어온 텍스트
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

        ProcessTextLines(textLines);

        
        // 예시: StartButton을 비활성화
        SetButtonState(Buttons.StartButton, false);

        // 예시: StartButton을 활성화
        SetButtonState(Buttons.StartButton, true);
    }

    void Update()
    {

    }

    private IEnumerator StartTutorialSequence()
    {
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


        yield return StartCoroutine(FadeInCoroutine(friendACharImg, fadeDuration));
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



    public void MoveCameraToCanvas(int moveCamera)
    {
        if (cameraMove != null)
        {
            cameraMove.Turn(moveCamera);
        }
    }

    private Dictionary<int, Action> indexActions = new Dictionary<int, Action>
    {
        /*
         * { 0, () => FunctionA() },
        { 1, () => FunctionB() },
        { 2, () => FunctionC() },
        // 필요한 만큼 추가
        */
    };


    void ProcessTextLines(List<string> lines)
    {
        foreach (string line in lines)
        {
            if (line.Contains("@"))
            {
                currentIndex++;
                Debug.Log($"Index incremented to {currentIndex}");

                if (indexActions.TryGetValue(currentIndex, out Action action))
                {
                    action?.Invoke();
                }
            }
        }
    }

    void FunctionA()
    {
        Debug.Log("Function A executed!");
        // 여기에 기능 구현
    }

    void FunctionB()
    {
        Debug.Log("Function B executed!");
        // 여기에 기능 구현
    }

    void FunctionC()
    {
        Debug.Log("Function C executed!");
        // 여기에 기능 구현
    }



    public void SetButtonState(Buttons buttonEnum, bool isActive)
    {
        if (buttonDictionary.TryGetValue(buttonEnum, out Button button))
        {
            button.interactable = isActive;
        }
        else
        {
            Debug.LogError($"Button with enum {buttonEnum} not found.");
        }
    }


    private IEnumerator TypeTextEffect(string text)
    {
        descriptTxt.text = string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        float delay = Mathf.Clamp(0.25f - (SaveManager.Instance.gameSettingData.textSpeed * 0.2f), 0.01f, 0.25f);

        isTypingDone = false;
        starnextClicked = false;
        lockActivate = true;
        cameraMove.LockCamera(lockActivate); 

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '*')
            {
                i++;
                yield return new WaitUntil(() => starnextClicked);

                starnextClicked = false;
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
            else
            {
                stringBuilder.Append(text[i]);
            }

            descriptTxt.text = stringBuilder.ToString();

            if (isTypingDone)
            {
                descriptTxt.text = text;
                break;
            }

            yield return new WaitForSeconds(delay);
        }

        isTypingDone = true;
    }



}
