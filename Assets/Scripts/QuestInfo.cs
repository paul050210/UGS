using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestInfo
{
    [Header("어느 날짜에 받은 퀘스트인지")]
    [SerializeField]
    public int day;  // 필드를 public으로 변경

    [Header("그 날짜에 어느 퀘스트를 받은지")]
    [SerializeField]
    public int index;  // 필드를 public으로 변경

    [Header("이 퀘스트 수락했는지 안했는지")]
    [SerializeField]
    public bool isAccept;  // 필드를 public으로 변경

    public void SetData(int _day, int _index, bool _isAccept)
    {
        day = _day;
        index = _index;
        isAccept = _isAccept;
    }
}