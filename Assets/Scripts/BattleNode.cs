using System.Collections.Generic;
using UnityEngine;

public class BattleNode : Node
{
    public EnemySpawnPlan enemySpawnPlan; // 적 스폰 계획
    public int rewardGold;           // 전투 보상 (골드)
    public List<Unit> rewardUnits; // 전투 보상 (유닛들)
    public BattleNode()
    {
        nodeType = NodeType.Battle; // 노드 타입을 Battle로 설정
        targetScene = "BattleScene"; // 해당 노드가 로드할 씬 이름 설정 
    }
}

