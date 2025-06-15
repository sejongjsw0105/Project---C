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
    public Unit occupyingUnit;         // ���� ����
    public bool isOccupied => occupyingUnit != null;
    public AreaCondition areaCondition; // ������ ����
    public Tuple<int, int> GetPosition()
    {
        return Tuple.Create(areaIndexX, areaIndexY);
    }
    public void SetOccupyingUnit(Unit unit)
    {
        occupyingUnit = unit;
        areaCondition = unit == null ? AreaCondition.Empty :
            unit.faction switch
            {
                Unit.Faction.Friendly => AreaCondition.FriendlyOccupied,
                Unit.Faction.Enemy => AreaCondition.EnemyOccupied,
                _ => AreaCondition.NeutralOccupied
            };
    }

}
