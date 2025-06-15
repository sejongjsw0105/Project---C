using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{

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
    }
    public Area area;              // 유닛이 위치한 영역
    public string unitName;            // 유닛 이름
    public int unitId;                 // 유닛 ID
    public int health;                 // 유닛 체력
    public int attackPower;            // 유닛 공격력
    public int defensePower;           // 유닛 방어력
    public Faction faction;           // 유닛의 진영 (예: 아군, 적군)
    public UnitType type;              // 유닛의 종류 (예: 보병, 기병, 원거리)
    
    public List<StatusEffect> statusEffects = new List<StatusEffect> (); // 유닛의 상태 효과 목록
    private void OnMouseDown()
    {
        UnitSelector.Instance.OnUnitClicked(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject gameObject = this.gameObject;
    }
    public void Damaged(int damage)
    {
        // 유닛이 피해를 입었을 때의 처리
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        // 유닛이 죽었을 때의 처리
        Destroy(gameObject);
    }
    public class Support
    {
        public AreaCondition areacond; // 지원이 가능한 영역 조건
        public int supportAmount; // 지원량
        public int supportRange; // 지원 범위
        public Support(AreaCondition areacond, int supportAmount, int supportRange)
        {
            this.areacond = areacond;
            this.supportAmount = supportAmount;
            this.supportRange = supportRange;
        }

    }


}
