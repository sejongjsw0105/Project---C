using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
[Serializable]
public enum DamageType
{
    Support,
    Damage,
    Debuff,
}
[Serializable]
public enum Faction
{
    Friendly,   // 우리 진영  
    Enemy,       // 적 진영  
    Neutral,      // 중립 진영  
    All,
    Other,
    Same
}
[Serializable]
public enum UnitType
{
    Melee,       // 보병  
    Cavalry,        // 기병  
    Ranged,         // 원거리  
    RangedCavalry,   // 원거리 기병  
}
[Serializable]
public class UnitStats
{
    public int maxHealth;
    public int attackPower;
    public int defensePower;
    public int range;
}
public abstract class Unit : MonoBehaviour, IUnit
{
    public UnitData unitData { get; set; } // 유닛 데이터
    public UnitState state { get; set; } = new UnitState();
    public UnitStats stats { get; set; }
    public UnitStats upgradedStats { get; set; }
    public IArea area { get; set; }
    public float unitPriority { get; set; } = 1.0f; // 유닛 우선순위
    public int currentHealth { get; set; }
    public string unitName { get; set; }
    public UnitType unitType { get; set; }
    public Faction faction { get; set; }
    public List<UnitTrait> unitTrait { get; set; } = new();
    public List<StatusEffect> statusEffects { get; set; } = new();
    public int unitId { get; set; }
    public Faction supportableFaction { get; set; } = Faction.Other; // 예: Friendly 유닛만 지원 가능

    public T GetStatusEffect<T>() where T : StatusEffect
    {
        return statusEffects.FirstOrDefault(e => e is T) as T;
    }
    public StatusEffect GetStatusEffectByName(string effectName)
    {
        return statusEffects.FirstOrDefault(e => e.effectName == effectName);
    }
    public void Start()
    {
        UnitManager.Instance.RegisterUnit(this);
    }

    private void OnMouseDown()
    {
        BattleManager.Instance.stateMachine.CurrentState.HandleUnitClick(this);
    }

    public void BeginBattle()
    {
        foreach (var trait in unitTrait) trait.OnBattleStart(this);
        foreach (var effect in statusEffects) effect.OnBattleStart(this);
        foreach (var artifact in GameContext.Instance.myArtifacts) artifact.OnBattleStart(this);
    }

    public void BeforeTurnStart()
    {
        state.ResetTurn();
        foreach (var e in statusEffects) e.OnTurnStart(this);
        foreach (var t in unitTrait) t.OnTurnStart(this);
        foreach (var a in GameContext.Instance.myArtifacts) a.OnTurnStart(this);
    }

    public void AfterTurnEnd()
    {
        foreach (var t in unitTrait) t.OnTurnEnd(this);
        foreach (var e in statusEffects) e.OnTurnEnd(this);
        foreach (var a in GameContext.Instance.myArtifacts) a.OnTurnEnd(this);
    }
    public void Die()
    {
        UnitManager.Instance.UnregisterUnit(this);
        area.RemoveOccupant(this);
        foreach (var x in statusEffects) x.OnDie(this);
        foreach (var x in unitTrait) x.OnDie(this);
        foreach (var x in GameContext.Instance.myArtifacts) x.OnDie(this);
        GameEvents.OnUnitDied.Invoke(this);
        Destroy(gameObject);
    }
    public void Win()
    {
        foreach (var t in unitTrait)
            t.OnWin(this);
        foreach (var e in statusEffects)
            e.OnWin(this);
        foreach (var a in GameContext.Instance.myArtifacts)
            a.OnWin(this);
    }

    public void Lose()
    {
        foreach (var t in unitTrait)
            t.OnLose(this);
        foreach (var e in statusEffects)
            e.OnLose(this);
        foreach (var a in GameContext.Instance.myArtifacts)
            a.OnLose(this);
    }

    public virtual void AddStatusEffect(StatusEffect newEffect)
    {
        newEffect.OnApply(this); // 상태 효과 적용
        var existing = statusEffects.Find(e => e.effectName == newEffect.effectName);

        if (existing != null)
        {
            HandleExistingStatusEffect(existing, newEffect);
        }
        else
        {
            AddNewStatusEffect(newEffect);
        }
    }

    public void HandleExistingStatusEffect(StatusEffect existing, StatusEffect newEffect)
    {
        switch (newEffect.stackType)
        {
            case StackType.duration:
                existing.duration += newEffect.duration;
                statusEffects.Remove(existing); // 기존 효과를 제거하여 중복 방지
                AddNewStatusEffect(existing); // 새로운 효과를 추가하여 로그 출력                
                break;
            case StackType.value:
                existing.value += newEffect.value;
                statusEffects.Remove(existing); // 기존 효과를 제거하여 중복 방지
                AddNewStatusEffect(existing); // 새로운 효과를 추가하여 로그 출력 
                break;
            case StackType.none:
                break;
        }
    }
    private void AddNewStatusEffect(StatusEffect newEffect)
    {
        statusEffects.Add(newEffect);
    }
    public void UpdateUnitDataFromBattle()
    {
        if (unitData == null)
            return;

        unitData.currentHealth = currentHealth;

        unitData.statusEffects = statusEffects
            .Where(e => e.isPersistent) // 스테이지 지속 효과만 저장
            .ToList();
    }

}
