using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SceneSO")]
public class SceneSO : ScriptableObject
{
    [SerializeField] private int startIndex;
    [SerializeField] private int endIndex;
}
