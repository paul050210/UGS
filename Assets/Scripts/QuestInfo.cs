using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestInfo
{
    [Header("��� ��¥�� ���� ����Ʈ����")]
    [SerializeField]
    public int day;  // �ʵ带 public���� ����

    [Header("�� ��¥�� ��� ����Ʈ�� ������")]
    [SerializeField]
    public int index;  // �ʵ带 public���� ����

    [Header("�� ����Ʈ �����ߴ��� ���ߴ���")]
    [SerializeField]
    public bool isAccept;  // �ʵ带 public���� ����

    public void SetData(int _day, int _index, bool _isAccept)
    {
        day = _day;
        index = _index;
        isAccept = _isAccept;
    }
}