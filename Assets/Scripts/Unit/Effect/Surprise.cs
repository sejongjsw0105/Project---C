using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class Surprise: UnitTrait
{
    Area lastSurprisedArea = null; 
    Unit lastSurprised = null; // ���� ����� ���� �Ұ��� �������� Ȯ���ϱ� ���� ����
    public Surprise()
    {
        type = UnitTraitType.Surprise;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee, Unit.UnitType.Cavalry };
    }
    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        if (!attacker == attacker.area.firstAttacker) return (damage, true); 
        if (target == lastSurprised) return (damage, true); 
        target.isAttackable = false; // ���� ����� ���� �Ұ��� ���·� ����
        lastSurprised = target; // ������ ���� ��� ����
        lastSurprisedArea = attacker.area; // ������ ���� ����� ���� ����
        return (damage, true);
    }
    public override void OnUpdate(Unit from, Area area)
    {
        if (lastSurprisedArea == area && lastSurprised.area == area) return;
        lastSurprisedArea = null;
        lastSurprised = null; // ������ ���� ��� �ʱ�ȭ
    }
}
