using UnityEngine;
using System.Collections.Generic;

public class Surprise : UnitTrait
{
    Area lastSurprisedArea = null;
    Unit lastSurprised = null;

    public Surprise()
    {
        type = UnitTraitType.Surprise;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee, Unit.UnitType.Cavalry };
    }

    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        if (attacker != attacker.area.firstAttacker) return (damage, true); // 기습자 조건
        if (target == lastSurprised) return (damage, true); // 이미 적용된 대상

        target.isAttackable = false; // 반격 봉쇄
        lastSurprised = target;
        lastSurprisedArea = attacker.area;
        return (damage, true);
    }

    public override void OnUpdate(Unit from)
    {
        if (lastSurprised == null || lastSurprised.area != lastSurprisedArea)
        {
            lastSurprised = null;
            lastSurprisedArea = null;
        }
    }
}