using UnityEngine;
using static Unit;
public enum StackType
{
    duration, // 지속 시간 증가
    value,    // 효과 값 증가
    count,    // 효과 횟수 증가
    none      // 스택 없음
}
public enum StatusEffectType
{
    Stunned,
    Immune,
    SideExposed,
    OnFire,
    Defend,
    Reloading,
    // 필요에 따라 추가
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
        Debug.Log($"[{type}] 상태 효과 적용됨: {target.unitName}, 지속 시간: {duration}턴, 값: {value} {(buff ? "버프" : "디버프")}");
    }
    public virtual void OnUpdate(Unit target, Area area)
    {

    }
    public virtual void OnExpire(Unit target, Area area)
    {
        Debug.Log($"[{type}] 상태 효과 만료됨: {target.unitName}, 지속 시간: {duration}턴, 값: {value} {(buff ? "버프" : "디버프")}");
        // 상태 효과가 만료되면 추가적인 로직이 필요할 경우 여기에 작성
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
        // 이동 후 처리할 로직이 있다면 여기에 작성
    }

}
