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
}
