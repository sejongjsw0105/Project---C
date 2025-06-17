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
    Stunned,
    Immune,
    SideExposed,
    OnFire,
    Defend,
    Reloading,
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
    public virtual int OnBeforeDamage(Unit from,  Unit target, DamageType damageType, int damage) => damage;
    public virtual void OnAfterDamage(Unit from, Unit target, DamageType damageType, int damage) {}
    public virtual int OnBeforeAttack(Unit attacker, Unit target, int damage) { return damage; }
    public virtual void OnAfterAttack(Unit attacker, Unit target, int damage) { }
    public virtual int OnBeforeSupport(Unit supporter, Area area) { return 0; }
    public virtual void OnAfterSupport(Unit supporter, Area area) { }
    public virtual int OnBeforeMove(Unit unit, Area target) => 0;
    public virtual void OnAfterMove(Unit unit, Area target)
    {
        // �̵� �� ó���� ������ �ִٸ� ���⿡ �ۼ�
    }

}
