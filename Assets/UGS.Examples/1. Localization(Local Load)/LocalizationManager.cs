﻿ 
using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{

    public enum Language
    {
        EN,
        KR
    }
    static LocalizationManager instance;
    public static LocalizationManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<LocalizationManager>();
            }
            return instance;
        }
    }
     
    /// <summary>
    /// current language
    /// </summary>
    public Language currentLanguage;
     
    // Start is called before the first frame update
    void Awake()
    {
        LoadMethod1();
    }


    /// <summary>
    /// Same LoadMethod 2
    /// </summary>
    public void LoadMethod1()
    {  
        UnityGoogleSheet.LoadAllData();
    }

    /// <summary>
    /// Manually Called. Same LoadMethod 1
    /// </summary>
    public void LoadMethod2()
    {
        Debug.Log("Load Method 2 (Manually)"); 
        Example1.Localization.Item.Name.Load();
        Example1.Localization.Item.Description.Load(); 
    }

    /// <summary>
    /// Get Item Description
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public string GetItemDescription(string itemID)
    {
        var localeMap = Example1.Localization.Item.Description.GetDictionary();
        if (currentLanguage == Language.EN)
            return localeMap[itemID].EN;
        else if (currentLanguage == Language.KR)
            return localeMap[itemID].KR;

        return null;
    }

    /// <summary>
    /// Get Item Name
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public string GetItemName(string itemID)
    {
        var localeMap = Example1.Localization.Item.Name.GetDictionary();
        if(currentLanguage == Language.EN)
            return localeMap[itemID].EN;
        else if (currentLanguage == Language.KR)
            return localeMap[itemID].KR;

        return null;
    } 
}
