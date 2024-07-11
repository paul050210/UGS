using System;
using UnityEngine;

[Serializable]
public class UIItem
{
    public Item item { get; private set;}
    public int index { get; private set; }
    public GameObject gameObject;

    public UIItem(Item item, int index)
    {
        this.item = item;
        this.index = index;
    }

    public override int GetHashCode()
    {
        return item.GetHashCode() + index.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        UIItem i = obj as UIItem;
        return i != null && (i.item.Equals(this.item) && i.index == this.index);
    }
}