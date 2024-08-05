using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeNpcControl : MonoBehaviourSingleton<TradeNpcControl>
{
    private TradeNpcUI[] buttons;

    private void Start()
    {
        buttons = GetComponentsInChildren<TradeNpcUI>();
    }

    public void TurnOffAll()
    {
        for(int i = 0; i<buttons.Length;i++)
        {
            buttons[i].TurnOffTrade();
        }
    }
}
