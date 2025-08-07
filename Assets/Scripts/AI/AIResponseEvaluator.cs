using System.Collections.Generic;
using System.Linq;

public static class AIResponseEvaluator
{

    public static float EvaluateFriendlyResponse(
        SimWorldSnapshot snapshot,
        AIFactorSet factorSet,
        AIDifficulty difficulty)
    {
        // 1. 모든 가능한 전령 기반 행동 시뮬레이션 조합 생성
        var allCombos = GenerateAllValidActionChains(snapshot, GameContext.Instance.maxCommandPoints);

        float bestScore = float.MaxValue;

        foreach (var combo in allCombos)
        {
            // 2. 상태 복제
            var clonedUnits = snapshot.units.Values.Select(u => new SimUnit(u)).ToList();
            var clonedAreas = snapshot.areas.Select(a => new SimArea(a)).ToList();
            var after = SimWorldSnapshot.Capture(clonedUnits.Cast<IUnit>().ToList(), clonedAreas.Cast<IArea>().ToList());

            // 3. 행동 실행
            foreach (var (unitId, plan) in combo)
            {
                if (!after.units.ContainsKey(unitId)) continue;
                var unit = after.units[unitId];
                var area = plan.targetArea;

                switch (plan.action)
                {
                    case Action.Move:
                        if (MoveAction.CanExecute(unit, area))
                            MoveAction.Execute(unit, area);
                        break;
                    case Action.Support:
                        if (SupportAction.CanExecute(unit, area))
                            SupportAction.Execute(unit, area);
                        break;
                    case Action.Defend:
                        if (DefendAction.CanExecute(unit, area))
                            DefendAction.Execute(unit, area);
                        break;
                }
            }

            // 4. 점수 평가 (손실이 가장 적은 경로가 best)
            float score = AIScoreEvaluator.ScoreDelta(snapshot, after, factorSet);
            if (score < bestScore)
                bestScore = score;
        }

        return bestScore;
    }

    private static List<List<(int unitId, SimActionPlan)>> GenerateAllValidActionChains(SimWorldSnapshot snapshot, int commandPoints)
    {
        var result = new List<List<(int, SimActionPlan)>>();
        Explore(snapshot, commandPoints, new List<(int, SimActionPlan)>(), result);
        return result;
    }

    private static void Explore(
        SimWorldSnapshot snapshot,
        int remainingCP,
        List<(int unitId, SimActionPlan)> currentPath,
        List<List<(int, SimActionPlan)>> result)
    {
        if (remainingCP <= 0)
        {
            result.Add(new List<(int, SimActionPlan)>(currentPath));
            return;
        }

        var actions = AIActionExplorer.GetAllAvailableActionsByUnit(snapshot, Faction.Friendly);
        bool found = false;

        foreach (var (unitId, plans) in actions)
        {
            var unit = snapshot.units[unitId];

            foreach (var plan in plans)
            {
                int cost = ActionCostCalculator.GetCost(plan.action);
                if (cost > remainingCP) continue;

                // 상태 복제
                var clonedUnits = snapshot.units.Values.Select(u => new SimUnit(u)).ToList();
                var clonedAreas = snapshot.areas.Select(a => new SimArea(a)).ToList();
                var clonedSnapshot = SimWorldSnapshot.Capture(clonedUnits.Cast<IUnit>().ToList(), clonedAreas.Cast<IArea>().ToList());

                var simUnit = clonedSnapshot.units[unitId];
                var simArea = plan.targetArea;

                switch (plan.action)
                {
                    case Action.Move:
                        if (!MoveAction.CanExecute(simUnit, simArea)) continue;
                        MoveAction.Execute(simUnit, simArea);
                        break;
                    case Action.Support:
                        if (!SupportAction.CanExecute(simUnit, simArea)) continue;
                        SupportAction.Execute(simUnit, simArea);
                        break;
                    case Action.Defend:
                        if (!DefendAction.CanExecute(simUnit, simArea)) continue;
                        DefendAction.Execute(simUnit, simArea);
                        break;
                    default: continue;
                }

                var nextPath = new List<(int, SimActionPlan)>(currentPath) { (unitId, plan) };
                Explore(clonedSnapshot, remainingCP - cost, nextPath, result);
                found = true;
            }
        }

        if (!found && currentPath.Count > 0)
            result.Add(new List<(int, SimActionPlan)>(currentPath));
    }
}
