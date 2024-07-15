using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UnitySheetTest : MonoBehaviour
{
    [SerializeField] Text textStr;
    [SerializeField] Text textName;
    private int index = 0;
    private bool isTypingDone = false;
    List<DefaultTable.Data> datas = new List<DefaultTable.Data>();


    private Quest testQ;


    private void Awake()
    {
        UnityGoogleSheet.LoadAllData();
    }

    private void Start()
    {
        testQ = new Quest(0, 9);
        datas = QuestManager.Instance.GetQuestText();
        SetText();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isTypingDone)
            {
                index++;
                if (index >= testQ.scriptLength) index = 0;
                SetText();
            }
            else
            {
                isTypingDone = true;
            }
        }
        
    }

    private void SetText()
    {
        try
        {
            isTypingDone = false;
            StartCoroutine(TypeTextEffect(datas[index].strValue));
            textName.text = datas[index].name;
        }
        catch (KeyNotFoundException)
        {
            Debug.LogError("인덱스값 오류");
        }
    }

    private IEnumerator TypeTextEffect(string text)
    {
        textStr.text = string.Empty;

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            if(isTypingDone)
            {
                textStr.text = text;
                break;       
            }
            stringBuilder.Append(text[i]);
            textStr.text = stringBuilder.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        isTypingDone = true;
    }
}