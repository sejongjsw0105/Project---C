using UnityEngine;

public enum StatusEffectType
{
    Stunned,
    Poisoned,
    BuffedAttack,
    WeakenedDefense,
    Immune,
    // �ʿ信 ���� �߰�
}

public class StatusEffect
{
    public StatusEffectType type;
    public int duration; // ���� �� ��
    public int value;    // ��ġ�� ���� (ex. ���ݷ� +5)

    public StatusEffect(StatusEffectType type, int duration, int value = 0)
    {
        this.type = type;
        this.duration = duration;
        this.value = value;
    }

    public void OnApply(Unit target) { /* ���� �� ȿ�� */ }
    public void OnExpire(Unit target) { /* ���� �� ȿ�� */ }
}
