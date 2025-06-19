using UnityEngine;

public class Defending : StatusEffect
{
    public int Shield = 0;

    public Defending(int duration, int value)
        : base(StatusEffectType.Defend, StackType.value, true, value,duration,1000)
    {
    }

    public override void OnApply(Unit target, Area area)
    {
        base.OnApply(target, area);
        Shield += value; // ���� ����
        Debug.Log($"[Defend] {target.unitName}�� ������ {value} �����߽��ϴ�. (���� ����: {target.defensePower})");
    }

    public override (int,bool) OnBeforeDamaged(Unit from, Unit target, Unit.DamageType damageType, int incomingDamage)
    {
        int absorbed = Mathf.Min(Shield, incomingDamage);
        Shield -= absorbed;
        int result = incomingDamage - absorbed;

        Debug.Log($"[Defend] {target.unitName}�� ���� {absorbed} ���ظ� ����߽��ϴ�. (���� ��: {Shield})");
        if (Shield <= 0)
        {
            target.RemoveExpiredEffect(this); // ���� 0 ���ϰ� �Ǹ� ȿ�� ����

        }
        return (result,true);
    }

    public override void OnExpire(Unit target, Area area)
    {
        base.OnExpire(target, area);
        Shield =0; // ���� ���󺹱�
        Debug.Log($"[Defend] {target.unitName}�� ������ {value} �����߽��ϴ�. (���� ����: {target.defensePower})");
    }
}
