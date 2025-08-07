public interface IArtifact
{
    void OnBattleStart();
    void OnTurnStart(Unit unit);
    void OnTurnEnd(Unit unit);
    void OnAfterMove(Unit unit);
    void OnSupport(Unit supporter, Unit target);
    void OnKill(Unit killer, Unit victim);
    void OnDie(Unit dead);
}
