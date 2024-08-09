using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class QuestMainUI : MonoBehaviour
{
    [SerializeField] private Image charImg;
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text descriptTxt;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Button doneButton;

    private TabletUI tabletUI;
    private NorthUI north;
    private InventoryUI inventoryUI;

    private Quest goingQuest;
    private List<DefaultTable.Data> curDatas;
    private bool isTypingDone = false;
    private bool isChooesd = false;
    private bool starnextClicked = false;


    private int curIndex;
    private int maxIndex;

    private void Start()
    {
        nextButton.onClick.AddListener(OnClickNext);
        tabletUI = FindObjectOfType<TabletUI>();
        north = FindObjectOfType<NorthUI>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        north.OnDisable.AddListener(() =>
        {
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            doneButton.onClick.RemoveListener(QuestDone);
            if (!isChooesd && goingQuest.questState == QuestState.Default)
                QuestManager.Instance.RemoveEnableQuest(goingQuest);
        });
    }

    private void OnEnable()
    {
        if (goingQuest == null)
        {
            goingQuest = QuestManager.Instance.GetQuest();
            if (goingQuest == null)
            {
                //TODO: 디버그에서 퀘스트 없을때 해야되는걸로 변경
                Debug.LogWarning("퀘스트 더이상 없음");
                charImg.sprite = null;
                nameTxt.text = " ";
                descriptTxt.text = "진행가능 퀘스트 없음(임시)";
                return;
            }
            if (goingQuest.questState == QuestState.Accept)
            {
                Debug.Log(goingQuest.questState.ToString());
                curDatas = goingQuest.GetText(3);
            }
            else
            {
                curDatas = goingQuest.GetText(0);
            }
            curIndex = 0;
            maxIndex = curDatas.Count - 1;
            charImg.sprite = goingQuest.CharSprite;
            isChooesd = false;
        }
        if (curIndex <= maxIndex)
            isTypingDone = false;
        SetText();
    }

    private void OnClickNext()
    {
        starnextClicked = true;

        if (isTypingDone)
        {
            curIndex++;
            if (curIndex > maxIndex)
            {
                if (!isChooesd)
                {
                    if (goingQuest.questState == QuestState.Accept)
                    {
                        ItemManager.Instance.canSelect = true;
                        tabletUI.TurnOnTablet(State.Inventory);
                        doneButton.onClick.AddListener(QuestDone);
                    }
                    else
                    {
                        tabletUI.TurnOnTablet(State.Quest);
                        AddContents();
                    }
                    return;
                }
                else
                {
                    if (goingQuest?.questState != QuestState.Done)
                        QuestManager.Instance.AddQuestIndex();
                    goingQuest = null;
                    north.SetActiveCloseBtn(true);
                    north.CloseWindow();
                    return;
                }
            }
            SetText();
        }
        else
        {
            isTypingDone = true;
        }
    }

    private void SetText()
    {
        if (curIndex > maxIndex)
        {
            Debug.LogWarning("인덱스 오류");
            return;
        }
        isTypingDone = false;
        StartCoroutine(TypeTextEffect(curDatas[curIndex].strValue));
        nameTxt.text = curDatas[curIndex].name;
    }

    private IEnumerator TypeTextEffect(string text)
    {
        descriptTxt.text = string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        float delay = Mathf.Clamp(0.25f - (SaveManager.Instance.gameSettingData.textSpeed * 0.2f), 0.01f, 0.25f);

        isTypingDone = false;
        starnextClicked = false;

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


    private void AddContents()
    {
        goingQuest.isAceepted = true;
        goingQuest.questState = QuestState.Accept;
        QuestManager.Instance.AddEnableQuest(goingQuest);
        QuestContentControl.Instance.SetContents(curDatas);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() =>
        {
            curDatas = goingQuest.GetText(1);
            QuestManager.Instance.AddAcceptQuest(goingQuest);
            OnClickChoose();
        });
        noButton.onClick.AddListener(() =>
        {
            curDatas = goingQuest.GetText(2);
            QuestManager.Instance.RemoveEnableQuest(goingQuest);
            goingQuest.isAceepted = false;
            goingQuest.questState = QuestState.Refuse;
            QuestManager.Instance.AddEnableQuest(goingQuest);
            OnClickChoose();
        });
    }

    private void OnClickChoose()
    {
        curIndex = 0;
        maxIndex = curDatas.Count - 1;
        isChooesd = true;
        tabletUI.TurnOnTablet(State.Quest);
        north.SetActiveCloseBtn(false);
        SetText();
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }

    private void QuestDone()
    {
        if (inventoryUI == null)
        {
            inventoryUI = FindObjectOfType<InventoryUI>();
        }
        Item[] items = inventoryUI.GetToQuest();
        if (items == null) return;

        ItemManager.Instance.canSelect = false;
        goingQuest.questState = QuestState.Done;
        List<Item> reward = goingQuest.DoneQuest(items);
        if (reward != null)
        {
            curDatas = goingQuest.GetText(4);
            for (int i = 0; i < reward.Count; i++)
            {
                int n = ItemManager.Instance.GetItem(reward[i]);
                ItemManager.Instance.AddItem(reward[i], n + 1);
            }
            for (int i = 0; i < items.Length; i++)
            {
                int n = ItemManager.Instance.GetItem(items[i]);
                ItemManager.Instance.AddItem(items[i], n - 1);
            }
            //성공
        }
        else if (items[0].Equals((Item)goingQuest.HalfItem))
        {
            curDatas = goingQuest.GetText(5);
            for (int i = 0; i < items.Length; i++)
            {
                int n = ItemManager.Instance.GetItem(items[i]);
                ItemManager.Instance.AddItem(items[i], n - 1);
            }
            //절반 성공
        }
        else
        {
            curDatas = goingQuest.GetText(6);
            //실패
        }

        doneButton.onClick.RemoveListener(QuestDone);
        curIndex = 0;
        maxIndex = curDatas.Count - 1;
        isChooesd = true;
        tabletUI.TurnOnTablet(State.Inventory);
        north.SetActiveCloseBtn(false);
        SetText();
    }

    public void ResetQuestUI()
    {
        goingQuest = null;
        curDatas = null;
        curIndex = 0;
        maxIndex = 0;
        isChooesd = true;
    }
}
