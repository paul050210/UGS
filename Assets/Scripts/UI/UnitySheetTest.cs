using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

public class UnitySheetTest : MonoBehaviour
{
    [SerializeField] Text textStr;
    [SerializeField] Text textName;
    private int index = 0;
    private bool isTypingDone = false;
    Dictionary<int, DefaultTable.Data> localeMap = new Dictionary<int, DefaultTable.Data>();
    List<DefaultTable.Data> datas = new List<DefaultTable.Data>();


    private void Awake()
    {
        UnityGoogleSheet.LoadAllData();
    }

    private void Start()
    {
        localeMap = DefaultTable.Data.GetDictionary();
        datas = DefaultTable.Data.GetList();
        SetText();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isTypingDone)
            {
                index++;
                if (index >= 4) index = 0;
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
            textName.text = localeMap[index].name;
        }
        catch (KeyNotFoundException)
        {
            Debug.LogError("�ε����� ����");
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