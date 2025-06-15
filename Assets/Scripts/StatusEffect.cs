using UnityEngine;

public enum StatusEffectType
{
    Stunned,
    Poisoned,
    BuffedAttack,
    WeakenedDefense,
    Immune,
    // 필요에 따라 추가
}

public class StatusEffect
{
    public StatusEffectType type;
    public int duration; // 남은 턴 수
    public int value;    // 수치적 영향 (ex. 공격력 +5)

    public StatusEffect(StatusEffectType type, int duration, int value = 0)
    {
        this.type = type;
        this.duration = duration;
        this.value = value;
    }

    public void OnApply(Unit target) { /* 적용 시 효과 */ }
    public void OnExpire(Unit target) { /* 해제 시 효과 */ }
}
