using UnityEngine;

public class OnFire : StatusEffect
{
    public OnFire(int value, int duration) : base("OnFire", StackType.duration, EffectType.Debuff, duration, duration)
    {
    }
    public override void AreaOnApply(IArea area)
    {
        area.occupyingEnemyUnit.AddStatusEffect(new OnFire(value, 1)); // ������ �ִ� �� ���ֿ��Ե� �� ȿ���� ����
        area.occupyingFriendlyUnit.AddStatusEffect(new OnFire(value, 1)); // ������ �ִ� �Ʊ� ���ֿ��Ե� �� ȿ���� ����
    }
    public override void OnTurnEnd(IUnit target)
    {
        DamageAction.Execute(null, target, DamageType.Debuff, value); // 2. ��󿡰� ���ظ� ����
    }
    public override void AreaOnTurnEnd(IArea area)
    {
        // ���� �� ��� ���ֿ��� �� ȿ�� ����
        if (area.occupyingEnemyUnit != null)
            area.occupyingEnemyUnit.AddStatusEffect(new OnFire(value, 2));
        if (area.occupyingFriendlyUnit != null)
            area.occupyingFriendlyUnit.AddStatusEffect(new OnFire(value, 2));
    }

}