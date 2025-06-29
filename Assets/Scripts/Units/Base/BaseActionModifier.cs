using UnityEngine;

public abstract class BaseActionModifier : MonoBehaviour
{
    // ========== 전투 훅 (공격/지원/피해) ==========

    public virtual ResultContext<int> OnBeforeAttack(IUnit attacker, IUnit target, int baseDamage)
        => new ResultContext<int>(baseDamage);

    public virtual void OnAfterAttack(IUnit attacker, IUnit target, int damage) { }

    public virtual ResultContext<int> OnBeforeSupport(IUnit supporter, IArea area, int value)
        => new ResultContext<int>(value);

    public virtual void OnAfterSupport(IUnit supporter, IArea area, int value) { }

    public virtual ResultContext<int> OnBeforeDamaged(IUnit from, IUnit to, DamageType type, int damage)
        => new ResultContext<int>(damage);

    public virtual void OnAfterDamaged(IUnit from, IUnit to, DamageType type, int damage) { }

    // ========== 이동/방어 등 행동 관련 훅 ==========

    public virtual ResultContext<int> OnBeforeMove(IUnit unit, IArea target, int moveRange)
        => new ResultContext<int>(moveRange);

    public virtual void OnAfterMove(IUnit unit, IArea target) { }

    public virtual int OnBeforeDefend(IUnit unit) => 0;
    public virtual void OnAfterDefend(IUnit unit) { }
    // ========== 턴 관련 훅 ==========
    public virtual void OnTurnEnd(IUnit unit) { }
    public virtual void OnTurnStart(IUnit unit) { }
    public virtual void OnApply(IUnit unit) { }

    // ========== 생사 관련 훅 ==========
    public virtual void OnBattleStart(IUnit unit) { }
    public virtual void OnWin(IUnit unit) { }

    public virtual void OnLose(IUnit unit) { }

    public virtual void OnDie(IUnit unit) { }

    public virtual void OnExpire(IUnit unit) { }
}
