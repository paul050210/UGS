using System;


[Serializable]
public enum ItemType
{
    herb,
    stone,
    bio,
    potion,
    another
}

[Serializable]
public class Item
{
    public ItemType type;
    public string itemName;
    public string itemDesc;

    public override int GetHashCode()
    {
        return type.GetHashCode() + itemName.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        Item i = obj as Item;
        return i != null && (i.itemName == this.itemName);
    }

    //public static bool operator ==(Item lhs, Item rhs)
    //{
    //    return lhs.Equals(rhs);
    //}

    //public static bool operator !=(Item lhs, Item rhs)
    //{
    //    return !lhs.Equals(rhs);
    //}
}
