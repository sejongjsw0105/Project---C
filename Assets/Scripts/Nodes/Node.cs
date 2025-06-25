using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Node: MonoBehaviour
{
    public enum NodeType
    {
        Battle,     // 일반 전투
        Elite,      // 엘리트 전투
        Boss,       // 보스 전투
        Camp,       // 회복/강화
        Shop,       // 상점
        Artifact,   // 유물 획득
        Trait,      // 트레잇 부여
        Event,      // 이벤트
        None        // 빈 노드 또는 미정
    }
    public NodeType nodeType;                  // 노드 타입
    public int nodeMapId;                                   
    public int nodeId;
    public Vector2Int gridPosition;            // 메인맵 내 좌표
    public List<Node> connectedNextNodes;    // 연결된 다음 노드들
    public List<Node> connectedPrevNodes;    // 연결된 이전 노드들
    public bool isCleared;                     // 클리어 여부
    public bool isCurrent;                     // 현재 위치 여부
    public string targetScene;                 // 해당 노드가 로드할 씬 이름
    public void OnNodeClicked()
    {

        if(isCurrent) return; // 이미 현재 위치인 경우 클릭 무시
        if (isCleared) return; // 이미 클리어된 노드인 경우 클릭 무시
        foreach (var Node in connectedPrevNodes)
        {
            if(Node.isCurrent == true&& Node.isCleared)
            {
                Node.isCurrent = false; // 이전 노드의 현재 위치 해제
                isCurrent = true; // 현재 노드를 현재 위치로 설정
                GameContext.Instance.setCurrentNode(this); // 게임 컨텍스트에 현재 노드 설정
                foreach (var artifact in GameContext.Instance.myArtifacts)
                {
                    artifact.OnNodeClicked(); // 현재 노드 클릭 이벤트 처리
                }
                SceneManager.LoadScene(targetScene);
                return;
            }
        }
    }
    public virtual void OnNodeCleared()
    {
        isCleared = true; // 노드 클리어 상태로 변경
        SceneManager.LoadScene("MainMapScene"); // 메인맵 씬으로 이동
    }
}

