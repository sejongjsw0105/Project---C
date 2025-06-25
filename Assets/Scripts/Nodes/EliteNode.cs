using System.Collections.Generic;
using UnityEngine;

public class EliteNode : Node
{
    public EnemySpawnPlan enemySpawnPlan; // �� ���� ��ȹ
    public int rewardGold;           // ���� ���� (���)
    public List<Unit> rewardUnits; // ���� ���� (���ֵ�)
    //public List<Artifact> rewardArtifacts; // ���� ���� (������)
    public EliteNode()
    {
        nodeType = NodeType.Elite; // ��� Ÿ���� Elite�� ����
        targetScene = "EliteScene"; // �ش� ��尡 �ε��� �� �̸� ����
    }
    // ����Ʈ ��忡 Ưȭ�� �߰� ����̳� �Ӽ��� �ʿ��ϴٸ� ���⿡ ����

}