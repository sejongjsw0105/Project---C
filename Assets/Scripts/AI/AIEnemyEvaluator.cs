using System.Collections.Generic;
using System.Linq;
public static class AIEnemyEvaluator
{
    public static float EvaluateEnemyComboWithFriendlyResponse(
        List<(int unitId, SimActionPlan)> combo,
        SimWorldSnapshot beforeState,
        AIFactorSet factorSetEnemy,
        AIFactorSet factorSetFriendly,
        AIDifficulty difficulty)
    {
        // 1. 상태 복제
        var clonedUnits = beforeState.units.Values.Select(u => new SimUnit(u)).ToList();
        var clonedAreas = beforeState.areas.Select(a => new SimArea(a)).ToList();
        var afterState = SimWorldSnapshot.Capture(clonedUnits.Cast<IUnit>().ToList(), clonedAreas.Cast<IArea>().ToList());

        // 2. 행동 실행
        foreach (var (unitId, plan) in combo)
        {
            var unit = afterState.units[unitId];
            var area = plan.targetArea;

            switch (plan.action)
            {
                case Action.Move: MoveAction.Execute(unit, area); break;
                case Action.Support: SupportAction.Execute(unit, area); break;
                case Action.Defend: DefendAction.Execute(unit, area); break;
            }
        }

        // 3. Enemy 점수 평가
        float enemyScore = AIScoreEvaluator.ScoreDelta(beforeState, afterState, factorSetEnemy);

        // 4. Friendly 대응 평가
        float friendlyBest = AIResponseEvaluator.EvaluateFriendlyResponse(afterState, factorSetFriendly, difficulty);

        // 5. 최종 점수 = 내가 얻은 점수 - 상대 최적 대응 점수
        return enemyScore - friendlyBest;
    }
}
