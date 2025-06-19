using UnityEngine;
using System.Collections.Generic;
public enum UnitTraitType
{
    SuccesiveFiring,
    Pressure,
    Reload,
    Fire,
    Fear,
    Cover,
    CounterAttack,
    CloseRangeShooting,
    Sniping,
    PsychologicalWarfare,
    Surprise,
    Shielded,
    Armored,
    Heavy, // ���尩
    // �ʿ信 ���� �߰�
}

public abstract class UnitTrait : MonoBehaviour

{
    public UnitTraitType type;
    public List<Unit.UnitType> unitTypes; 
    public UnitTrait()
    {
    }
    public virtual void OnApply(Unit unit) { }
    public virtual void OnUpdate(Unit from, Area area) { }
    public virtual (int,bool) OnBeforeDamaged(Unit from, Unit target, Unit.DamageType damageType, int damage) { return (damage,true); }
    public virtual void OnAfterDamaged(Unit from, Unit target, Unit.DamageType damageType, int damage) { }
    public virtual (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage) { return  (damage,true); }
    public virtual void OnAfterAttack(Unit attacker, Unit target,int damage) { }
    public virtual (int, bool) OnBeforeSupport(Unit supporter, Area area, int value) { return (value,true) ; }
    public virtual void OnAfterSupport(Unit supporter, Area area, int value) { }
    public virtual (int,bool) OnBeforeMove(Unit unit, Area target, int moveRange) { return (moveRange,true); }
    public virtual void OnAfterMove(Unit unit, Area target)
    {
        // �̵� �� ó���� ������ �ִٸ� ���⿡ �ۼ�
    }
    public virtual void OnDie(Unit unit) { }

}
