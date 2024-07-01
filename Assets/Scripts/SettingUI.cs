using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeReference] private Scrollbar soundScrollbar;
    [SerializeReference] private Text soundText;

    [SerializeField] private Scrollbar speedScrollbar;
    [SerializeField] private Text speedText;

    [SerializeField] private Button playButton;
    [SerializeField] private Text playText;
    private const string testText = "가나다라마바사아자차카타파하";


    private void Start()
    {
        soundScrollbar.onValueChanged.AddListener(SetSoundText);
        speedScrollbar.onValueChanged.AddListener(SetSpeedText);
        playButton.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            StartCoroutine(Type(testText));
        });
    }
    
    private void SetSoundText(float f)
    {
        soundText.text = Mathf.FloorToInt(f * 100).ToString();
    }

    private void SetSpeedText(float f) 
    {
        speedText.text = Mathf.FloorToInt(f * 10).ToString();
    }

    private IEnumerator Type(string text)
    {
        float delay = 0.3f - (speedScrollbar.value * 0.2f);
        playText.text = string.Empty;

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            stringBuilder.Append(text[i]);
            playText.text = stringBuilder.ToString();
            yield return new WaitForSeconds(delay);
        }
    }
}
