using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SOGenerator : MonoBehaviour
{
    [SerializeField] private Button generateButton;

    private void Start()
    {
        generateButton.onClick.AddListener(Generate);
    }

    private void Generate()
    {
        var datas = ItemTable.Data.GetList();
        foreach (var data in datas)
        {
            Item testItem = new Item();
            testItem.type = data.itemType;
            testItem.itemName = data.itemName;
            testItem.itemDesc = data.itemDesc;
            testItem.itemPrice = data.itemPrice;

            ItemSO testSO = ScriptableObject.CreateInstance<ItemSO>();
            testSO.item = testItem;
            testSO.sprite = null;
            
            AssetDatabase.CreateAsset(testSO, $"Assets/Resources/SO/Item/{testItem.itemName}.asset");
            
        }
    }
}
