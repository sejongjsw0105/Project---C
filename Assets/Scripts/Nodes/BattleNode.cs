using System.Collections.Generic;
using UnityEngine;

public class BattleNode : Node
{
    //public EnemySpawnPlan enemySpawnPlan; // �� ���� ��ȹ
    public int rewardGold;           // ���� ���� (���)
    public List<Unit> rewardUnits; // ���� ���� (���ֵ�)
    public BattleNode()
    {
        nodeType = NodeType.Battle; // ��� Ÿ���� Battle�� ����
        targetScene = "BattleScene"; // �ش� ��尡 �ε��� �� �̸� ���� 
    }
}

