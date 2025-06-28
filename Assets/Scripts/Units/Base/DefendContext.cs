using System.Linq;

public class DefendContext
{
    public Unit Unit { get; private set; }

    public int DefenseValue { get; private set; }

    public DefendContext(Unit unit)
    {
        Unit = unit;
        DefenseValue = unit.defensePower;
    }

    public void Resolve()
    {
        ApplyModifiers();
        Unit.AddStatusEffect(new Defending(1, DefenseValue));

        Unit.state.CanAttack = false;
        Unit.state.CanMove = false;
        Unit.state.CanDefend = false;

        ApplyPostDefend();
    }

    private void ApplyModifiers()
    {
        var modifiers = Unit.unitTrait
            .Concat<BaseActionModifier>(Unit.statusEffects)
            .Concat(GameContext.Instance.myArtifacts);

        foreach (var mod in modifiers)
        {
            DefenseValue += mod.OnBeforeDefend(Unit);
        }
    }

    private void ApplyPostDefend()
    {
        var modifiers = Unit.unitTrait
            .Concat<BaseActionModifier>(Unit.statusEffects)
            .Concat(GameContext.Instance.myArtifacts);

        foreach (var mod in modifiers)
        {
            mod.OnAfterDefend(Unit);
        }
    }
}
