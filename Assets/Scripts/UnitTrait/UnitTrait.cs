using UnityEngine;
using System.Collections.Generic;
using static Unit;
public enum UnitTraitType
{
    Stunning,
    Reload,
    Pressure,
    Shielding,
    Armor,
    Fire,
    FastAttack,
    DoubleAttack,

    // 필요에 따라 추가
}

public abstract class UnitTrait : MonoBehaviour

{
    public UnitTraitType type;
    public List<Unit.UnitType> unitTypes; 
    public int TraitId; 
    public UnitTrait()
    {

    }
    public virtual void OnUpdate(Unit from, Area area) { }
    public virtual int OnBeforeDamage(Unit from, Unit target, Unit.DamageType damageType, int damage) => damage;
    public virtual void OnAfterDamage(Unit from, Unit target, Unit.DamageType damageType, int damage) { }
    public virtual int OnBeforeAttack(Unit attacker, Unit target, int damage) { return  damage; }
    public virtual void OnAfterAttack(Unit attacker, Unit target,int damage) { }
    public virtual int OnBeforeSupport(Unit supporter, Area area) { return 0; }
    public virtual void OnAfterSupport(Unit supporter, Area area) { }
    public virtual int OnBeforeMove(Unit from, Area area) { return 0; }
    public virtual void OnAfterMove(Unit unit, Area target)
    {
        // 이동 후 처리할 로직이 있다면 여기에 작성
    }

}
