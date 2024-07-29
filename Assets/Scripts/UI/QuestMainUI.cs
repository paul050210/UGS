using System.Collections;
using System.Collections.Generic;
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

    private TabletUI tabletUI;
    private NorthUI north;

    private Quest goingQuest;
    private List<DefaultTable.Data> curDatas;
    private bool isTypingDone = false;
    private bool isChooesd = false;

    private int curIndex;
    private int maxIndex;

    private void Start()
    {
        nextButton.onClick.AddListener(OnClickNext);
        tabletUI = FindObjectOfType<TabletUI>();
        north = FindObjectOfType<NorthUI>();
        north.OnDisable.AddListener(() => 
        {
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            if(!isChooesd)
                QuestManager.Instance.RemoveEnableQuest(goingQuest);
        });
    }

    private void OnEnable()
    {
        if(goingQuest == null)
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
            curDatas = goingQuest.GetText(0);
            curIndex = 0;
            maxIndex = curDatas.Count - 1;
            charImg.sprite = goingQuest.CharSprite;
            isChooesd = false;
        }
        if(curIndex <= maxIndex)
            isTypingDone = false;
        SetText();
    }

    private void OnClickNext()
    {
        if(isTypingDone)
        {
            curIndex++;
            if(curIndex > maxIndex)
            {
                if(!isChooesd)
                {
                    tabletUI.TurnOnTablet(State.Quest);
                    AddContents();
                    return;
                }
                else
                {
                    goingQuest = null;
                    north.SetActiveCloseBtn(true);
                    north.CloseWindow();
                    QuestManager.Instance.AddQuestIndex();
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
        if(curIndex > maxIndex)
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
        float delay = 0.25f - (SaveManager.Instance.gameSettingData.textSpeed * 0.2f);

        for(int i = 0; i< text.Length; i++)
        {
            if(isTypingDone)
            {
                descriptTxt.text = text;
                break;
            }
            stringBuilder.Append(text[i]);
            descriptTxt.text = stringBuilder.ToString();
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

    public void ResetQuestUI()
    {
        goingQuest = null;
        curDatas = null;
        curIndex = 0;
        maxIndex = 0;
        isChooesd = true;
    }
}
