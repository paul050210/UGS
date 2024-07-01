using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;
using System.Text;

public class UnitySheetTest : MonoBehaviour
{
    [SerializeField] private List<Text> textList;
    private int index = 0;
    private bool isTypingDone = false;
    Dictionary<int, DefaultTable.Data> localeMap = new Dictionary<int, DefaultTable.Data>();


    private void Awake()
    {
        UnityGoogleSheet.LoadAllData();
    }

    private void Start()
    {
        localeMap = DefaultTable.Data.GetDictionary();
        SetText();   
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isTypingDone)
            {
                index++;
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
            StartCoroutine(TypeTextEffect(localeMap[index].strValue));
        }
        catch (KeyNotFoundException)
        {
            Debug.LogError("인덱스값 오류");
        }
    }

    IEnumerator TypeTextEffect(string text)
    {
        textList[0].text = string.Empty;

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            if(isTypingDone)
            {
                textList[0].text = text;
                break;       
            }
            stringBuilder.Append(text[i]);
            textList[0].text = stringBuilder.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        isTypingDone = true;
    }
}