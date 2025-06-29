using System.Linq;

public class AttackContext
{
    public IUnit Attacker { get; private set; }
    public IUnit Target { get; private set; }

    public int BaseDamage { get; private set; }
    public int FinalDamage { get; private set; }

    public bool IsBlocked { get; private set; }
    public string BlockedBy { get; private set; }

    public AttackContext(IUnit attacker, IUnit target)
    {
        Attacker = attacker;
        Target = target;
        BaseDamage = attacker.stats.attackPower;
        FinalDamage = BaseDamage;
    }

    public void Resolve()
    {
        ApplyModifiers();
        if (IsBlocked) return;
        DamageAction.Execute(Attacker, Target, DamageType.Damage, FinalDamage);
        Attacker.state.CanAttack = false;

        ApplyPostAttack();
    }

    private void ApplyModifiers()
    {
        var modifiers = Attacker.unitTrait
            .Concat<BaseActionModifier>(Attacker.statusEffects)
            .Concat(GameContext.Instance.myArtifacts);

        foreach (var mod in modifiers)
        {
            var result = mod.OnBeforeAttack(Attacker, Target, FinalDamage);
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

    private void ApplyPostAttack()
    {
        Attacker.state.CanAttack = false;
        var modifiers = Attacker.unitTrait
            .Concat<BaseActionModifier>(Attacker.statusEffects)
            .Concat(GameContext.Instance.myArtifacts);

        foreach (var mod in modifiers)
        {
            mod.OnAfterAttack(Attacker, Target, FinalDamage);
        }
        GameEvents.OnAttackPerformed.Invoke((Attacker, Target, FinalDamage));
    }
}
