using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button endButton;

    public bool startButtonClicked = false;

    private void Start()
    {
        startButton.onClick.AddListener(GameStart);
        endButton.onClick.AddListener(GameEnd);
    }

    private void GameStart()
    {
        startButtonClicked = true;
        SceneManager.LoadScene(1);
    }

    private void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
