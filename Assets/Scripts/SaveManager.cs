using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviourSingleton<SaveManager>
{
    private string GameSettingDataFileName = "GameSettingData.json";

    public GameData gameSettingData = new GameData();

    public void LoadGameSettingData()
    {
        string filePath = Application.persistentDataPath + "/" + GameSettingDataFileName;

        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            gameSettingData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
    }

    public void SaveGameSettingData()
    {
        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        string ToJsonData = JsonUtility.ToJson(gameSettingData, true);
        string filePath = Application.persistentDataPath + "/" + GameSettingDataFileName;

        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);
    }
}
