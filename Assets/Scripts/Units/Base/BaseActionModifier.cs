using UnityEngine;

public abstract class BaseActionModifier : MonoBehaviour
{
    // ========== 전투 훅 (공격/지원/피해) ==========

    public virtual ResultContext<int> OnBeforeAttack(Unit attacker, Unit target, int baseDamage)
        => new ResultContext<int>(baseDamage);

    public virtual void OnAfterAttack(Unit attacker, Unit target, int damage) { }

    public virtual ResultContext<int> OnBeforeSupport(Unit supporter, Area area, int value)
        => new ResultContext<int>(value);

    public virtual void OnAfterSupport(Unit supporter, Area area, int value) { }

    public virtual ResultContext<int> OnBeforeDamaged(Unit from, Unit to, DamageType type, int damage)
        => new ResultContext<int>(damage);

    public virtual void OnAfterDamaged(Unit from, Unit to, DamageType type, int damage) { }

    // ========== 이동/방어 등 행동 관련 훅 ==========

    public virtual ResultContext<int> OnBeforeMove(Unit unit, Area target, int moveRange)
        => new ResultContext<int>(moveRange);

    public virtual void OnAfterMove(Unit unit, Area target) { }

    public virtual int OnBeforeDefend(Unit unit) => 0;
    public virtual void OnAfterDefend(Unit unit) { }
    // ========== 턴 관련 훅 ==========
    public virtual void OnTurnEnd(Unit unit) { }
    public virtual void OnTurnStart(Unit unit) { }
    public virtual void OnApply(Unit unit) { }

    // ========== 생사 관련 훅 ==========
    public virtual void OnBattleStart(Unit unit) { }
    public virtual void OnWin(Unit unit) { }

    public virtual void OnLose(Unit unit) { }

    public virtual void OnDie(Unit unit) { }

    public virtual void OnExpire(Unit unit) { }
}
