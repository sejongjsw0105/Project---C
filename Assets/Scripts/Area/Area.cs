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
    public Unit occupyingFriendlyUnit;         // 현재 유닛
    public Unit occupyingEnemyUnit;         // 현재 영역을 점령하고 있는 유닛
    public bool isOccupied => occupyingFriendlyUnit != null || occupyingEnemyUnit != null; // 영역이 점령되었는지 여부
    public AreaCondition areaCondition; // 영역의 상태
    public Tuple<int, int> GetPosition()
    {
        return Tuple.Create(areaIndexX, areaIndexY);
    }
    public void SetAreaCondition(Unit unit1, Unit unit2)
    {
        if (unit1 != null && unit2 != null)
        {
            areaCondition = AreaCondition.InCombat; // 두 유닛이 모두 존재하면 전투 중
        }
        else if (unit1 != null)
        {
            areaCondition = AreaCondition.FriendlyOccupied; // 아군 유닛만 존재
        }
        else if (unit2 != null)
        {
            areaCondition = AreaCondition.EnemyOccupied; // 적군 유닛만 존재
        }
        else
        {
            areaCondition = AreaCondition.Empty; // 비어 있음
        }

    }
}
