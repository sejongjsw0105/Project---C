using System;
using UnityEngine;
using static Unit;
public abstract class Artifact
{
    public string artifactName; // Artifact name
    public void OnGetArtifact()
    {
        if (!GameContext.Instance.myArtifacts.Contains(this))
        {
            GameContext.Instance.myArtifacts.Add(this);
        }
        
    }

    public virtual void OnNodeClicked()
    {
    }
    public virtual void OnApply(Unit target, Area area)
    {
    }
    public virtual void OnUpdate(Unit target, Area area)
    {

    }
    public virtual void OnExpire(Unit target, Area area)
    {

    }
    public virtual (int, bool) OnBeforeDamaged(Unit from, Unit target, DamageType damageType, int damage) { return (damage, true); }
    public virtual void OnAfterDamaged(Unit from, Unit target, DamageType damageType, int damage) { }
    public virtual (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage) { return (damage, true); }
    public virtual void OnAfterAttack(Unit attacker, Unit target, int damage) { }
    public virtual (int, bool) OnBeforeSupport(Unit supporter, Area area, int value) { return (value, true); }
    public virtual void OnAfterSupport(Unit supporter, Area area, int value) { }
    public virtual (int, bool) OnBeforeMove(Unit unit, Area target, int moveRange) { return (moveRange, true); }
    public virtual void OnAfterMove(Unit unit, Area target) { }
    public virtual int OnBeforeDefend(Unit unit) => 0;
    public virtual void OnDie(Unit unit) { }
    public virtual void OnWin(Unit unit) { }
    public virtual void OnLose(Unit unit) { }
    public virtual void OnTurnStart(Unit unit, Area area)
    {
        // 턴 시작 시 처리할 로직이 있다면 여기에 작성
    }
}
