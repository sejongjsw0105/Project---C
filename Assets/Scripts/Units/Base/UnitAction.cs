public static class DamageAction
{
    public static bool CanExecute(IUnit unit, IArea target)
    {
        return unit.state.CanDamaged;
    }

    public static void Execute(IUnit attacker, IUnit target, DamageType type, int damage)
    {
        if (!CanExecute(attacker, target.area)) return;

        var context = new DamageContext(attacker, target, type, damage);
        context.Resolve();
    }
}

public static class SupportAction
{
    public static bool CanExecute(IUnit unit, IArea target)
    {
        if (!unit.state.CanSupport)
            return false;

        if (!GridHelper.Instance.IsInRange(unit.area, target, unit.stats.range))
            return false;

        if (unit.supportableFaction == Faction.Neutral || unit.supportableFaction == Faction.All)
            return true;

        IUnit targetUnit = unit.supportableFaction switch
        {
            Faction.Friendly => target.occupyingFriendlyUnit,
            Faction.Enemy => target.occupyingEnemyUnit,
            Faction.Other => target.GetEnemyOccupant(unit),
            Faction.Same => target.GetAllyOccupant(unit),
            _ => null
        };

        return targetUnit != null;
    }

    public static void Execute(IUnit unit, IArea target)
    {
        if (!CanExecute(unit, target)) return;

        var context = new SupportContext(unit, target);
        context.Resolve();
    }
}

public static class DefendAction
{
    public static bool CanExecute(IUnit unit, IArea _)
    {
        return unit.state.CanDefend;
    }

    public static void Execute(IUnit unit, IArea target)
    {
        if (!CanExecute(unit, target)) return;

        var context = new DefendContext(unit);
        context.Resolve();
    }
}

public static class MoveAction
{
    public static bool CanExecute(IUnit unit, IArea target)
    {
        return unit.state.CanMove && GridHelper.Instance.IsMoveAllowed(unit, target);
    }

    public static void Execute(IUnit unit, IArea target)
    {
        if (!CanExecute(unit, target)) return;

        var context = new MoveContext(unit, target);
        context.Resolve();
    }
}

public static class AttackAction
{
    public static bool CanExecute(IUnit unit, IArea target)
    {
        return unit.state.CanAttack && target.GetEnemyOccupant(unit) != null;
    }

    public static void Execute(IUnit unit, IArea target)
    {
        if (!CanExecute(unit, target)) return;

        var context = new AttackContext(unit, target.GetEnemyOccupant(unit));
        context.Resolve();
    }
}