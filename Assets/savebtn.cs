using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class savebtn : MonoBehaviour
{

    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() => save());
    }

    void save()
    {
        Day day = new Day(GameManager.Instance.CurrentDay);
        SaveManager.Instance.SaveItemData();
        SaveManager.Instance.SaveGameSettingData();
        SaveManager.Instance.SaveItemDicData();
        SaveManager.Instance.SaveDay(day);
        SaveManager.Instance.SaveQuestInfo();
    }

}
