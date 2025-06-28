using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Node: MonoBehaviour
{
    public enum NodeType
    {
        Battle,     // �Ϲ� ����
        Elite,      // ����Ʈ ����
        Boss,       // ���� ����
        Camp,       // ȸ��/��ȭ
        Shop,       // ����
        Artifact,   // ���� ȹ��
        Trait,      // Ʈ���� �ο�
        Event,      // �̺�Ʈ
        None        // �� ��� �Ǵ� ����
    }
    public NodeType nodeType;                  // ��� Ÿ��
    public int mapStage; // �� ��° ��/������        
    public int nodeId;
    public Vector2Int gridPosition;            // ���θ� �� ��ǥ
    public List<Node> connectedNextNodes;    // ����� ���� ����
    public List<Node> connectedPrevNodes;    // ����� ���� ����
    public bool isCleared;                     // Ŭ���� ����
    public bool isCurrent;                     // ���� ��ġ ����
    public string targetScene;                 // �ش� ��尡 �ε��� �� �̸�
    public void OnNodeClicked()
    {

        if(isCurrent) return; // �̹� ���� ��ġ�� ��� Ŭ�� ����
        if (isCleared) return; // �̹� Ŭ����� ����� ��� Ŭ�� ����
        foreach (var Node in connectedPrevNodes)
        {
            if(Node.isCurrent == true&& Node.isCleared)
            {
                Node.isCurrent = false; // ���� ����� ���� ��ġ ����
                isCurrent = true; // ���� ��带 ���� ��ġ�� ����
                GameContext.Instance.setCurrentNode(this); // ���� ���ؽ�Ʈ�� ���� ��� ����
                foreach (var artifact in GameContext.Instance.myArtifacts)
                {
                    artifact.OnNodeClicked(); // ���� ��� Ŭ�� �̺�Ʈ ó��
                }
                SceneManager.LoadScene(targetScene);
                return;
            }
        }
    }
    public virtual void OnNodeCleared()
    {
        isCleared = true; // ��� Ŭ���� ���·� ����
        SceneManager.LoadScene("MainMapScene"); // ���θ� ������ �̵�
    }
}

