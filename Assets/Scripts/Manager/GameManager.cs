using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private int currentDay;
    public int CurrentDay => currentDay;


    private void Awake()
    {
        currentDay = SaveManager.Instance.LoadDay();
    }
}
