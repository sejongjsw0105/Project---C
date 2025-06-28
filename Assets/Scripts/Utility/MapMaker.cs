
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public GameObject nodePrefab;
    public Transform nodeParent;
    public int totalStages = 12;

    public List<List<Node>> nodeGrid = new(); // 전체 맵 노드 구조

    private Dictionary<Node.NodeType, float> branchNodeWeights = new()
    {
        { Node.NodeType.Battle, 0.4f },
        { Node.NodeType.Event, 0.3f },
        { Node.NodeType.Elite, 0.1f },
        { Node.NodeType.Camp, 0.1f },
        { Node.NodeType.Shop, 0.1f }
    };

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        nodeGrid.Clear();
        int nodeIdCounter = 0;
        System.Random rng = new();

        // 1. 메인 경로 타입 분배
        Dictionary<Node.NodeType, int> mainPathTypeCount = new()
        {
            { Node.NodeType.Artifact, 1 },
            { Node.NodeType.Camp, 1 },
            { Node.NodeType.Shop, Random.value < 0.5f ? 1 : 0 },
            { Node.NodeType.Elite, Random.value < 0.5f ? 1 : 0 },
            { Node.NodeType.Event, Random.Range(2, 6) },
            { Node.NodeType.Battle, 11 } // 나중에 보스/주둔지/기타로 줄어듬
        };

        List<Node.NodeType> fixedTypes = new();
        foreach (var kvp in mainPathTypeCount)
        {
            for (int i = 0; i < kvp.Value; i++)
                fixedTypes.Add(kvp.Key);
        }
        fixedTypes = fixedTypes.OrderBy(_ => Random.value).ToList();
        fixedTypes.Insert(totalStages - 1, Node.NodeType.Boss); // 마지막 보스 확정
        fixedTypes[totalStages - 2] = Node.NodeType.Camp; // 마지막 전 주둔지 고정
        fixedTypes[totalStages / 2] = Node.NodeType.Artifact; // 중간 유물 고정

        // 2. 메인 경로 생성
        List<Node> mainPath = new();
        for (int stage = 0; stage < totalStages; stage++)
        {
            Vector2Int pos = new(rng.Next(0, 4), stage);
            GameObject obj = Instantiate(nodePrefab, nodeParent);
            obj.transform.localPosition = new Vector3(pos.x * 3, 0, -stage * 3);

            Node node = obj.GetComponent<Node>();
            node.nodeId = nodeIdCounter++;
            node.mapStage = stage;
            node.gridPosition = pos;
            node.nodeType = fixedTypes[stage];
            node.targetScene = GetSceneByType(node.nodeType);

            EnsureGridSize(stage);
            nodeGrid[stage].Add(node);
            mainPath.Add(node);
        }

        // 3. 메인 경로 연결
        for (int i = 0; i < mainPath.Count - 1; i++)
        {
            var curr = mainPath[i];
            var next = mainPath[i + 1];
            curr.connectedNextNodes.Add(next);
            next.connectedPrevNodes.Add(curr);
        }

        // 4. 분기 노드 생성 및 연결
        for (int stage = 0; stage < totalStages; stage++)
        {
            int additional = Random.Range(2, 4);
            while (nodeGrid[stage].Count < additional + 1)
            {
                Vector2Int pos = new(Random.Range(0, 4), stage);
                if (nodeGrid[stage].Any(n => n.gridPosition == pos)) continue;

                GameObject obj = Instantiate(nodePrefab, nodeParent);
                obj.transform.localPosition = new Vector3(pos.x * 3, 0, -stage * 3);

                Node node = obj.GetComponent<Node>();
                node.nodeId = nodeIdCounter++;
                node.mapStage = stage;
                node.gridPosition = pos;
                node.nodeType = GetWeightedRandomType();
                node.targetScene = GetSceneByType(node.nodeType);

                nodeGrid[stage].Add(node);
            }
        }

        // 5. 연결: 모든 노드 → 다음 층 노드 중 1~2개 연결
        for (int stage = 0; stage < totalStages - 1; stage++)
        {
            foreach (var curr in nodeGrid[stage])
            {
                var nextStage = nodeGrid[stage + 1];
                var candidates = nextStage.OrderBy(_ => Random.value).Take(Random.Range(1, 3));
                foreach (var next in candidates)
                {
                    if (!curr.connectedNextNodes.Contains(next))
                        curr.connectedNextNodes.Add(next);
                    if (!next.connectedPrevNodes.Contains(curr))
                        next.connectedPrevNodes.Add(curr);
                }
            }
        }
    }

    void EnsureGridSize(int stage)
    {
        while (nodeGrid.Count <= stage)
            nodeGrid.Add(new List<Node>());
    }

    Node.NodeType GetWeightedRandomType()
    {
        float roll = Random.value;
        float sum = 0f;

        foreach (var pair in branchNodeWeights)
        {
            sum += pair.Value;
            if (roll <= sum)
                return pair.Key;
        }
        return Node.NodeType.Battle; // fallback
    }

    string GetSceneByType(Node.NodeType type)
    {
        return type switch
        {
            Node.NodeType.Battle => "BattleScene",
            Node.NodeType.Elite => "EliteScene",
            Node.NodeType.Boss => "BossScene",
            Node.NodeType.Camp => "CampScene",
            Node.NodeType.Shop => "ShopScene",
            Node.NodeType.Artifact => "ArtifactScene",
            Node.NodeType.Trait => "TraitScene",
            Node.NodeType.Event => "EventScene",
            _ => "BattleScene"
        };
    }
}
