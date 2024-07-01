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
    private int index = 3;



    private void Awake()
    {
        UnityGoogleSheet.LoadAllData();
    }

    private void Start()
    {
        var localeMap = DefaultTable.Data.GetDictionary();
        try
        {
            StartCoroutine(TypeTextEffect(localeMap[index].strValue));
            //textList[0].text = localeMap[index].strValue;
            Debug.Log(localeMap[index].strValue);
        }
        catch ( KeyNotFoundException )
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
            stringBuilder.Append(text[i]);
            textList[0].text = stringBuilder.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}