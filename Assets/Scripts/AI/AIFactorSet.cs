public class AIFactorSet
{
    public float Kill = 10f;            // �� ���� óġ
    public float SelfDeath = 15f;       // �ڱ� ��� ���Ƽ
    public float HPChange = 0.2f;       // ü�� ��ȭ (����)
    public float CapturePoint = 5f;     // ���� ��
    public float CombatPoint = 3f;      // ���� ����
    public float SideAttackBonus = 8f;  // ���� ���� ����
    public float SideExposePenalty = 8f;// ���� ���� �г�Ƽ
    public float StatusEffectScore = 1.0f; // �����̻� ���� ���

    // ���� Ÿ�Կ� ���� Preset ������
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
