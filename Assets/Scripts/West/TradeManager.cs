using UnityEngine;
using UnityEngine.UI;



[Serializable]
public struct ItemData
{
    public EntityCode id;
    public string name;
    [TextArea] public string description;

    public ItemData(EntityCode id, string name, string description)
    {
        this.id = id;
        this.name = name;
        this.description = description;
    }
}


public class TradeManager : MonoBehaviour
{
    public static TradeManager Instance;

    [SerializeField] private Text totalPriceText;
    [SerializeField] private int tradersFriendship;
    [SerializeField] private int playersFriendship;
    [SerializeField] private int itemPrice;


    public int totalPrice;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTotalPrice(int value)
    {
        totalPrice = value;
        totalPriceText.text = "Total Price: " + totalPrice.ToString();
    }

    public void OnTradeCompleted()
    {
        // 거래 완료 시 처리 로직
    }
}
