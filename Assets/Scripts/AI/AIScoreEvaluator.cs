using System.Linq;

public static class AIScoreEvaluator
{
    public static float ScoreDelta(
        SimWorldSnapshot before,
        SimWorldSnapshot after,
        AIFactorSet factor)
    {
        float score = 0f;

        foreach (var (id, unit) in after.units)
        {
            if (!before.units.ContainsKey(id)) continue;

            var beforeUnit = before.units[id];
            int deltaHp = beforeUnit.currentHealth - unit.currentHealth;
            bool died = beforeUnit.currentHealth > 0 && unit.currentHealth <= 0;

            float priority = unit.unitPriority;
            bool isFriendly = unit.faction == Faction.Friendly;

            if (died)
            {
                score += (isFriendly ? -1 : +1) * priority * factor.Kill;
               
            }
            float friendlyHpWeightedSum = before.units.Values
                .Where(u => u.faction == Faction.Friendly && u.currentHealth > 0)
                .Sum(u => u.currentHealth * u.unitPriority);

            float enemyHpWeightedSum = before.units.Values
                .Where(u => u.faction == Faction.Enemy && u.currentHealth > 0)
                .Sum(u => u.currentHealth * u.unitPriority);
            float baseScore = deltaHp * priority;

            if (unit.faction == Faction.Enemy && enemyHpWeightedSum > 0)
            {
                score += (baseScore / enemyHpWeightedSum) * factor.HPChange;
            }
            else if (unit.faction == Faction.Friendly && friendlyHpWeightedSum > 0)
            {
                score -= (baseScore / friendlyHpWeightedSum) * factor.HPChange;

            }

            foreach (var effect in unit.statusEffects)
            {
                float effScore = effect.scoreBonus * priority;

                int sign = 0;
                if (unit.faction == Faction.Enemy)
                    sign = (effect.effectType == EffectType.Buff ? -1 : +1);
                else if (unit.faction == Faction.Friendly)
                    sign = (effect.effectType == EffectType.Buff ? +1 : -1);

                score += sign * effScore * factor.StatusEffectScore;
            }
        }

        foreach (var area in after.areas)
        {
            var beforeCond = before.areas.First(a => a.areaIndexX == area.areaIndexX && a.areaIndexY == area.areaIndexY).areaCondition;
            var afterCond = area.areaCondition;

            if (beforeCond != afterCond)
            {
                if (afterCond == AreaCondition.InCombat && beforeCond != AreaCondition.InCombat)
                    score += factor.CombatPoint;
                else if (afterCond == AreaCondition.FriendlyOccupied && beforeCond != AreaCondition.FriendlyOccupied)
                    score += factor.CapturePoint;
            }
        }

        return score;
    }
}

