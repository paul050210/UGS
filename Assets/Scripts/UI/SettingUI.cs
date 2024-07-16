using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Scrollbar soundScrollbar;
    [SerializeField] private Text soundText;

    [SerializeField] private Scrollbar speedScrollbar;
    [SerializeField] private Text speedText;

    [SerializeField] private Button playButton;
    [SerializeField] private Text playText;
    private const string testText = "가나다라마바사아자차카타파하";

    [SerializeField] private Dropdown screenDropdown;
    private int[] widthArray = new int[]{ 1920, 1366 };
    private int[] heightArray = new int[]{ 1080, 768 };

    private void Start()
    {
        soundScrollbar.onValueChanged.AddListener(SetSoundText);
        speedScrollbar.onValueChanged.AddListener(SetSpeedText);
        playButton.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            StartCoroutine(Typing(testText));
        });
        screenDropdown.onValueChanged.AddListener(SetDropDown);
        Load();
        SetSoundText(soundScrollbar.value);
        SetSpeedText(speedScrollbar.value);
        SetDropDown(screenDropdown.value);
    }

    private void OnEnable()
    {
        Load();
    }

    private void Load()
    {
        SaveManager.Instance.LoadGameSettingData();
        var gD = SaveManager.Instance.gameSettingData;
        soundScrollbar.value = gD.volume;
        speedScrollbar.value = gD.textSpeed;
        screenDropdown.value = gD.screenSize;
    }
    
    private void SetSoundText(float f)
    {
        int volume = Mathf.FloorToInt(f * 100);
        soundText.text = volume.ToString();
        SaveManager.Instance.gameSettingData.volume = f;
        SaveManager.Instance.SaveGameSettingData();
    }

    private void SetSpeedText(float f) 
    {
        int speed = Mathf.FloorToInt(f * 10);
        speedText.text = speed.ToString();
        SaveManager.Instance.gameSettingData.textSpeed = f;
        SaveManager.Instance.SaveGameSettingData();
    }

    private void SetDropDown(int i)
    {
        Screen.SetResolution(widthArray[i], heightArray[i], true);
        SaveManager.Instance.gameSettingData.screenSize = i;
        SaveManager.Instance.SaveGameSettingData();
    }

    private IEnumerator Typing(string text)
    {
        float delay = 0.25f - (speedScrollbar.value * 0.2f);
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
