using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossNode : Node
{
    public EnemySpawnPlan enemySpawnPlan; // �� ���� ��ȹ
    public int rewardGold;           // ���� ���� (���)
    public List<Unit> rewardUnits; // ���� ���� (���ֵ�)
    //public List<Artifact> rewardArtifacts; // ���� ���� (������)
    public string cutScene;
    public BossNode()
    {
        nodeType = NodeType.Boss; // ��� Ÿ���� Battle�� ����
        targetScene = "BossScene"; // �ش� ��尡 �ε��� �� �̸� ���� 
    }
    public override void  OnNodeCleared()
    {
        isCleared = true; // ��� Ŭ���� ���·� ����
        SceneManager.LoadScene(cutScene); // �ƽ� �� �ε�
    }
}
