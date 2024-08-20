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
    private List<ButtonDescTable.Data> curDatas;
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

    public bool startButtonClicked = true;
    private bool isTypingDone = false;
    private bool starNextClicked = false; // 텍스트 쪼갬 단위
    public bool lockActivate = false;
    public bool allClicked { get; private set; } // 모든 버튼이 클릭되었는지 여부

    public float fadeDuration = 1.0f; // 페이드 아웃 시간
    public float moveDuration = 60.0f; // 이동 시간
    public int currentIndex = 0; // @(골뱅이) 카운트 인덱스
    private int index = 0;
    private int totalItems = 7; // 기물의 총 개수, 필요에 맞게 수정하세요.
    private int screenNumber = 0;
    private int curIndex;
    private int maxIndex;
    public bool isDialogActive;
    public int lockCameraInt;

    public Vector3 nameTextEndPos = new Vector3(0, 400, 0); // 이동할 최종 위치
    public Vector3 mainTextEndPos = new Vector3(0, 300, 0); // 이동할 최종 위치



    void Awake()
    {
        StartCoroutine(TutorialBeginning());
    }

    void Start()
    {
        lockActivate = true;

        allClicked = false;


    }

    void Update()
    {

        if (3 < index && index <= lockCameraInt)
        {
            lockActivate = false;
        }

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

        nameTextBoxTransform.anchoredPosition = new Vector3(0, -300, 0); // 화면 아래에서 시작
        mainTextBoxTransform.anchoredPosition = new Vector3(0, -300, 0); // 화면 아래에서 시작

        MoveTextBoxes();
    }




    public void MoveTextBoxes()
    {
        // 텍스트를 현재 위치에서 endPos로 moveDuration 동안 이동시킵니다.
        nameTextBoxTransform.DOAnchorPos(nameTextEndPos, moveDuration).SetEase(Ease.OutCubic);
        mainTextBoxTransform.DOAnchorPos(mainTextEndPos, moveDuration).SetEase(Ease.OutCubic);
        Debug.Log("텍스트박스 움직임");

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
        Debug.Log("하원 이미지 보이기");
        StartCoroutine(TypeTextEffect(curDatas[curIndex].strValue));
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
        // 해당 버튼에 대한 데이터를 찾음
        ButtonData buttonData = buttonDataList.Find(b => b.Button == button);

        if (buttonData != null && !buttonData.IsClicked)
        {
            buttonData.IsClicked = true;
            return;
        }
    }



    private void SetButtonState(Buttons buttonEnum, bool isActive)
    {
    }
    private void ButtonStateManager(Buttons buttonEnum)
    {
        if (buttonStates.TryGetValue(buttonEnum, out ButtonState state))
        {
            if (state == ButtonState.Activated)
            {
                buttonStates[buttonEnum] = ButtonState.Clicked;


                // 상태가 변경되었으니, 다시 allClicked 확인
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

    public void StartDialog(string text, int dialogIndex)
    {
        index = dialogIndex;
        isDialogActive = true; // 다이얼로그 진행 중으로 설정
        StartCoroutine(TypeTextEffect(text));
        isDialogActive = false; // 다이얼로그가 끝나면 진행 중이 아니라고 설정
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

