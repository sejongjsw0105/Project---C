using System;
using UnityEngine;
public enum StackType
{
    duration, // 지속 시간 증가
    value,    // 효과 값 증가
    none      // 스택 없음
}
public enum EffectType
{
    Buff,     // 버프
    Debuff,   // 디버프
    Neutral   // 중립 효과
}
public abstract class StatusEffect : BaseActionModifier
{
    public float scoreBonus = 0f;
    public string effectName; // 효과 이름
    public StackType stackType;
    public bool isPersistent;
    public EffectType effectType; // 효과 유형 (버프, 디버프, 중립)
    public int duration;
    public int value;
    protected StatusEffect(string effectName, StackType stackType, EffectType effectType ,int duration, int value)
    {
        this.isPersistent = false; // 기본값은 비영구적 효과
        this.effectType = effectType; // 효과 유형 설정
        this.effectName = effectName;
        this.stackType = stackType;
        this.duration = duration;
        this.value = value;
    }
    public virtual void UpdateTurn(IUnit target)
    {
        duration--;
        if (duration <= 0)
        {
            OnExpire(target);
        }
    }
    public virtual void Expire(IUnit target)
    {
        OnExpire(target);
        target.statusEffects.Remove(this); // 상태 효과 제거
    }
    public virtual void AreaOnApply(IArea area) { }
    public virtual void AreaOnExpire(IArea area) { }
    public virtual void AreaOnSupport(IArea area, IUnit supporter, int value) { }
    public virtual void AreaOnTurnEnd(IArea area) { }
    public virtual void AreaExpire(IArea area)
    {
        AreaOnExpire(area);
        area.statusEffects.Remove(this); // 상태 효과 제거
    }

}
