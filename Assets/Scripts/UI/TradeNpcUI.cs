using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeNpcUI : MonoBehaviour
{
    [SerializeField] private TradeUI tradeUI;
    [SerializeField] private TradeInfo tradeInfo;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TurnOnTrade);
    }

    private void TurnOnTrade()
    {
        ItemManager.Instance.canSelect = true;
        tradeUI.TurnOn(tradeInfo);
    }
}
