using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class GameContext : MonoBehaviour
{
    public int Gold;
    public Node currentNode;
    public static GameContext Instance { get; private set; }
    public string commanederName; // Commander name
    public List<UnitData> myUnitDataList = new();
    public List<Artifact> myArtifacts = new List<Artifact>();
    public FriendlyArmyState armyState = new FriendlyArmyState();
    public EnemySpawnPlan nextEnemyPlan = null;
    public List<string> chosenEvents = new();
    public List<Unit> deadUnits = new();
    public void setCurrentNode(Node node)
    {
        if (node == null)
        {
            Debug.LogError("Attempted to set current node to null.");
            return;
        }
        currentNode = node;

    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
