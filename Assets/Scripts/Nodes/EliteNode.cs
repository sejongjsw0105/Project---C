using System.Collections.Generic;
using UnityEngine;

public class EliteNode : Node
{
    public EnemySpawnPlan enemySpawnPlan; // 적 스폰 계획
    public int rewardGold;           // 전투 보상 (골드)
    public List<Unit> rewardUnits; // 전투 보상 (유닛들)
    //public List<Artifact> rewardArtifacts; // 전투 보상 (유물들)
    public EliteNode()
    {
        nodeType = NodeType.Elite; // 노드 타입을 Elite로 설정
        targetScene = "EliteScene"; // 해당 노드가 로드할 씬 이름 설정
    }
    // 엘리트 노드에 특화된 추가 기능이나 속성이 필요하다면 여기에 구현

}