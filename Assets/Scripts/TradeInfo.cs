using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "TradeInfo")]
public class TradeInfo : ScriptableObject
{
    public string traderName;
    public Sprite charSprite;
    public List<ItemSO> itemList;
    public List<string> textList;
}
