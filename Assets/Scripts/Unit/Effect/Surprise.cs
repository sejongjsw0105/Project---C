using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class Surprise: UnitTrait
{
    Area lastSurprisedArea = null; 
    Unit lastSurprised = null; // 공격 대상이 공격 불가능 상태인지 확인하기 위한 변수
    public Surprise()
    {
        type = UnitTraitType.Surprise;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee, Unit.UnitType.Cavalry };
    }
    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        if (!attacker == attacker.area.firstAttacker) return (damage, true); 
        if (target == lastSurprised) return (damage, true); 
        target.isAttackable = false; // 공격 대상이 공격 불가능 상태로 변경
        lastSurprised = target; // 마지막 공격 대상 저장
        lastSurprisedArea = attacker.area; // 마지막 공격 대상의 지역 저장
        return (damage, true);
    }
    public override void OnUpdate(Unit from, Area area)
    {
        if (lastSurprisedArea == area && lastSurprised.area == area) return;
        lastSurprisedArea = null;
        lastSurprised = null; // 마지막 공격 대상 초기화
    }
}
