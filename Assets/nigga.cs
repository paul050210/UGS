using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nigga : MonoBehaviour
{
    Button btn;
    SaveManager mgr;
    void Start()
    {
        mgr = GetComponent<SaveManager>();
        btn = this.gameObject.GetComponent<Button>();

        btn.onClick.AddListener(() => save());
    }

    void save()
    {
        mgr.LoadGameSettingData();
    }
}
