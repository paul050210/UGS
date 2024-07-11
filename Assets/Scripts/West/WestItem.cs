using UnityEngine;

public class WestItem : MonoBehaviour
{
    public int baseValue;
    public int playerPreference;

    public int GetItemValue()
    {
        return baseValue + playerPreference;
    }
}
