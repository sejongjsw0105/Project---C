using UnityEngine;

public class UnitState : MonoBehaviour
{

    public bool CanAttack = true; // 유닛이 공격 가능한지 여부
    public bool CanMove = true;   // 유닛이 이동 가능한지 여부
    public bool CanSupport = true; // 유닛이 지원 가능한지 여부
    public bool CanDamaged = true; // 유닛이 피해를 입을 수 있는지 여부
    public bool CanDefend = true; // 유닛이 방어 중인지 여부
    public void BeginBattle()
    {

    }
    public void ResetTurn()
    {
        CanAttack = true; // 턴 시작 시 공격 가능
        CanMove = true;   // 턴 시작 시 이동 가능
        CanSupport = true; // 턴 시작 시 지원 가능
        CanDamaged = true; // 턴 시작 시 피해 입을 수 있음
        CanDefend = true; // 턴 시작 시 방어 가능
    }
}

