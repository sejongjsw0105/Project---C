public static class DamageAction
{
    public static bool CanExecute(Unit unit, Area target)
    {
        return unit.state.CanDamaged;
    }

    public static void Execute(Unit attacker, Unit target, DamageType type, int damage)
    {
        var context = new DamageContext(attacker, target, type, damage);
        context.Resolve();
    }
}
public static class SupportAction 
{
    public static bool CanExecute(Unit unit, Area target)
    {
        return unit.state.CanSupport && GridHelper.Instance.IsInRange(unit.area, target, unit.stats.range);
    }

    public static void Execute(Unit unit, Area target)
    {
        var context = new SupportContext(unit, target);
        context.Resolve();
    }
}

public static class DefendAction
{
    public static bool CanExecute(Unit unit, Area _)
    {
        return unit.state.CanDefend;
    }

    public static void Execute(Unit unit, Area _)
    {
        var context = new DefendContext(unit);
        context.Resolve();
    }
}
public static class MoveAction
{
    public static bool CanExecute(Unit unit, Area target)
    {
        return unit.state.CanMove && GridHelper.Instance.IsFriendlyMoveAllowed(unit.area, target);
    }

    public static void Execute(Unit unit, Area target)
    {
        var context = new MoveContext(unit, target);
        context.Resolve();
    }
}
public static class AttackAction
{
    public static bool CanExecute(Unit unit, Area target)
    {
        return unit.state.CanAttack && target.GetEnemyOccupant(unit) != null;
    }

    public static void Execute(Unit unit, Area target)
    {
        var context = new AttackContext(unit, target.GetEnemyOccupant(unit));
        context.Resolve();
    }
}
