using System;
using UnityEngine;
using static Unit;
public enum StackType
{
    duration, // 지속 시간 증가
    value,    // 효과 값 증가
    none      // 스택 없음
}
public abstract class StatusEffect : BaseActionModifier
{
    public string effectName; // 효과 이름
    public StackType stackType;
    public int duration;
    public int value;
    protected StatusEffect(string effectName, StackType stackType, int duration, int value)
    {
        this.effectName = effectName;
        this.stackType = stackType;
        this.duration = duration;
        this.value = value;
    }
    public virtual void UpdateTurn(Unit target)
    {
        duration--;
        if (duration <= 0)
        {
            OnExpire(target);
        }
    }
    public virtual void Expire(Unit target)
    {
        OnExpire(target);
        target.statusEffects.Remove(this); // 상태 효과 제거
    }
    public virtual void AreaOnApply(Area area) { }

    public virtual void AreaOnUpdate(Area area) { }

    public virtual void AreaOnExpire(Area area) { }

    public virtual void AreaOnSupport(Area area, Unit supporter, int value) { }
    public virtual void AreaOnTurnEnd(Area area) { }


    public virtual void AreaExpire(Area area)
    {
        AreaOnExpire(area);
        area.statusEffects.Remove(this); // 상태 효과 제거

    }

}
