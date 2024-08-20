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
    [SerializeField] private Text descriptTxt;

    [SerializeField] public Image blackScreen;
    [SerializeField] public Image streetImg;
    [SerializeField] public Image friendACharImg;

<<<<<<< HEAD

    [SerializeField] private RectTransform nameTextBoxTransform;
    [SerializeField] public RectTransform mainTextBoxTransform;

    [SerializeField] private TabletUI tabletUI;
    [SerializeField] private NorthUI north;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private CameraMove cameraMove;

    public bool startButtonClicked = true;
    private bool isTypingDone = false;
    private bool starNextClicked = false; // 텍스트 쪼갬 단위
=======
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
>>>>>>> parent of cd0c5e3 (Merge branch 'main' of https://github.com/paul050210/UGS)
    public bool lockActivate = false;
    public bool allClicked { get; private set; } // 모든 버튼이 클릭되었는지 여부

    public float fadeDuration = 1.0f; // 페이드 아웃 시간
    public float moveDuration = 60.0f; // 이동 시간
    public int currentIndex = 0; // @(골뱅이) 카운트 인덱스
<<<<<<< HEAD
    private int index = 0;
    private int totalItems = 7; // 기물의 총 개수, 필요에 맞게 수정하세요.
    private int screenNumber = 0;
    private int curIndex;
    private int maxIndex;
    public bool isDialogActive;
    public int lockCameraInt;




    void Awake()
    {
        StartCoroutine(TutorialBeginning());
    }

    void Start()
    {
        lockActivate = true;

        allClicked = false;
=======
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

>>>>>>> parent of cd0c5e3 (Merge branch 'main' of https://github.com/paul050210/UGS)
        

    }

    void Update()
    {

<<<<<<< HEAD
        if (3 < index && index <= lockCameraInt)
        {
            lockActivate = false;
        }

=======
>>>>>>> parent of cd0c5e3 (Merge branch 'main' of https://github.com/paul050210/UGS)
    }

    private IEnumerator StartTutorialSequence()
    {
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

        yield return StartCoroutine(MoveTextBoxes());

        blackScreen.color = endColor;
        blackScreen.gameObject.SetActive(false);

<<<<<<< HEAD
        yield return StartCoroutine(FadeInCoroutine(HawonImg, fadeDuration));
=======

        yield return StartCoroutine(FadeInCoroutine(friendACharImg, fadeDuration));
>>>>>>> parent of cd0c5e3 (Merge branch 'main' of https://github.com/paul050210/UGS)
    }

    private IEnumerator MoveTextBoxes()
    {
        nameTxt.gameObject.SetActive(true);
        mainTxt.gameObject.SetActive(true);

        Vector3 nameTextBoxStartPos = new Vector3(0, -300, 0);
        Vector3 nameTextBoxEndPos = nameTextBoxTransform.anchoredPosition;

        Vector3 mainTextBoxStartPos = new Vector3(0, -400, 0);
        Vector3 mainTextBoxEndPos = mainTextBoxTransform.anchoredPosition;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            // 비율을 계산
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            // Lerp를 사용하여 중간 위치로 이동
            nameTextBoxTransform.anchoredPosition = Vector3.Lerp(nameTextBoxStartPos, nameTextBoxEndPos, t);
            mainTextBoxTransform.anchoredPosition = Vector3.Lerp(mainTextBoxStartPos, mainTextBoxEndPos, t);

            // 시간 갱신
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 마지막 위치를 정확히 설정
        nameTextBoxTransform.anchoredPosition = nameTextBoxEndPos;
        mainTextBoxTransform.anchoredPosition = mainTextBoxEndPos;

<<<<<<< HEAD
        Debug.Log("텍스트 박스 움직임 완료");
=======
    public void FadeIn(Image imageObject, float duration)
    {
        StartCoroutine(FadeInCoroutine(imageObject.GetComponent<Image>(), duration));
>>>>>>> parent of cd0c5e3 (Merge branch 'main' of https://github.com/paul050210/UGS)
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
        { 0, () => FunctionA() },
        { 1, () => FunctionB() },
        { 2, () => FunctionC() },
        // 필요한 만큼 추가
        */
    };


    void ProcessTextLines(List<string> lines)
    {
<<<<<<< HEAD
        { Buttons.WindowButton, ButtonState.NotActivated },
        { Buttons.CounterButton, ButtonState.NotActivated },
        { Buttons.MergeButton, ButtonState.NotActivated },
        { Buttons.DecomButton, ButtonState.NotActivated },
        { Buttons.DoorButton, ButtonState.NotActivated },
        { Buttons.MapButton, ButtonState.NotActivated },
        { Buttons.DictionaryButton, ButtonState.NotActivated }
    };
=======
        foreach (string line in lines)
        {
            if (line.Contains("@"))
            {
                currentIndex++;
                Debug.Log($"Index incremented to {currentIndex}");
>>>>>>> parent of cd0c5e3 (Merge branch 'main' of https://github.com/paul050210/UGS)

                if (indexActions.TryGetValue(currentIndex, out Action action))
                {
                    action?.Invoke();
                }
            }
        }
    }

<<<<<<< HEAD
    private void FunctionA()
=======
    void FunctionA()
>>>>>>> parent of cd0c5e3 (Merge branch 'main' of https://github.com/paul050210/UGS)
    {
        Debug.Log("Function A executed!");
        // allClicked가 true일 때 함수 호출

    }

    void FunctionB()
    {
        Debug.Log("Function B executed!");
        //bool allClicked==1 되면 화면 = 1 설정, 대화 진행
    }

    void FunctionC()
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

<<<<<<< HEAD
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
=======
    public void SetButtonState(Buttons buttonEnum, bool isActive)
    {
        if (buttonDictionary.TryGetValue(buttonEnum, out Button button))
        {
            button.interactable = isActive;
        }
        else
        {
            Debug.LogError($"Button with enum {buttonEnum} not found.");
>>>>>>> parent of cd0c5e3 (Merge branch 'main' of https://github.com/paul050210/UGS)
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
<<<<<<< HEAD

=======
>>>>>>> parent of cd0c5e3 (Merge branch 'main' of https://github.com/paul050210/UGS)
