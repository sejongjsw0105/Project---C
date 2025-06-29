// GameEvents.cs
public static class GameEvents
{
    public static GameEvent<IUnit> OnUnitDied = new();
    public static GameEvent<(IUnit unit, IArea to)> OnUnitMoved = new();
    public static GameEvent<(IUnit attacker, IUnit target, int damage)> OnAttackPerformed = new();
    public static GameEvent<(IUnit supporter, IArea area, int value)> OnSupportPerformed = new();
    public static GameEvent<BattleManager.BattleEndReason> OnBattleEnded = new();
}
