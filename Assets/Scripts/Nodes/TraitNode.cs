using System.Collections.Generic;
using UnityEngine;

public class TraitNode : Node
{
    public List<UnitTrait> Traits; // 전투 보상 (유닛들)
    public TraitNode()
    {
        nodeType = NodeType.Trait; // 노드 타입을 Battle로 설정
        targetScene = "TraitScene"; // 해당 노드가 로드할 씬 이름 설정 
    }
}
