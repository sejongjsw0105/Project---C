using System;
using UnityEngine;
using static Unit;
public enum StackType
{
    duration, // ���� �ð� ����
    value,    // ȿ�� �� ����
    count,    // ȿ�� Ƚ�� ����
    none      // ���� ����
}
public enum StatusEffectType
{
    Covered, 
    Defeated, 
    VisionLess,
    Boosted,
    Protected,
    SideExposed,
    OnFire,
    Defend,
    Reloading,
    Chaos,
    // �ʿ信 ���� �߰�
}
public abstract class StatusEffect
{
    public StackType stackType = StackType.none;
    public StatusEffectType type;
    public bool buff;
    public int duration;
    public int value;
    public int count;


    public StatusEffect( StatusEffectType type, StackType stackType, bool buff, int value, int duration, int count)
    {
        this.type = type;
        this.buff = buff;
        this.duration = duration;
        this.value = value;
        this.count = count;
        this.stackType = stackType;
    }

    public virtual void OnApply(Unit target, Area area)
    {
        Debug.Log($"[{type}] ���� ȿ�� �����: {target.unitName}, ���� �ð�: {duration}��, ��: {value} {(buff ? "����" : "�����")}");
    }
    public virtual void OnUpdate(Unit target, Area area)
    {
    }
    public virtual void OnExpire(Unit target, Area area)
    {
        Debug.Log($"[{type}] ���� ȿ�� �����: {target.unitName}, ���� �ð�: {duration}��, ��: {value} {(buff ? "����" : "�����")}");
        // ���� ȿ���� ����Ǹ� �߰����� ������ �ʿ��� ��� ���⿡ �ۼ�
    }
    public virtual (int,bool) OnBeforeDamaged(Unit from, Unit target, DamageType damageType, int damage) { return (damage,true); }
    public virtual void OnAfterDamaged(Unit from, Unit target, DamageType damageType, int damage) {}
    public virtual (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage) { return (damage,true); }
    public virtual void OnAfterAttack(Unit attacker, Unit target, int damage) { }
    public virtual (int, bool) OnBeforeSupport(Unit supporter, Area area, int value) { return (value,true); }
    public virtual void OnAfterSupport(Unit supporter, Area area, int value) { }
    public virtual (int,bool) OnBeforeMove(Unit unit, Area target, int moveRange) { return (moveRange,true); }
    public virtual void OnAfterMove(Unit unit, Area target)
    {    }
    public virtual int OnBeforeDefend(Unit unit) => 0;
    public virtual void OnDie(Unit unit) { }
    public virtual void OnWin(Unit unit) { }
    public virtual void OnTurnStart(Unit unit, Area area)
    {
        // �� ���� �� ó���� ������ �ִٸ� ���⿡ �ۼ�
    }
    public virtual void OnLose(Unit unit)
    {
        // �й� �� ó���� ������ �ִٸ� ���⿡ �ۼ�
    }
}
