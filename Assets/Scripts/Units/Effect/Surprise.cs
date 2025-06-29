using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Surprise : UnitTrait
{
    private IArea lastSurprisedArea = null;
    private IUnit lastSurprised = null;

    public Surprise()
    {
        unitTypes = new List<UnitType> { UnitType.Melee, UnitType.Cavalry };
    }

    public override ResultContext<int> OnBeforeAttack(IUnit attacker, IUnit target, int damage)
    {
        var result = new ResultContext<int>(damage);

        if (attacker != attacker.area.firstAttacker)
            return result; // ����ڰ� �ƴ� �� �ƹ� ȿ�� ����

        if (target == lastSurprised)
            return result; // �̹� ����� ��� �� �ߺ� ����

        // �ݰ� ���� ȿ�� �ο�
        target.state.CanAttack = false;

        lastSurprised = target;
        lastSurprisedArea = attacker.area;

        return result; // ������ ���� ����, ���� �ƴ�
    }

    public override void OnTurnEnd(IUnit unit)
    {
        // ��� ���� ����: Ÿ���� �ڸ��� �̵��ϰų� �����
        if (lastSurprised == null || lastSurprised.area != lastSurprisedArea)
        {
            lastSurprised = null;
            lastSurprisedArea = null;
        }
    }
}
