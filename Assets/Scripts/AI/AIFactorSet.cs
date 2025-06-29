public class AIFactorSet
{
    public float Kill = 10f;            // 적 유닛 처치
    public float SelfDeath = 15f;       // 자기 사망 페널티
    public float HPChange = 0.2f;       // 체력 변화 (가감)
    public float CapturePoint = 5f;     // 점령 시
    public float CombatPoint = 3f;      // 전투 유도
    public float SideAttackBonus = 8f;  // 측면 공격 유도
    public float SideExposePenalty = 8f;// 측면 노출 패널티
    public float StatusEffectScore = 1.0f; // 상태이상 점수 계수

    // 팩터 타입에 따른 Preset 생성기
    public static AIFactorSet GetPreset(AIStyle style)
    {
        var factor = new AIFactorSet();

        switch (style)
        {
            case AIStyle.Aggressive:
                factor.Kill *= 1.5f;
                factor.SelfDeath *= 0.5f;
                factor.CombatPoint *= 1.5f;
                break;

            case AIStyle.Defensive:
                factor.Kill *= 0.8f;
                factor.SelfDeath *= 1.5f;
                factor.HPChange *= 1.2f;
                break;

            case AIStyle.Balanced:
                break;

            case AIStyle.CaptureFocused:
                factor.CapturePoint *= 2.0f;
                factor.CombatPoint *= 1.5f;
                factor.Kill *= 0.7f;
                break;

            case AIStyle.UnitFocused:
                factor.Kill *= 1.5f;
                factor.HPChange *= 1.5f;
                factor.CapturePoint *= 0.5f;
                break;
        }

        return factor;
    }
}
