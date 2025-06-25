using UnityEngine;

public class EventNode : Node
{
    int eventId; // 이벤트 ID, 필요시 사용
    public EventNode() {
        nodeType = NodeType.Event; // 노드 타입을 Event로 설정
        targetScene = "EventScene"; // 해당 노드가 로드할 씬 이름 설정
    }

}
