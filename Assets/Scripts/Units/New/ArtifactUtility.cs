public static class ArtifactUtility
{
    public static List<IArtifact> GetArtifacts()
    {
        return GameContext.Instance.GetComponents<IArtifact>().ToList();
    }

    public static void InvokeOnBattleStart()
    {
        foreach (var artifact in GetArtifacts())
            artifact.OnBattleStart();
    }

    public static void InvokeOnTurnStart(Unit unit)
    {
        foreach (var artifact in GetArtifacts())
            artifact.OnTurnStart(unit);
    }

    public static void InvokeOnKill(Unit killer, Unit victim)
    {
        foreach (var artifact in GetArtifacts())
            artifact.OnKill(killer, victim);
    }

    // 등등 필요 시점마다 호출 가능
}
