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
    private bool starNextClicked = false; // �ؽ�Ʈ �ɰ� ����
=======
    [SerializeField] private TextMeshProUGUI nameTextBox; // �ؽ�Ʈ ����� ��Ÿ�� ������
    [SerializeField] private RectTransform nameTextBoxTransform; // UI ��ġ�� ũ�� ������
    [SerializeField] private TextMeshProUGUI mainTextBox; // �ؽ�Ʈ ����� ��Ÿ�� ������
    [SerializeField] public RectTransform mainTextBoxTransform; 

    private TabletUI tabletUI;
    private NorthUI north;
    private InventoryUI inventoryUI;
    private CameraMove cameraMove;


    private bool isTypingDone = false;
    private bool starnextClicked = false; // �ؽ�Ʈ �ɰ� ����
>>>>>>> parent of cd0c5e3 (Merge branch 'main' of https://github.com/paul050210/UGS)
    public bool lockActivate = false;
    public bool allClicked { get; private set; } // ��� ��ư�� Ŭ���Ǿ����� ����

    public float fadeDuration = 1.0f; // ���̵� �ƿ� �ð�
    public float moveDuration = 60.0f; // �̵� �ð�
    public int currentIndex = 0; // @(�����) ī��Ʈ �ε���
<<<<<<< HEAD
    private int index = 0;
    private int totalItems = 7; // �⹰�� �� ����, �ʿ信 �°� �����ϼ���.
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
    private int moveCamera = 1; // ī�޶� �̵� �ε���




    void Start()
    {
        lockActivate = true; 

        startButton.onClick.AddListener(() => StartCoroutine(StartTutorialSequence()));

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
            // ������ ���
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            // Lerp�� ����Ͽ� �߰� ��ġ�� �̵�
            nameTextBoxTransform.anchoredPosition = Vector3.Lerp(nameTextBoxStartPos, nameTextBoxEndPos, t);
            mainTextBoxTransform.anchoredPosition = Vector3.Lerp(mainTextBoxStartPos, mainTextBoxEndPos, t);

            // �ð� ����
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ ��ġ�� ��Ȯ�� ����
        nameTextBoxTransform.anchoredPosition = nameTextBoxEndPos;
        mainTextBoxTransform.anchoredPosition = mainTextBoxEndPos;

<<<<<<< HEAD
        Debug.Log("�ؽ�Ʈ �ڽ� ������ �Ϸ�");
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
        // �ʿ��� ��ŭ �߰�
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
        // allClicked�� true�� �� �Լ� ȣ��

    }

    void FunctionB()
    {
        Debug.Log("Function B executed!");
        //bool allClicked==1 �Ǹ� ȭ�� = 1 ����, ��ȭ ����
    }

    void FunctionC()
    {
        Debug.Log("Function C executed!");
        //ȭ��=0�̰� bool allClicked==1�̸� ���� ��ư Ŭ������ �ʾƵ� npc ��ȭ ����
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
        // �ش� ��ư�� ���� �����͸� ã��
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
        isDialogActive = true; // ���̾�α� ���� ������ ����
        StartCoroutine(TypeTextEffect(text));
        isDialogActive = false; // ���̾�αװ� ������ ���� ���� �ƴ϶�� ����
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
