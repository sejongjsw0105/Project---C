using UnityEngine;

public class EventNode : Node
{
    int eventId; // �̺�Ʈ ID, �ʿ�� ���
    public EventNode() {
        nodeType = NodeType.Event; // ��� Ÿ���� Event�� ����
        targetScene = "EventScene"; // �ش� ��尡 �ε��� �� �̸� ����
    }

}
