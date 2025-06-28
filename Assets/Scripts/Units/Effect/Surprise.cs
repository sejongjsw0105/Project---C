using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Surprise : UnitTrait
{
    private Area lastSurprisedArea = null;
    private Unit lastSurprised = null;

    public Surprise()
    {
        unitTypes = new List<UnitType> { UnitType.Melee, UnitType.Cavalry };
    }

    public override ResultContext<int> OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        var result = new ResultContext<int>(damage);

        if (attacker != attacker.area.firstAttacker)
            return result; // 기습자가 아님 → 아무 효과 없음

        if (target == lastSurprised)
            return result; // 이미 적용된 대상 → 중복 차단

        // 반격 봉쇄 효과 부여
        target.state.CanAttack = false;

        lastSurprised = target;
        lastSurprisedArea = attacker.area;

        return result; // 데미지 변경 없음, 차단 아님
    }

    public override void OnTurnEnd(Unit unit)
    {
        // 기습 상태 리셋: 타겟이 자리를 이동하거나 사라짐
        if (lastSurprised == null || lastSurprised.area != lastSurprisedArea)
        {
            lastSurprised = null;
            lastSurprisedArea = null;
        }
    }
}
