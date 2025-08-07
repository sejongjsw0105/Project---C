using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string UnitName { get; private set; }
    public Faction Faction { get; private set; }
    public UnitType UnitType { get; private set; }

    private UnitStats _stats;
    private TraitHandler _traitHandler;
    private StatusEffectHandler _statusHandler;
    private CombatHandler _combatHandler;

    private void Awake()
    {
        _stats = GetComponent<UnitStats>();
        _traitHandler = GetComponent<TraitHandler>();
        _statusHandler = GetComponent<StatusEffectHandler>();
        _combatHandler = GetComponent<CombatHandler>();

        InitializeUnit();
    }

    private void InitializeUnit()
    {
        // 이 부분은 유닛 생성 방식에 따라 수정 가능
        UnitName = gameObject.name;
        Faction = Faction.Friendly; // 기본값, 설정 필요
        UnitType = UnitType.Melee;  // 기본값, 설정 필요
    }

    public void ApplyDamage(int damage)
    {
        _statusHandler.OnBeforeTakeDamage(ref damage);
        _stats.CurrentHealth -= damage;
        _traitHandler.OnAfterDamaged();
        _statusHandler.OnAfterTakeDamage();
    }

    public void Heal(int amount)
    {
        _stats.CurrentHealth = Mathf.Min(_stats.CurrentHealth + amount, _stats.MaxHealth);
    }

    public void PerformAction(UnitActionType actionType)
    {
        _traitHandler.OnAction(actionType);
        _statusHandler.OnAction(actionType);
    }

    public bool IsAlive()
    {
        return _stats.CurrentHealth > 0;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
