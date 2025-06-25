using System.Collections.Generic;
using UnityEngine;

public class ShopNode : Node
{
    //public List<Artifact> artifacts; // 상점에서 판매하는 유물들
    public List<Unit> sellingUnits; // 전투 보상 (유닛들)
    public ShopNode()
    {
        nodeType = NodeType.Shop; // 노드 타입을 Battle로 설정
        targetScene = "ShopScene"; // 해당 노드가 로드할 씬 이름 설정 
    }
}


