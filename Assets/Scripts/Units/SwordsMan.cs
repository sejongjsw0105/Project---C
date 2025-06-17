using UnityEngine;

public class SwordsMan : Unit
{
    // SwordsMan Ŭ������ Unit Ŭ������ ��ӹ޾� �����˴ϴ�.
    // �� Ŭ������ ���� ������ Ư���� �����մϴ�.
    public SwordsMan()
    {
        unitName = "Swordsman"; // ���� �̸� ����
        unitId = 1; // ���� ID ����
        health = 100; // �ʱ� ü�� ����
        attackPower = 15; // ���ݷ� ����
        defensePower = 10; // ���� ����
        faction = Faction.Friendly; // ���� ���� (��: �Ʊ�)
        unitType = UnitType.Melee; // ���� ���� ���� (��: ����)
    }
}
