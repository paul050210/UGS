using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item item;
    public Sprite sprite;
}
