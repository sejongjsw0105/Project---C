using System.Collections.Generic;
using UnityEngine;

public class TraitNode : Node
{
    public List<UnitTrait> Traits; // ���� ���� (���ֵ�)
    public TraitNode()
    {
        nodeType = NodeType.Trait; // ��� Ÿ���� Battle�� ����
        targetScene = "TraitScene"; // �ش� ��尡 �ε��� �� �̸� ���� 
    }
}
