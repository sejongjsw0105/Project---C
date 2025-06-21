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
    private void Start()
    {
        if (AreaManager.Instance != null)
        {
            AreaManager.Instance.RegisterArea(this);
        }
        switch (areaIndexY)
        {
            case 0: areaType = AreaType.FriendlyFinal; break;
            case 1: areaType = AreaType.FriendlyRear; break;
            case 2: areaType = AreaType.Frontline; break;
            case 3: areaType = AreaType.EnemyRear; break;
            case 4: areaType = AreaType.EnemyFinal; break;
        }
    }
    public Tuple<int, int> GetPosition()
    {
        return Tuple.Create(areaIndexX, areaIndexY);
    }
    public void UpdateAreaCondition()
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
    public virtual void AddStatusEffect(StatusEffect newEffect)
    {
        newEffect.OnApply(null,this); // 상태 효과 적용
        var existing = statusEffects.Find(e => e.type == newEffect.type);

        if (existing != null)
        {
            HandleExistingStatusEffect(existing, newEffect);
        }
        else
        {
            AddNewStatusEffect(newEffect);
        }
    }

    private void HandleExistingStatusEffect(StatusEffect existing, StatusEffect newEffect)
    {
        switch (newEffect.stackType)
        {
            case StackType.duration:
                existing.duration += newEffect.duration;
                statusEffects.Remove(existing); // 기존 효과를 제거하여 중복 방지
                AddNewStatusEffect(existing); // 새로운 효과를 추가하여 로그 출력                
                Debug.Log($"[{newEffect.type}] 지속시간이 중첩되었습니다. 총 {existing.duration}턴");
                break;
            case StackType.value:
                existing.value += newEffect.value;
                statusEffects.Remove(existing); // 기존 효과를 제거하여 중복 방지
                AddNewStatusEffect(existing); // 새로운 효과를 추가하여 로그 출력 
                Debug.Log($"[{newEffect.type}] 효과값이 중첩되었습니다. 총 value: {existing.value}");
                break;
            case StackType.count:
                existing.count += newEffect.count;
                statusEffects.Remove(existing); // 기존 효과를 제거하여 중복 방지
                AddNewStatusEffect(existing); // 새로운 효과를 추가하여 로그 출력 
                Debug.Log($"[{newEffect.type}] 사용 횟수가 중첩되었습니다. 총 count: {existing.count}");
                break;
            case StackType.none:
                break;
        }
    }

    private void AddNewStatusEffect(StatusEffect newEffect)
    {
        statusEffects.Add(newEffect);
    }
    //상태이상 턴마다, 제거시
    public void UpdateStatusEffects()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            var effect = statusEffects[i];
            effect.OnUpdate(null,this); // 상태 효과 업데이트
            if (effect.duration > 0)
                effect.duration--;

            if (effect.duration <= 0)
                RemoveExpiredEffect(effect); // index 없이 제거
        }
    }
    public void RemoveExpiredEffect(StatusEffect effect)
    {
        effect.OnExpire(null, this);
        statusEffects.Remove(effect);
    }

}
