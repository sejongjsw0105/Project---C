using System;
using System.Collections.Generic;
using UnityEngine;

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

public class Area : MonoBehaviour
{
    public Unit firstAttacker;  // 새로 이동해온 유닛
    public Unit secondAttacker;
    public int areaIndexX;
    public int areaIndexY;
    public AreaType areaType;          // 영역의 타입
    public Unit occupyingFriendlyUnit;         // 현재 유닛
    public Unit occupyingEnemyUnit;         // 현재 영역을 점령하고 있는 유닛
    public AreaCondition areaCondition; // 영역의 상태
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    public Tuple<int, int> GetPosition()
    {
        return Tuple.Create(areaIndexX, areaIndexY);
    }
    public void SetAreaCondition()
    {
        Unit unit1 = occupyingFriendlyUnit;
        Unit unit2 = occupyingEnemyUnit;
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
    public void AddStatusEffect(StatusEffect effect)
    {
        statusEffects.Add(effect);
        effect.OnApply(null, this); // 상태 효과 적용
    }
    //상태이상 턴마다, 제거시
    public void UpdateStatusEffects()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            StatusEffect effect = statusEffects[i];

            effect.OnUpdate(null, this);       //  1. 턴 중 효과 적용
            effect.duration--;           //  2. 턴 종료 시 duration 감소

            if (effect.duration <= 0)
            {
                effect.OnExpire(null, this);   //  3. 만료 시 해제
                statusEffects.RemoveAt(i);
            }
        }
    }

}
