using UnityEngine;

public class SideExposed : StatusEffect
{
    public SideExposed(int duration,int value =1)
        : base("SideExposed", StackType.value,duration, value)
    {
        // �ʿ��ϸ� �߰� �ʱ�ȭ
    }
    public override ResultContext<int> OnBeforeDamaged(Unit from, Unit target, DamageType damageType, int damage)
    {
        var context = new ResultContext<int>(damage);
        context.Modify((int)(damage * 1.5f)); // ���ط��� 50% ����
        value--;
        if (value <= 0)
        {
            Expire(target);
        }
        return context;
    }
}
