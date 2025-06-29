using System.Collections.Generic;

public static class AITurnDecider
{
    public static List<(int unitId, SimActionPlan)> DecideBestEnemyAction(
        SimWorldSnapshot beforeState,
        AIFactorSet enemyFactor,
        AIFactorSet friendlyFactor,
        AIDifficulty difficulty)
    {
        // 1. 적 유닛 행동 탐색
        var enemyActionsByUnit = AIActionExplorer.GetAllAvailableActionsByUnit(beforeState, Faction.Enemy);

        // 2. 가능한 모든 조합 생성
        var allCombos = AICombinationGenerator.EnumerateAllCombos(enemyActionsByUnit);

        float bestScore = 0.0f;
        List<(int unitId, SimActionPlan)> bestCombo = null;

        foreach (var combo in allCombos)
        {
            float score = AIEnemyEvaluator.EvaluateEnemyComboWithFriendlyResponse(
                combo, beforeState, enemyFactor, friendlyFactor, difficulty);

            if (score > bestScore)
            {
                bestScore = score;
                bestCombo = combo;
            }
        }

        return bestCombo;
    }
}