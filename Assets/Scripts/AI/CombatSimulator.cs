using System.Collections.Generic;
public static class CombatSimulator
{
    public static int PredictAttackDamage(UnitSnapshot attacker, UnitSnapshot defender)
    {
        int baseDamage = attacker.stats.attackPower;
        int finalDamage = baseDamage;

        var modifiers = defender.unitTrait.Concat<BaseActionModifier>(defender.statusEffects);

        foreach (var mod in modifiers)
        {
            var result = mod.OnBeforeDamaged(null, null, DamageType.Damage, finalDamage);
            if (result.IsBlocked)
                return 0;
            if (result.IsModified)
                finalDamage = result.Value;
        }

        return finalDamage;
    }

    public static int PredictSupportDamage(UnitSnapshot supporter, UnitSnapshot target)
    {
        int value = supporter.stats.attackPower;
        var modifiers = target.unitTrait.Concat<BaseActionModifier>(target.statusEffects);

        foreach (var mod in modifiers)
        {
            var result = mod.OnBeforeDamaged(null, null, DamageType.Support, value);
            if (result.IsBlocked)
                return 0;
            if (result.IsModified)
                value = result.Value;
        }

        return value;
    }

    public static int PredictEffectiveDefense(UnitSnapshot unit)
    {
        int defense = unit.stats.defensePower;

        foreach (var mod in unit.unitTrait.Concat<BaseActionModifier>(unit.statusEffects))
        {
            defense += mod.OnBeforeDefend(null);
        }

        return defense;
    }

    public static int PredictIncomingDamageTo(Unit target, Dictionary<Unit, UnitSnapshot> clonedFriendlies)
    {
        if (!clonedFriendlies.ContainsKey(target))
            return 0;

        var snapshot = clonedFriendlies[target];
        int predictedDamage = 0;

        foreach (var pair in clonedFriendlies)
        {
            var attacker = pair.Value;
            if (!attacker.canAttack || attacker.area == null)
                continue;

            if (GridHelper.Instance.IsInRange(attacker.area, snapshot.area, attacker.stats.range))
            {
                predictedDamage = Math.Max(predictedDamage, PredictAttackDamage(attacker, snapshot));
            }
        }

        return predictedDamage;
    }

    public static int PredictSupportDamageToFriendlyAt(Area targetArea, Dictionary<Unit, UnitSnapshot> friendlies, Dictionary<Unit, UnitSnapshot> enemies)
    {
        Unit target = targetArea.occupyingFriendlyUnit;
        if (target == null || !friendlies.ContainsKey(target))
            return 0;

        UnitSnapshot simTarget = friendlies[target];
        int totalSupport = 0;

        foreach (var pair in enemies)
        {
            Unit unit = pair.Key;
            UnitSnapshot sim = pair.Value;

            if (!GridHelper.Instance.IsInRange(sim.area, targetArea, sim.stats.range))
                continue;

            int value = PredictSupportDamage(sim, simTarget);
            totalSupport += value;
        }

        return totalSupport;
    }
}
