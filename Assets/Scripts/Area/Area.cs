using System;
using Mono.Cecil;
using UnityEngine;

public interface IArea
{
    Tuple<int,int> GetPosition();
}
public interface IAreaClickable
{
    void OnAreaClicked(Area area);
}

public enum AreaCondition
{
    InCombat, // ���� ��
    Empty, // ��� ����
    FriendlyOccupied, // �Ʊ� ����
    EnemyOccupied, // ���� ����
    NeutralOccupied // �߸� ����
}
public enum AreaType
{
    FriendlyFinal,   // �츮 ���Ĺ� (1ĭ)
    FriendlyRear,    // �츮 �Ĺ� (3ĭ)
    Frontline,       // ���� (3ĭ, ���� �߻� ����)
    EnemyRear,       // �� �Ĺ� (3ĭ)
    EnemyFinal       // �� ���Ĺ� (1ĭ)
}

public class Area : MonoBehaviour, IArea
{
    public int areaIndexX;
    public int areaIndexY;
    public AreaType areaType;          // ������ Ÿ��
    public Unit occupyingFriendlyUnit;         // ���� ����
    public Unit occupyingEnemyUnit;         // ���� ������ �����ϰ� �ִ� ����
    public bool isOccupied => occupyingFriendlyUnit != null || occupyingEnemyUnit != null; // ������ ���ɵǾ����� ����
    public AreaCondition areaCondition; // ������ ����
    public Tuple<int, int> GetPosition()
    {
        return Tuple.Create(areaIndexX, areaIndexY);
    }
    public void SetAreaCondition(Unit unit1, Unit unit2)
    {
        if (unit1 != null && unit2 != null)
        {
            areaCondition = AreaCondition.InCombat; // �� ������ ��� �����ϸ� ���� ��
        }
        else if (unit1 != null)
        {
            areaCondition = AreaCondition.FriendlyOccupied; // �Ʊ� ���ָ� ����
        }
        else if (unit2 != null)
        {
            areaCondition = AreaCondition.EnemyOccupied; // ���� ���ָ� ����
        }
        else
        {
            areaCondition = AreaCondition.Empty; // ��� ����
        }

    }
}
