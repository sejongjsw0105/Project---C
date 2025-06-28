using UnityEngine;

public class UnitState : MonoBehaviour
{

    public bool CanAttack = true; // ������ ���� �������� ����
    public bool CanMove = true;   // ������ �̵� �������� ����
    public bool CanSupport = true; // ������ ���� �������� ����
    public bool CanDamaged = true; // ������ ���ظ� ���� �� �ִ��� ����
    public bool CanDefend = true; // ������ ��� ������ ����
    public void BeginBattle()
    {

    }
    public void ResetTurn()
    {
        CanAttack = true; // �� ���� �� ���� ����
        CanMove = true;   // �� ���� �� �̵� ����
        CanSupport = true; // �� ���� �� ���� ����
        CanDamaged = true; // �� ���� �� ���� ���� �� ����
        CanDefend = true; // �� ���� �� ��� ����
    }
}

