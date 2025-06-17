using UnityEngine;

public class SwordsMan : Unit
{
    // SwordsMan 클래스는 Unit 클래스를 상속받아 구현됩니다.
    // 이 클래스는 보병 유닛의 특성을 정의합니다.
    public SwordsMan()
    {
        unitName = "Swordsman"; // 유닛 이름 설정
        unitId = 1; // 유닛 ID 설정
        health = 100; // 초기 체력 설정
        attackPower = 15; // 공격력 설정
        defensePower = 10; // 방어력 설정
        faction = Faction.Friendly; // 진영 설정 (예: 아군)
        unitType = UnitType.Melee; // 유닛 종류 설정 (예: 보병)
    }
}
