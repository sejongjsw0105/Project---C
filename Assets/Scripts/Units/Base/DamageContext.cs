using System.Linq;

public class DamageContext
{
    public Unit Attacker { get; private set; }
    public Unit Target { get; private set; }
    public DamageType Type { get; private set; }

    public int BaseDamage { get; private set; }
    public int FinalDamage { get; private set; }

    public bool IsBlocked { get; private set; } = false;
    public string BlockedBy { get; private set; } = null;

    public DamageContext(Unit attacker, Unit target, DamageType type, int baseDamage)
    {
        Attacker = attacker;
        Target = target;
        Type = type;
        BaseDamage = baseDamage;
        FinalDamage = baseDamage;
    }

    public void Resolve()
    {
        ApplyModifiers();
        if (IsBlocked) return;

        Target.currentHealth -= FinalDamage;

        if (Target.currentHealth <= 0)
            Target.Die();

        ApplyPostDamage();
    }

    private void ApplyModifiers()
    {
        // 측면 공격 등 특수 상황 처리
        if (Type == Unit.DamageType.Support &&
            (Attacker.unitType == Unit.UnitType.Melee || Attacker.unitType == Unit.UnitType.Cavalry) &&
            GridHelper.Instance.isSide(Attacker.area, Target.area))
        {
            Target.AddStatusEffect(new SideExposed(1));
        }

        var modifiers = Target.unitTrait
            .Concat<BaseActionModifier>(Target.statusEffects)
            .Concat(GameContext.Instance.myArtifacts);

        foreach (var mod in modifiers)
        {
            var result = mod.OnBeforeDamaged(Attacker, Target, Type, FinalDamage);
            if (result.IsBlocked)
            {
                IsBlocked = true;
                BlockedBy = result.Source ?? mod.GetType().Name;
                return;
            }
            if (result.IsModified)
                FinalDamage = result.Value;
        }
    }

    private void ApplyPostDamage()
    {
        var modifiers = Target.unitTrait
            .Concat<BaseActionModifier>(Target.statusEffects)
            .Concat(GameContext.Instance.myArtifacts);

        foreach (var mod in modifiers)
        {
            mod.OnAfterDamaged(Attacker, Target, Type, FinalDamage);
        }
    }
}
