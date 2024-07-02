using System;
using UnityEngine;

public enum ItemType
{
    Example1,
    Example2,
    Example3,
    Example4,
    Example5
}

[Serializable]
[CreateAssetMenu(menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public ItemType type;
    public string itemName;
    public string itemDesc;
    public Sprite sprite;
}
