using System.Linq;

public class MoveContext
{
    public IUnit Unit { get; private set; }
    public IArea From { get; private set; }
    public IArea To { get; private set; }

    public int BaseMoveRange { get; private set; }
    public int FinalMoveRange { get; private set; }

    public bool IsBlocked { get; private set; } = false;
    public string BlockedBy { get; private set; } = null;

    public MoveContext(IUnit unit, IArea to, int baseMoveRange = 1)
    {
        Unit = unit;
        From = unit.area;
        To = to;
        BaseMoveRange = baseMoveRange;
        FinalMoveRange = baseMoveRange;
    }

    public void Resolve()
    {
        if (!Unit.state.CanMove)
        {
        }

        ApplyModifiers();
        if (IsBlocked)
        {
        }

        DoMove();
        ApplyPostMove();
    }

    private void ApplyModifiers()
    {
        var modifiers = Unit.unitTrait
            .Concat<BaseActionModifier>(Unit.statusEffects)
            .Concat(GameContext.Instance.myArtifacts);

        foreach (var mod in modifiers)
        {
            var result = mod.OnBeforeMove(Unit, To, FinalMoveRange);
            if (result.IsBlocked)
            {
                IsBlocked = true;
                BlockedBy = result.Source ?? mod.GetType().Name;
                return;
            }

            if (result.IsModified)
                FinalMoveRange = result.Value;
        }
    }

    private void DoMove()
    {
        From.occupyingFriendlyUnit = null;
        if (Unit is Unit u && To is Area a)
        {
            u.transform.position = a.transform.position;
        }
        Unit.area = To;
        To.occupyingFriendlyUnit = Unit;
        To.UpdateAreaCondition();

        Unit.state.CanMove = false;
        GameEvents.OnUnitMoved.Invoke((Unit, To));

    }

    private void ApplyPostMove()
    {
        Unit.state.CanMove = false;
        var modifiers = Unit.unitTrait
            .Concat<BaseActionModifier>(Unit.statusEffects)
            .Concat(GameContext.Instance.myArtifacts);

        foreach (var mod in modifiers)
        {
            mod.OnAfterMove(Unit, To);
        }
        
    }
}
