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
    InCombat, // 전투 중
    Empty, // 비어 있음
    FriendlyOccupied, // 아군 점령
    EnemyOccupied, // 적군 점령
    NeutralOccupied // 중립 상태
}
public enum AreaType
{
    FriendlyFinal,   // 우리 최후방 (1칸)
    FriendlyRear,    // 우리 후방 (3칸)
    Frontline,       // 전방 (3칸, 교전 발생 지역)
    EnemyRear,       // 적 후방 (3칸)
    EnemyFinal       // 적 최후방 (1칸)
}

public class Area : MonoBehaviour, IArea
{
    public int areaIndexX;
    public int areaIndexY;
    public AreaType areaType;          // 영역의 타입
    public Unit occupyingUnit;         // 현재 유닛
    public bool isOccupied => occupyingUnit != null;
    public AreaCondition areaCondition; // 영역의 상태
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
