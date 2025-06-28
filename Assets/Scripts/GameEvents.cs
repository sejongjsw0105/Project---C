// GameEvents.cs
public static class GameEvents
{
    public static GameEvent<Unit> OnUnitDied = new();
    public static GameEvent<(Unit unit, Area to)> OnUnitMoved = new();
    public static GameEvent<(Unit attacker, Unit target, int damage)> OnAttackPerformed = new();
    public static GameEvent<(Unit supporter, Area area, int value)> OnSupportPerformed = new();
    public static GameEvent<BattleManager.BattleEndReason> OnBattleEnded = new();
}
