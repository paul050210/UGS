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
    WindowButton,
    CounterButton,
    MergeButton,
    DecomButton,
    DoorButton,
    MapButton,
    DictionaryButton
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
    private List<TutorialTable.Data> curDatas;
    private List<ButtonData> buttonDataList;



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

    [SerializeField] private TabletUI tabletUI;
    [SerializeField] private NorthUI north;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private TutorialTextManager tutorialTextManager;

    public bool startButtonClicked = true;
    private bool isTypingDone = false;
    private bool starNextClicked = false; // 텍스트 쪼갬 단위
    public bool lockActivate = false;
    public bool allClicked { get; private set; } // 모든 버튼이 클릭되었는지 여부

    public float fadeDuration = 1.0f; // 페이드 아웃 시간
    public float moveDuration = 60.0f; // 이동 시간
    private int screenNumber = 0;
    private int curIndex;
    private int maxIndex;
    private int dialogStartIndex;
    private int dialogEndIndex;
    public bool isDialogActive;
    public int lockCameraInt;

    private Button[] buttonsArray;

    private Vector3 nameTextEndPos = new Vector3(-325, -250, 0); 
    private Vector3 mainTextEndPos = new Vector3(0, -500, 0); 



    void Awake()
    {
        StartCoroutine(TutorialBeginning());
    }

    void Start()
    {
        lockActivate = true;
        allClicked = false;

        buttonsArray = new Button[]
        {
        WindowButton,    // Buttons.WindowButton
        CounterButton,   // Buttons.CounterButton
        MergeButton,     // Buttons.MergeButton
        DecomButton,     // Buttons.DecomButton
        DoorButton,      // Buttons.DoorButton
        MapButton,       // Buttons.MapButton
        DictionaryButton // Buttons.DictionaryButton
        };

    }

    void Update()
    {

    }



    private IEnumerator TutorialBeginning()
    {
        cameraMove.SetScreen(1);
        blackScreen.gameObject.SetActive(true);
        streetImg.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);

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

        MoveTextBoxes();
    }

              
    public void MoveTextBoxes()
    {
        nameTextBoxTransform.DOAnchorPos(nameTextEndPos, moveDuration).SetEase(Ease.OutCubic);
        mainTextBoxTransform.DOAnchorPos(mainTextEndPos, moveDuration).SetEase(Ease.OutCubic);
        StartDialog(1,15);
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
        StartDialog();
    }

    private Dictionary<int, Action> indexActions = new Dictionary<int, Action>
    {
        /*
        { 0, () => FunctionA() },
        { 1, () => FunctionB() },
        { 2, () => FunctionC() },
        // 필요한 만큼 추가
        */
    };

    

    private void FunctionA()
    {
        Debug.Log("Function A executed!");
        // allClicked가 true일 때 함수 호출

    }

    private void FunctionB()
    {
        Debug.Log("Function B executed!");
        //bool allClicked==1 되면 화면 = 1 설정, 대화 진행
    }

    private void FunctionC()
    {
        Debug.Log("Function C executed!");
        //화면=0이고 bool allClicked==1이면 계산대 버튼 클릭하지 않아도 npc 대화 진행
    }


    private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>()
    {
        { Buttons.WindowButton, ButtonState.NotActivated },
        { Buttons.CounterButton, ButtonState.NotActivated },
        { Buttons.MergeButton, ButtonState.NotActivated },
        { Buttons.DecomButton, ButtonState.NotActivated },
        { Buttons.DoorButton, ButtonState.NotActivated },
        { Buttons.MapButton, ButtonState.NotActivated },
        { Buttons.DictionaryButton, ButtonState.NotActivated }
    };

    public class ButtonData
    {
        public Buttons Button { get; private set; }
        public bool IsClicked { get; set; }

        public ButtonData(Buttons button)
        {
            Button = button;
            IsClicked = false;
        }
    }

    public void OnButtonClick(Buttons button)
    {
        ButtonData buttonData = buttonDataList.Find(b => b.Button == button);

        if (buttonData != null && !buttonData.IsClicked)
        {
            buttonData.IsClicked = true;
            int buttonNumber = (int)button;
            GetButtonText(buttonNumber);
            return;
        }
    }

    private void SetButtonState(Buttons buttonEnum, bool isActive)
    {
        // 배열의 인덱스에 해당하는 버튼의 상태를 설정합니다.
        buttonsArray[(int)buttonEnum].Enabled = isActive;
    }
    private void ButtonStateManager(Buttons buttonEnum)
    {
        if (buttonStates.TryGetValue(buttonEnum, out ButtonState state))
        {
            if (state == ButtonState.Activated)
            {
                buttonStates[buttonEnum] = ButtonState.Clicked;
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

    private void StartDialog(int dialogStartIndex, int dialogEndIndex)
    {
        var data = tutorialTextManager.GetTextData(1, 3);

        curIndex = dialogEndIndex;
        isDialogActive = true; 
        foreach (var item in data)
        {
            yield return StartCoroutine(TypeTextEffect(item.Text));
        }
        isDialogActive = false; 
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


    private List<TutorialTable.Data> GetButtonText(int buttonNumber)
    {
        int start=0;
        int end=0;
        switch (buttonNumber)
        {
            case 0:
                start = buttonDescStartIndex+13;
                end = start+5;
                break;
            case 1:
                break;
            case 2:
                start = buttonDescStartIndex;
                end = start + 4;
                break;
            case 3:
                start = buttonDescStartIndex + 6;
                end = start + 5;
                break;
            case 4:
                start = buttonDescStartIndex + 30;
                end = start + 6;
                break;
            case 5:
                start = buttonDescStartIndex + 20;
                end = start + 4;
                break;
            case 6:
                start = buttonDescStartIndex + 26;
                end = start + 3;
                break;
            default:
                start = buttonDescStartIndex;
                end = buttonDescEndIndex;
                break;
        }
        var datas = TutorialTable.Data.GetList();
        List<TutorialTable.Data> returnList = new List<TutorialTable.Data>();
        for (int i = start; i <= end; i++)
        {
            returnList.Add(datas[i]);
        }
        return returnList;
    }

}

