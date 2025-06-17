using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public abstract class Unit : MonoBehaviour
{
    public enum DamageType
    {
        Support,
        Damage,
        Debuff,
    }
    public enum Faction
    {
        Friendly,   // 우리 진영  
        Enemy,       // 적 진영  
        Neutral      // 중립 진영  
    }

    public enum UnitType
    {
        Melee,       // 보병  
        Cavalry,        // 기병  
        Ranged,         // 원거리  
        RangedCavalry,   // 원거리 기병  
    }

    public Area area;              // 유닛이 위치한 영역  
    public string unitName;            // 유닛 이름  
    public int unitId;                 // 유닛 ID  
    public int health;                 // 유닛 체력  
    public int attackPower;            // 유닛 공격력  
    public int defensePower;           // 유닛 방어력  
    public bool isAttackable = true; // 유닛이 공격 가능한지 여부
    public bool isMovable = true;   // 유닛이 이동 가능한지 여부
    public bool isSupportable = true; // 유닛이 지원 가능한지 여부
    public Faction faction;           // 유닛의 진영 (예: 아군, 적군)  
    public UnitType unitType;              // 유닛의 종류 (예: 보병, 기병, 원거리)  
    public List<UnitTrait> unitTrait = null;          // 유닛의 특성 (예: 공격력 증가, 방어력 증가 등)

    public List<StatusEffect> statusEffects = new List<StatusEffect>(); // 유닛의 상태 효과 목록  

    private void OnMouseDown()
    {
        UnitSelector.Instance.OnUnitClicked(this);
    }

    void Start()
    {
        UnitManager.Instance.RegisterUnit(this); // 유닛 등록  
    }

    public virtual Support GetSupport()
    {
        return null;
    }

    public virtual void Defense()
    {
        BattleManager.Instance.SpendCommandPoints(BattleManager.Instance.GetActionCost(Action.Defend)); // 방어를 사용하기 위해 명령 포인트 소모
        AddStatusEffect(new Defending(1, defensePower)); // 방어 상태 효과 추가  
    }

    public virtual void Damaged(Unit unit, DamageType damageType, int damage)
    {
        int finalDamage = ApplyBeforeDamageEffects(unit, damageType, damage);
        ApplyDamage(finalDamage);
        ApplyAfterDamageEffects(unit, damageType, finalDamage);

        if (health <= 0)
        {
            Die();

        }
    }
    public virtual int ApplyBeforeMove(Unit unit, Area targetArea)
    {
        int moveRange = 0;
        foreach (var effect in statusEffects)
        {
            moveRange += effect.OnBeforeMove(unit, targetArea);

        }
        foreach (var trait in unitTrait)
        {
            moveRange += trait.OnBeforeMove(unit, targetArea);
        }
        return moveRange;
    }
    public virtual void AfterMove(Unit unit, Area targetArea)
    {
        foreach (var effect in statusEffects)
        {
            effect.OnAfterMove(unit, targetArea);
        }
        foreach (var trait in unitTrait)
        {
            trait.OnAfterMove(unit, targetArea);
        }
        isMovable = false; // 이동 후에는 다시 이동할 수 없도록 설정
    }

    private int ApplyBeforeDamageEffects(Unit unit, DamageType damageType, int damage)
    {
        if (SideAttack(unit, this, damageType, damage))
        {
            this.AddStatusEffect(new SideExposed(1)); // 측면 공격 효과 추가
        }
        foreach (var trait in unitTrait)
        {
            damage = trait.OnBeforeDamage(unit, this, damageType, damage);
        }

        foreach (var effect in statusEffects)
        {
            damage = effect.OnBeforeDamage(unit, this, damageType, damage);
        }



        return damage;
    }

    private void ApplyDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{unitName}이 {damage} 피해를 입었습니다.");
    }
    private bool SideAttack(Unit from, Unit to, DamageType damageType, int damage)
    {
        if (damageType == DamageType.Support && (from.unitType == UnitType.Melee || from.unitType == UnitType.Cavalry) && GridHelper.Instance.isSide(from.area, to.area))
        {
            return true; // 측면 공격이 적용됨
        }
        else
        {
            return false; // 측면 공격이 적용되지 않음
        }
    }

    private void ApplyAfterDamageEffects(Unit unit, DamageType damageType, int damage)
    {
        foreach (var trait in unitTrait)
        {
            trait.OnAfterDamage(unit, this, damageType, damage);
        }
        foreach (var effect in statusEffects)
        {
            effect.OnAfterDamage(unit, this, damageType, damage);
        }

    }

    public virtual void Die()
    {
        switch (faction)
        {
            case Faction.Friendly:
                Debug.Log($"{unitName}이(가) 사망했습니다. 아군 유닛 제거");
                area.occupyingFriendlyUnit = null; // 영역에서 아군 유닛 제거
                break;
            case Faction.Enemy:
                Debug.Log($"{unitName}이(가) 사망했습니다. 적군 유닛 제거");
                area.occupyingEnemyUnit = null; // 영역에서 적군 유닛 제거
                break;
        }
        area.SetAreaCondition();
        UnitManager.Instance.UnregisterUnit(this);
        Destroy(gameObject);
    }

    public virtual void AddStatusEffect(StatusEffect newEffect)
    {
        var existing = statusEffects.Find(e => e.type == newEffect.type);

        if (existing != null)
        {
            HandleExistingStatusEffect(existing, newEffect);
        }
        else
        {
            AddNewStatusEffect(newEffect);
        }
    }

    private void HandleExistingStatusEffect(StatusEffect existing, StatusEffect newEffect)
    {
        switch (newEffect.stackType)
        {
            case StackType.duration:
                existing.duration += newEffect.duration;
                Debug.Log($"[{newEffect.type}] 지속시간이 중첩되었습니다. 총 {existing.duration}턴");
                break;
            case StackType.value:
                existing.value += newEffect.value;
                Debug.Log($"[{newEffect.type}] 효과값이 중첩되었습니다. 총 value: {existing.value}");
                break;
            case StackType.count:
                existing.count += newEffect.count;
                Debug.Log($"[{newEffect.type}] 사용 횟수가 중첩되었습니다. 총 count: {existing.count}");
                break;
            case StackType.none:
                Debug.Log($"[{newEffect.type}] 이미 같은 효과가 존재하므로 무시됨.");
                break;
        }
    }

    private void AddNewStatusEffect(StatusEffect newEffect)
    {
        statusEffects.Add(newEffect);
        newEffect.OnApply(this, null);
    }

    public virtual void UpdateStatusEffects()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            StatusEffect effect = statusEffects[i];
            effect.OnUpdate(this, null);
            effect.duration--;

            if (effect.duration <= 0)
            {
                RemoveExpiredEffect(effect, i);
            }
        }
    }

    private void RemoveExpiredEffect(StatusEffect effect, int index)
    {
        effect.OnExpire(this, null);
        statusEffects.RemoveAt(index);
    }

    public int PrepareAttack(Unit target, int damage)
    {
        int bonusDamage = 0;
        foreach (var trait in unitTrait)
        {
            bonusDamage += trait.OnBeforeAttack(this, target, damage);
        }
        foreach (var effect in statusEffects)
        {
            bonusDamage += effect.OnBeforeAttack(this, target, damage);
        }
        return damage + bonusDamage;

    }
    public void AfterAttack(Unit target, int damage)
    {
        foreach (var trait in unitTrait)
        {
            trait.OnAfterAttack(this, target, damage);
        }
        foreach (var effect in statusEffects)
        {
            effect.OnAfterAttack(this, target, damage);
        }
    }
    public int PrepareSupport(Area to)
    {
        int bonus = 0;
        Support support = GetSupport();
        foreach (var effect in statusEffects)
        {
            bonus += effect.OnBeforeSupport(this, to);
        }
        foreach (var trait in unitTrait)
        {
            bonus += trait.OnBeforeSupport(this, to);
        }
        isSupportable = isSupportable || support.areacond.Contains(area.areaCondition) && GridHelper.Instance.IsInRange(area, to, support.supportRange);
        BattleManager.Instance.SpendCommandPoints(BattleManager.Instance.GetActionCost(Action.Support)); // 지원을 사용하기 위해 명령 포인트 소모

        return support.supportAmount + bonus; // 지원 효과에 보너스 추가
    }


    public class Support
    {
        public Unit owner;
        public List<AreaCondition> areacond;
        public List<Unit.Faction> factions = new List<Unit.Faction> { Unit.Faction.Friendly, Unit.Faction.Enemy };
        public int supportAmount;
        public int supportRange;

        public Support(Unit owner, List<AreaCondition> areacond, List<Unit.Faction> factions, int supportAmount, int supportRange)
        {
            this.owner = owner;
            this.areacond = areacond;
            this.factions = factions;
            this.supportAmount = supportAmount;
            this.supportRange = supportRange;
        }
        public void BeforeSupport(Area area)
        {
            owner.PrepareSupport(area); // 지원 준비 단계에서 호출
            if (!owner.isSupportable)
            {
                return;
            }
        }

        public virtual void DoSupport(Area area)
        {
            BeforeSupport(area); // 지원 전 준비 단계 호출
        }
        public virtual void AfterSupport(Area area)
        {
            foreach (var effect in owner.statusEffects)
            {
                effect.OnAfterSupport(owner, area); // 상태 효과의 지원 후 처리 호출
            }
            foreach (var trait in owner.unitTrait)
            {
                trait.OnAfterSupport(owner, area); // 유닛 특성의 지원 후 처리 호출
            }
            owner.isSupportable = false; // 지원 후에는 다시 지원할 수 없도록 설정
            if (owner.unitType == UnitType.Cavalry || owner.unitType == UnitType.RangedCavalry)
            {
                owner.AddStatusEffect(new SideExposed(1));
            }

        }
    }
}



