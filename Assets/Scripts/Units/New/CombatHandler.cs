using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    private Unit _unit;
    private UnitStats _stats;
    private TraitHandler _traitHandler;
    private StatusEffectHandler _statusHandler;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _stats = GetComponent<UnitStats>();
        _traitHandler = GetComponent<TraitHandler>();
        _statusHandler = GetComponent<StatusEffectHandler>();
    }

    public void DealDamage(Unit target)
    {
        var targetCombat = target.GetComponent<CombatHandler>();
        if (targetCombat == null)
            return;

        int baseDamage = _stats.AttackPower;
        targetCombat.TakeDamage(_unit, baseDamage);
    }

    public void TakeDamage(Unit attacker, int rawDamage)
    {
        int finalDamage = CalculateFinalDamage(rawDamage);

        _statusHandler.OnBeforeTakeDamage(ref finalDamage);

        _stats.CurrentHealth -= finalDamage;

        _statusHandler.OnAfterTakeDamage();
        _traitHandler.OnAfterDamaged();

        if (_stats.CurrentHealth <= 0)
        {
            Kill(attacker);
        }
    }

    private int CalculateFinalDamage(int rawDamage)
    {
        int reduced = Mathf.Max(rawDamage - _stats.DefensePower, 1);
        return reduced;
    }

    private void Kill(Unit killer)
    {
        _traitHandler.OnKill(killer);
        _unit.Kill();
    }
}
