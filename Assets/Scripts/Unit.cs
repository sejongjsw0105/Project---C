using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Unit;
using static UnityEngine.GraphicsBuffer;
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
    public class UnitStats
    {
        public int maxHealth;
        public int attackPower;
        public int defensePower;
        public int range;
    }
    public bool isUpgraded = false; // 유닛 업그레이드 여부
    public UnitStats stats; // 영구적인 유닛 정보 (체력, 공격력, 방어력 등)
    public UnitStats upgradedStats;
    public Area area;              // 유닛이 위치한 영역  
    public string unitName;            // 유닛 이름  
    public int unitId;                 // 유닛 ID  
    public int health;                 // 유닛 체력  
    public int attackPower;            // 유닛 공격력  
    public int defensePower;           // 유닛 방어력  
    public int range;                // 유닛 공격 사거리
    public bool isAttackable = true; // 유닛이 공격 가능한지 여부
    public bool isMovable = true;   // 유닛이 이동 가능한지 여부
    public bool isSupportable = true; // 유닛이 지원 가능한지 여부
    public bool isDamagedable = true; // 유닛이 피해를 입을 수 있는지 여부
    public bool isDefendable = false; // 유닛이 방어 중인지 여부
    public Faction faction;           // 유닛의 진영 (예: 아군, 적군)  
    public UnitType unitType;              // 유닛의 종류 (예: 보병, 기병, 원거리)  
    public List<UnitTrait> unitTrait;          // 유닛의 특성 (예: 공격력 증가, 방어력 증가 등)
    public List<StatusEffect> statusEffects = new List<StatusEffect>(); // 유닛의 상태 효과 목록  
    public void Start()
    {
        UnitManager.Instance.RegisterUnit(this); // 유닛 등록  
    }
    private void OnMouseDown()
    {
        BattleManager.Instance.HandleUnitClick(this); // 유닛 클릭 시 BattleManager에 전달
    }

    public void BeginBattle()
    {
        if (unitType == UnitType.Ranged)
        {
            isAttackable = false; // 원거리 유닛은 기본적으로 공격 불가능
        }
        foreach (var trait in unitTrait)
        {
            trait.OnApply(this);
        }
        foreach (var effect in statusEffects)
        {
            effect.OnApply(this, null);
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            artifact.OnApply(this, null); // 아티팩트 적용
        }
    }
    public void BeforeTurnStart()
    {
 
        isMovable = true; // 턴 시작 시 이동 가능하도록 설정
        if (GetSupport() != null)
        {
            isSupportable = true; // 턴 시작 시 지원 가능하도록 설정
        }
        isAttackable = true; // 턴 시작 시 공격 가능하도록 설정
        isDefendable = true; // 턴 시작 시 방어 가능하도록 설정
        
        foreach (var effect in statusEffects)
        {
            effect.OnTurnStart(this, area); // 상태 효과 턴 시작 처리
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            artifact.OnTurnStart(this, area); // 아티팩트 턴 시작 처리
        }
        foreach (var trait in unitTrait)
        {
            trait.OnTurnStart(this); // 유닛 특성 업데이트
        }

    }
    public void AfterTurnEnd()
    {
        foreach (var trait in unitTrait)
        {
            trait.OnUpdate(this); // 유닛 특성 턴 종료 처리
        }
        foreach (var effect in statusEffects)
        {
            UpdateStatusEffects();
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            artifact.OnUpdate(this, null); // 아티팩트 턴 종료 처리
        }
    }

    public virtual Support GetSupport()
    {
        return null;
    }

    public virtual void Defense()
    {
        AddStatusEffect(new Defending(1, defensePower)); // 방어 상태 효과 추가  
        isAttackable = false; // 방어 상태에서는 공격 불가능
        isMovable = false; // 방어 상태에서는 이동 불가능
        isDefendable = false; // 방어 상태에서는 방어 가능

    }

    public virtual void Damaged(Unit unit, DamageType damageType, int damage)
    {
        var (finalDamage,isDamagedable) = BeforeDamaged(unit, damageType, damage);
        if (!isDamagedable) return;
        health -= finalDamage;
        AfterDamaged(unit, damageType, finalDamage);
        if (health <= 0)
        {
            Die();

        }
    }
    public virtual int BeforeMove(Unit unit, Area target)
    {
        int moveRange = 1;
        foreach (var trait in statusEffects)
        {
            var (v, flag) = trait.OnBeforeMove(this, target, moveRange);
            moveRange = v;
            isMovable = flag || isMovable;

        }
        foreach (var effect in statusEffects)
        {
            var (v, flag) = effect.OnBeforeMove(this, target, moveRange);
            moveRange = v;
            isMovable = flag || isMovable;
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            var (v, flag) = artifact.OnBeforeMove(this, target, moveRange);
            moveRange = v;
            isMovable = flag || isMovable;
        }
        return moveRange;
    }
    public virtual void AfterMove(Unit unit, Area targetArea)
    {
        isMovable = false;
        foreach (var effect in statusEffects)
        {
            effect.OnAfterMove(unit, targetArea);
        }
        foreach (var trait in unitTrait)
        {
            trait.OnAfterMove(unit, targetArea);
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            artifact.OnAfterMove(unit, targetArea);
        }
    }

    public (int,bool) BeforeDamaged(Unit unit, DamageType damageType, int damage)
    {
        
        if (SideAttack(unit, this, damageType, damage))
        {
            AddStatusEffect(new SideExposed(1)); // 측면 공격 효과 추가
        }
        foreach (var trait in unitTrait)
        {
            var (v, flag) = trait.OnBeforeDamaged(unit, this, damageType, damage);
            damage = v;
            isDamagedable = flag || isDamagedable;
        }

        foreach (var effect in statusEffects)
        {
            var (v, flag) = effect.OnBeforeDamaged(unit, this, damageType, damage);
            damage = v;
            isDamagedable = flag || isDamagedable;
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            var (v, flag) = artifact.OnBeforeDamaged(unit, this, damageType, damage);
            damage = v;
            isDamagedable = flag || isDamagedable;
        }

        return (damage,isDamagedable);
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

    public void AfterDamaged(Unit unit, DamageType damageType, int damage)
    {
        foreach (var trait in unitTrait)
        {
            trait.OnAfterDamaged(unit, this, damageType, damage);
        }
        foreach (var effect in statusEffects)
        {
            effect.OnAfterDamaged(unit, this, damageType, damage);
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            artifact.OnAfterDamaged(unit, this, damageType, damage);
        }
    }
    public virtual void Win()
    {
        foreach (var trait in unitTrait)
        {
            trait.OnWin(this); // 승리 처리
        }
        foreach (var effect in statusEffects)
        {
            effect.OnWin(this);
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            artifact.OnWin(this); // 아티팩트 승리 처리
        }
    }
    public virtual void Lose()
    {
        foreach (var trait in unitTrait)
        {
            trait.OnLose(this); // 패배 처리
        }
        foreach (var effect in statusEffects)
        {
            effect.OnLose(this);
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            artifact.OnLose(this); // 아티팩트 패배 처리
        }
    }
    public virtual void Die()
    {
        switch (faction)
        {
            case Faction.Friendly:
                Debug.Log($"{unitName}이(가) 사망했습니다. 아군 유닛 제거");
                area.occupyingFriendlyUnit = null; // 영역에서 아군 유닛 제거
                RemoveUnitData(); // 유닛 데이터 제거
                break;
            case Faction.Enemy:
                Debug.Log($"{unitName}이(가) 사망했습니다. 적군 유닛 제거");
                area.occupyingEnemyUnit = null; // 영역에서 적군 유닛 제거
                break;
        }
        area.UpdateAreaCondition();
        UnitManager.Instance.UnregisterUnit(this);
        foreach (var effect in statusEffects)
        {
            effect.OnDie(this); // 상태 효과 만료 처리
        }
        foreach (var trait in unitTrait)
        {
            trait.OnDie(this); // 유닛 특성 처리
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            artifact.OnDie(this); // 아티팩트 처리
        }
        Destroy(gameObject);
    }

    public virtual void AddStatusEffect(StatusEffect newEffect)
    {
        newEffect.OnApply(this, null); // 상태 효과 적용
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

    public void HandleExistingStatusEffect(StatusEffect existing, StatusEffect newEffect)
    {
        switch (newEffect.stackType)
        {
            case StackType.duration:
                existing.duration += newEffect.duration;
                statusEffects.Remove(existing); // 기존 효과를 제거하여 중복 방지
                AddNewStatusEffect(existing); // 새로운 효과를 추가하여 로그 출력                
                Debug.Log($"[{newEffect.type}] 지속시간이 중첩되었습니다. 총 {existing.duration}턴");
                break;
            case StackType.value:
                existing.value += newEffect.value;
                statusEffects.Remove(existing); // 기존 효과를 제거하여 중복 방지
                AddNewStatusEffect(existing); // 새로운 효과를 추가하여 로그 출력 
                Debug.Log($"[{newEffect.type}] 효과값이 중첩되었습니다. 총 value: {existing.value}");
                break;
            case StackType.count:
                existing.count += newEffect.count;
                statusEffects.Remove(existing); // 기존 효과를 제거하여 중복 방지
                AddNewStatusEffect(existing); // 새로운 효과를 추가하여 로그 출력 
                Debug.Log($"[{newEffect.type}] 사용 횟수가 중첩되었습니다. 총 count: {existing.count}");
                break;
            case StackType.none:
                break;
        }
    }
    public void DoMove(Area target)
    {
        if (!isMovable) return; // 유닛이 이동할 수 없는 상태라면 아무 작업도 하지 않음
        int moveRange = BeforeMove(this, target); // 이동 전 처리
        switch (faction)
        {
            case Faction.Friendly:
                if (!isMovable || !GridHelper.Instance.IsFriendlyMoveAllowed(area, target, moveRange)) return;
                area.occupyingFriendlyUnit = null;
                transform.position = target.transform.position;
                area = target;
                if (target.occupyingEnemyUnit != null)
                {
                    target.firstAttacker = this;
                    target.secondAttacker = target.occupyingEnemyUnit;
                }
                target.occupyingFriendlyUnit = this;
                target.UpdateAreaCondition();
                BattleManager.Instance.CancelSelection(); 
                BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
                break;
            case Faction.Enemy:
                if (!isMovable || !GridHelper.Instance.IsEnemyMoveAllowed(area, target, moveRange)) return;
                area.occupyingEnemyUnit = null;
                transform.position = target.transform.position;
                area = target;
                if (target.occupyingEnemyUnit != null)
                {
                    target.firstAttacker = this;
                    target.secondAttacker = target.occupyingFriendlyUnit;
                }
                target.occupyingFriendlyUnit = this;
                target.UpdateAreaCondition();
                break;
        }
        AfterMove(this, target);

    }

    private void AddNewStatusEffect(StatusEffect newEffect)
    {
        statusEffects.Add(newEffect);
    }

    public void UpdateStatusEffects()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            var effect = statusEffects[i];
            effect.OnUpdate(this, null); // 상태 효과 업데이트
            if (effect.duration > 0)
                effect.duration--;

            if (effect.duration <= 0)
                RemoveExpiredEffect(effect); // index 없이 제거
        }
    }
    public void RemoveExpiredEffect(StatusEffect effect)
    {
        effect.OnExpire(this, null);
        statusEffects.Remove(effect);
    }

    public int BeforeAttack(Unit target)
    {
        int damage = attackPower;
        foreach (var trait in unitTrait)
        {
            var (v, flag) = trait.OnBeforeAttack(this, target, damage);
            damage = v;
            isAttackable = flag || isAttackable;
        }
        foreach (var effect in statusEffects)
        {
            var (v, flag) = effect.OnBeforeAttack(this, target, damage);
            damage = v;
            isAttackable = flag || isAttackable;
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            var (v, flag) = artifact.OnBeforeAttack(this, target, damage);
            damage = v;
            isAttackable = flag || isAttackable;
        }
        return damage;

    }

    public virtual void DoAttack(Unit target)
    {
        int damage = BeforeAttack(target);
        Damaged(target, Unit.DamageType.Damage, damage);
        AfterAttack(target, damage);
    }
    public void AfterAttack(Unit target, int damage)
    {
        isAttackable = false;
        foreach (var trait in unitTrait)
        {
            trait.OnAfterAttack(this, target, damage);
        }
        foreach (var effect in statusEffects)
        {
            effect.OnAfterAttack(this, target, damage);
        }
        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            artifact.OnAfterAttack(this, target, damage);
        }
        // 공격 후에는 다시 공격할 수 없도록 설정
    }
    private void RemoveUnitData()
    {
        var dataList = GameContext.Instance.myUnitDataList;
        var toRemove = dataList.FirstOrDefault(d => d.id == unitId);

        if (toRemove != null)
        {
            dataList.Remove(toRemove);
            Debug.Log($"[Unit] {unitName}의 UnitData도 삭제됨 (id={unitId})");
        }
    }
    public UnitData ToUnitData()
    {
        var data = new UnitData
        {
            id = unitId,
            name = unitName,
            faction = faction.ToString(),
            type = unitType.ToString(),
            maxHealth = stats.maxHealth,
            currentHealth = health,
            attack = stats.attackPower,
            defense = stats.defensePower,
            range = stats.range,
            traits = unitTrait.Select(t => t.type.ToString()).ToArray(),
            isUpgraded = isUpgraded       };

        if (isUpgraded)
        {
            data.maxHealth = upgradedStats.maxHealth;
            data.attack = upgradedStats.attackPower;
            data.defense = upgradedStats.defensePower;
            data.range = upgradedStats.range;
        }

        return data;
    }
    public class Support
    {
        public Unit owner;
        public List<AreaCondition> validConditions;
        public List<Unit.Faction> validFactions;
        public int baseValue;
        public int range;

        public Support(Unit owner, List<AreaCondition> validConditions, List<Unit.Faction> validFactions, int baseValue, int range)
        {
            this.owner = owner;
            this.validConditions = validConditions;
            this.validFactions = validFactions;
            baseValue = owner.attackPower;
            range = owner.range;
        }

        public (int value, bool isValid) BeforeSupport(Area target)
        {
            int value = baseValue;
            bool isSupportable = owner.isSupportable || GridHelper.Instance.IsInRange(owner.area, target, range) && validConditions.Contains(target.areaCondition) || validFactions.Contains(target.occupyingEnemyUnit.faction) || validFactions.Contains(target.occupyingFriendlyUnit.faction);

            foreach (var trait in owner.unitTrait)
            {
                var (v, flag) = trait.OnBeforeSupport(owner, target, value);
                value = v;
                isSupportable = flag || isSupportable;
            }
            foreach (var effect in owner.statusEffects)
            {
                var (v, flag) = effect.OnBeforeSupport(owner, target, value);
                value = v;
                isSupportable = flag || isSupportable;
            }
            foreach (var artifact in GameContext.Instance.myArtifacts)
            {
                var (v, flag) = artifact.OnBeforeSupport(owner, target, value);
                value = v;
                isSupportable = flag || isSupportable;
            }
            return (value, isSupportable);
        }

        public virtual void DoSupport(Area target)
        {
            var (value, isSupportable) = BeforeSupport(target);
            if (!isSupportable) return;
            if (owner.unitType == Unit.UnitType.Cavalry || owner.unitType == Unit.UnitType.RangedCavalry)
            {
                owner.AddStatusEffect(new SideExposed(1));
            }
            if (target.occupyingEnemyUnit != null)
            {
                target.occupyingEnemyUnit.Damaged(owner, Unit.DamageType.Support, value);
            }
            AfterSupport(target, value);
        }

        public void AfterSupport(Area target, int value)
        {
            owner.isSupportable = false; // 지원 후에는 다시 지원할 수 없도록 설정
            foreach (var effect in owner.statusEffects)
            {
                effect.OnAfterSupport(owner, target, value);
            }
            foreach (var trait in owner.unitTrait)
            {
                trait.OnAfterSupport(owner, target, value);
            }
            foreach (var artifact in GameContext.Instance.myArtifacts)
            {
                artifact.OnAfterSupport(owner, target, value);
            }
        }


    }
}



