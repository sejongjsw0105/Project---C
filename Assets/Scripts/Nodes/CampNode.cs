using UnityEngine;

public class CampNode : Node
{ 
    public CampNode()
    {
        nodeType = NodeType.Camp; // 노드 타입을 Camp로 설정
        targetScene = "CampScene"; // 해당 노드가 로드할 씬 이름 설정
    }
}
