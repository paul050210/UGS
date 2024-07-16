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
    [SerializeField] private GameObject npcScrollView;
    [SerializeField] private GameObject questScrollView;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private TabletUI tabletUI;
    private NorthUI north;

    private List<DefaultTable.Data> curDatas;
    private bool isTypingDone = false;
    private bool isLast = false;

    private int curIndex;
    private int maxIndex;

    private void Start()
    {
        nextButton.onClick.AddListener(OnClickNext);
        tabletUI = FindObjectOfType<TabletUI>();
        north = FindObjectOfType<NorthUI>();
    }

    private void OnEnable()
    {
        curDatas = QuestManager.Instance.GetQuestText();
        curIndex = 0;
        maxIndex = curDatas.Count - 1;
        charImg.sprite = QuestManager.Instance.GetQuestImg();
        isTypingDone = false;
        isLast = false;

        SetText();
    }

    private void OnClickNext()
    {
        if(isTypingDone)
        {
            curIndex++;
            if(curIndex > maxIndex)
            {
                if(!isLast)
                {
                    isLast = true;
                    tabletUI.TurnOnTablet(State.Quest);
                    AddContents();
                    return;
                }
                else
                {
                    north.ZoomOut();
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
        Transform contents = questScrollView.transform;
        contents = contents.GetChild(0).GetChild(0);
        contents.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (curDatas.Count+1) * 100f);
        for(int i = 0; i<curDatas.Count; i++)
        {
            string txt = curDatas[i].strValue;
            bool isLeft = curDatas[i].name != "A";
            contents.GetChild(i).GetComponent<QuestContentUI>().SetText(txt, isLeft);
        }

        yesButton.onClick.AddListener(() => 
        {
            curDatas = QuestManager.Instance.GetAcceptText();
            curIndex = 0;
            maxIndex = curDatas.Count - 1;
            tabletUI.TurnOnTablet(State.Quest);
            SetText();
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
        });
        noButton.onClick.AddListener(() => 
        {
            curDatas = QuestManager.Instance.GetRefuseText();
            curIndex = 0;
            maxIndex = curDatas.Count - 1;
            tabletUI.TurnOnTablet(State.Quest);
            SetText();
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
        });
    }
}
