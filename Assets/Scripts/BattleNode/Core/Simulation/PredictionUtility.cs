using System.Collections.Generic;
using UnityEngine;

public class PredictionUtility : MonoBehaviour
{
    public static int PredictIncomingDamageToEnemyAt(
    Area area,
    Dictionary<Unit, FakeUnit> clonedFriendlies
)
    {
        int totalDamage = 0;
        int maxAttackDamage = 0;
        List<Unit> supportUnits = new List<Unit>();

        foreach (var pair in clonedFriendlies)
        {
            Unit unit = pair.Key;
            FakeUnit sim = pair.Value;

            var support = sim.GetSupport();
            if (support == null) continue;

            var (value, isValid) = support.BeforeSupport(area);
            if (!isValid) continue;
            if (!support.validFactions.Contains(Unit.Faction.Enemy)) continue;

            support.AfterSupport(area, value);
            supportUnits.Add(unit);
            totalDamage += value;
        }

        if (area.occupyingEnemyUnit != null)
        {
            if (area.occupyingFriendlyUnit != null)
            {
                Unit unit = area.occupyingFriendlyUnit;
                if (clonedFriendlies.TryGetValue(unit, out var sim) && !supportUnits.Contains(unit))
                {
                    int value = sim.BeforeAttack(area.occupyingEnemyUnit);
                    sim.AfterAttack(area.occupyingEnemyUnit, value);
                    maxAttackDamage = value;
                }
            }
            else
            {
                foreach (var pair in clonedFriendlies)
                {
                    Unit unit = pair.Key;
                    FakeUnit sim = pair.Value;
                    if (supportUnits.Contains(unit)) continue;

                    int moveRange = sim.BeforeMove(sim, area);
                    if (!sim.isMovable) continue;
                    if (!GridHelper.Instance.IsFriendlyMoveAllowed(unit.area, area, moveRange)) continue;

                    int value = sim.BeforeAttack(area.occupyingEnemyUnit);
                    if (!sim.isAttackable) continue;

                    sim.AfterAttack(area.occupyingEnemyUnit, value);
                    if (value > maxAttackDamage)
                        maxAttackDamage = value;
                }
            }
        }

        totalDamage += maxAttackDamage;
        return totalDamage;
    }
    public static int PredictGivenAttackDamageToFriendlyAt(
    Unit attacker,
    Area targetArea,
    Dictionary<Unit, FakeUnit> clonedFriendlies
)
    {
        Unit target = targetArea.occupyingFriendlyUnit;
        if (target == null || !clonedFriendlies.ContainsKey(target))
            return 0;

        FakeUnit sim = clonedFriendlies[target];
        int value = attacker.BeforeAttack(target);
        var (final, _) = sim.BeforeDamaged(attacker, Unit.DamageType.Damage, value);
        sim.AfterDamaged(attacker, Unit.DamageType.Damage, final);
        return final;
    }
    public static int PredictSupportDamageToFriendlyAt(
    Area targetArea,
    Dictionary<Unit, FakeUnit> clonedFriendlies,
    Dictionary<Unit, FakeUnit> clonedEnemies
)
    {
        Unit target = targetArea.occupyingFriendlyUnit;
        if (target == null || !clonedFriendlies.ContainsKey(target))
            return 0;

        FakeUnit simTarget = clonedFriendlies[target];
        int totalSupport = 0;

        foreach (var pair in clonedEnemies)
        {
            Unit unit = pair.Key;
            FakeUnit sim = pair.Value;

            var support = unit.GetSupport();
            if (support == null) continue;

            var (value, isValid) = support.BeforeSupport(targetArea);
            support.DoSupport(targetArea); // Execute support action
            if (!isValid) continue;
            if (!support.validFactions.Contains(Unit.Faction.Friendly)) continue;
            support.AfterSupport(targetArea, value);
            var (final, _) = simTarget.BeforeDamaged(unit, Unit.DamageType.Support, value);
            simTarget.AfterDamaged(unit, Unit.DamageType.Support, final);
            totalSupport += final;
        }

        return totalSupport;
    }
}
