using System.Collections.Generic;
using UnityEngine;

public class ShopNode : Node
{
    //public List<Artifact> artifacts; // �������� �Ǹ��ϴ� ������
    public List<Unit> sellingUnits; // ���� ���� (���ֵ�)
    public ShopNode()
    {
        nodeType = NodeType.Shop; // ��� Ÿ���� Battle�� ����
        targetScene = "ShopScene"; // �ش� ��尡 �ε��� �� �̸� ���� 
    }
}


