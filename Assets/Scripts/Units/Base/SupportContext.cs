using System.Linq;

public class SupportContext
{
    public Unit Supporter { get; private set; }
    public Area TargetArea { get; private set; }

    public int BaseValue { get; private set; }
    public int FinalValue { get; private set; }

    public bool IsBlocked { get; private set; }
    public string BlockedBy { get; private set; }

    public SupportContext(Unit supporter, Area target)
    {
        Supporter = supporter;
        TargetArea = target;
        BaseValue = supporter.stats.attackPower;
        FinalValue = BaseValue;
    }

    public void Resolve()
    {
        ApplyModifiers();
        if (IsBlocked) return;

        ApplySupport();
        Supporter.state.CanSupport = false;

        ApplyPostSupport();
    }

    private void ApplyModifiers()
    {
        var modifiers = Supporter.unitTrait
            .Concat<BaseActionModifier>(Supporter.statusEffects)
            .Concat(GameContext.Instance.myArtifacts);

        foreach (var mod in modifiers)
        {
            var result = mod.OnBeforeSupport(Supporter, TargetArea, FinalValue);
            if (result.IsBlocked)
            {
                IsBlocked = true;
                BlockedBy = result.Source ?? mod.GetType().Name;
                return;
            }
            if (result.IsModified)
                FinalValue = result.Value;
        }
    }

    private void ApplySupport()
    {
        var target = TargetArea.occupyingEnemyUnit ?? TargetArea.occupyingFriendlyUnit;
        if (target != null)
        {
            DamageAction.Execute(Supporter, target, DamageType.Support, FinalValue);
        }
    }

    private void ApplyPostSupport()
    {
        Supporter.state.CanSupport = false;
        var modifiers = Supporter.unitTrait
            .Concat<BaseActionModifier>(Supporter.statusEffects)
            .Concat(GameContext.Instance.myArtifacts);

        foreach (var mod in modifiers)
        {
            mod.OnAfterSupport(Supporter, TargetArea, FinalValue);
        }
    }
}
