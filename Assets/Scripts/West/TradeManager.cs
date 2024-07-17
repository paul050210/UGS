using UnityEngine;
using UnityEngine.UI;

public class TradeGuideUI : MonoBehaviour
{
    public static TradeGuideUI Instance;

    [SerializeField] private Text totalValueText;
    private int totalValue;

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

    public void SetTotalValue(int value)
    {
        totalValue = value;
        totalValueText.text = "Total Value: " + totalValue.ToString();
    }

    public void OnTradeCompleted()
    {
        // �ŷ� �Ϸ� �� ó�� ����
    }
}
