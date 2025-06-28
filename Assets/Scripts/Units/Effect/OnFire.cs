using UnityEngine;

public class OnFire : StatusEffect
{
    public OnFire(int value, int duration) : base("OnFire", StackType.duration, duration, value)
    {
    }
    public override void AreaOnApply(Area area)
    {
        area.occupyingEnemyUnit.AddStatusEffect(new OnFire(value, 1)); // ������ �ִ� �� ���ֿ��Ե� �� ȿ���� ����
        area.occupyingFriendlyUnit.AddStatusEffect(new OnFire(value, 1)); // ������ �ִ� �Ʊ� ���ֿ��Ե� �� ȿ���� ����
    }
    public override void OnTurnEnd(Unit target)
    {
        DamageAction.Execute(null, target, DamageType.Debuff, value); // 2. ��󿡰� ���ظ� ����
    }
    public override void AreaOnTurnEnd(Area area)
    {
        // ���� �� ��� ���ֿ��� �� ȿ�� ����
        if (area.occupyingEnemyUnit != null)
            area.occupyingEnemyUnit.AddStatusEffect(new OnFire(value, 2));
        if (area.occupyingFriendlyUnit != null)
            area.occupyingFriendlyUnit.AddStatusEffect(new OnFire(value, 2));
    }

}