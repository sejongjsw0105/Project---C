using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;


public static class EnemyAIUtility
{
    public enum ActionType { Support, Attack, Move, Defend }

    public class AIAction
    {
        public ActionType type;
        public Area targetArea;
        public int score;
    }

    public static IEnumerator ExecuteEnemyTurn()
    {
        var enemies = UnitManager.Instance.GetUnitsByFaction(Unit.Faction.Enemy);
        var friendlies = CloneFakeFriendlies(); // 예측용

        foreach (var enemy in enemies)
        {
            yield return new WaitForSeconds(0.3f);

            if (enemy.health <= 0) continue;

            var best = EvaluateBestAction(enemy, friendlies);
            ExecuteAIAction(enemy, best);

            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.5f);
        BattleManager.Instance.SetState(BattleManager.States.TurnStart);
    }

    private static AIAction EvaluateBestAction(Unit enemy, Dictionary<Unit, FakeUnit> friendlies)
    {
        List<AIAction> candidates = new();

        // 1. 지원
        foreach (Area area in AreaManager.Instance.allAreas)
        {
            var support = enemy.GetSupport();
            if (support == null) continue;

            var result = support.BeforeSupport(area);
            int value = result.Item1;
            bool isValid = result.Item2;

            if (!isValid || !support.validFactions.Contains(Unit.Faction.Enemy)) continue;

            int danger = PredictionUtility.PredictSupportDamageToFriendlyAt(area, friendlies, CloneFakeEnemies());
            candidates.Add(new AIAction { type = ActionType.Support, targetArea = area, score = danger });
        }

        // 2. 전투
        foreach (Area area in AreaManager.Instance.allAreas)
        {
            if (!GridHelper.Instance.IsEnemyMoveAllowed(enemy.area, area)) continue;
            if (area.occupyingFriendlyUnit == null) continue;

            var sim = new FakeUnit(enemy);
            int damage = sim.BeforeAttack(area.occupyingFriendlyUnit);

            int score = damage;
            if (damage >= area.occupyingFriendlyUnit.health) score += 100;

            int retaliation = PredictionUtility.PredictIncomingDamageToEnemyAt(area, friendlies);
            if (retaliation >= enemy.health) score -= 999;
            else if (retaliation >= enemy.health / 2) score -= 20;

            candidates.Add(new AIAction { type = ActionType.Attack, targetArea = area, score = score });
        }

        // 3. 이동
        foreach (Area area in AreaManager.Instance.allAreas)
        {
            if (!GridHelper.Instance.IsEnemyMoveAllowed(enemy.area, area)) continue;
            if (area.occupyingEnemyUnit != null) continue;

            int risk = PredictionUtility.PredictIncomingDamageToEnemyAt(area, friendlies);
            int score = -risk / 2;
            candidates.Add(new AIAction { type = ActionType.Move, targetArea = area, score = score });
        }

        // 4. 방어
        candidates.Add(new AIAction { type = ActionType.Defend, targetArea = enemy.area, score = 5 });

        return candidates.OrderByDescending(c => c.score).First();
    }

    private static void ExecuteAIAction(Unit unit, AIAction action)
    {
        switch (action.type)
        {
            case ActionType.Support:
                var support = unit.GetSupport();
                support?.DoSupport(action.targetArea);
                break;

            case ActionType.Attack:
            case ActionType.Move:
                unit.DoMove(action.targetArea);
                break;

            case ActionType.Defend:
                unit.Defense();
                break;
        }
    }

    private static Dictionary<Unit, FakeUnit> CloneFakeFriendlies()
    {
        var result = new Dictionary<Unit, FakeUnit>();
        foreach (var unit in UnitManager.Instance.GetUnitsByFaction(Unit.Faction.Friendly))
            result[unit] = new FakeUnit(unit);
        return result;
    }

    private static Dictionary<Unit, FakeUnit> CloneFakeEnemies()
    {
        var result = new Dictionary<Unit, FakeUnit>();
        foreach (var unit in UnitManager.Instance.GetUnitsByFaction(Unit.Faction.Enemy))
            result[unit] = new FakeUnit(unit);
        return result;
    }
}
