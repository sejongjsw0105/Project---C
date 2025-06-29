using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossNode : Node
{
    //public EnemySpawnPlan enemySpawnPlan; // 적 스폰 계획
    public int rewardGold;           // 전투 보상 (골드)
    public List<Unit> rewardUnits; // 전투 보상 (유닛들)
    //public List<Artifact> rewardArtifacts; // 전투 보상 (유물들)
    public string cutScene;
    public BossNode()
    {
        nodeType = NodeType.Boss; // 노드 타입을 Battle로 설정
        targetScene = "BossScene"; // 해당 노드가 로드할 씬 이름 설정 
    }
    public override void  OnNodeCleared()
    {
        isCleared = true; // 노드 클리어 상태로 변경
        SceneManager.LoadScene(cutScene); // 컷신 씬 로드
    }
}
