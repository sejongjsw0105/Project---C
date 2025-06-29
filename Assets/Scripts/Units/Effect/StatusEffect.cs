using System;
using UnityEngine;
public enum StackType
{
    duration, // ���� �ð� ����
    value,    // ȿ�� �� ����
    none      // ���� ����
}
public enum EffectType
{
    Buff,     // ����
    Debuff,   // �����
    Neutral   // �߸� ȿ��
}
public abstract class StatusEffect : BaseActionModifier
{
    public float scoreBonus = 0f;
    public string effectName; // ȿ�� �̸�
    public StackType stackType;
    public bool isPersistent;
    public EffectType effectType; // ȿ�� ���� (����, �����, �߸�)
    public int duration;
    public int value;
    protected StatusEffect(string effectName, StackType stackType, EffectType effectType ,int duration, int value)
    {
        this.isPersistent = false; // �⺻���� �񿵱��� ȿ��
        this.effectType = effectType; // ȿ�� ���� ����
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
        target.statusEffects.Remove(this); // ���� ȿ�� ����
    }
    public virtual void AreaOnApply(IArea area) { }
    public virtual void AreaOnExpire(IArea area) { }
    public virtual void AreaOnSupport(IArea area, IUnit supporter, int value) { }
    public virtual void AreaOnTurnEnd(IArea area) { }
    public virtual void AreaExpire(IArea area)
    {
        AreaOnExpire(area);
        area.statusEffects.Remove(this); // ���� ȿ�� ����
    }

}
