using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DaySO")]
public class DayInfo : ScriptableObject
{
    [SerializeField] private List<Quest> quests;
    public List<Quest> Quests => quests;
    [SerializeField] private int dayIndex;


}
