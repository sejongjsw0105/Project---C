using System.Collections.Generic;
public class GameContext : MonoSingleton<GameContext>
{
    public Node currentNode;
    public List<Artifact> myArtifacts = new List<Artifact>();
    public int morale;
    public int commandPoints;
    //public Commander currentCommander;
    public int Gold;
    public Node currentNode;
    public static GameContext Instance { get; private set; }
    public int maxCommandPoints = 4;
    public string commanederName; // Commander name
    public List<UnitData> myUnitDataList = new();
    public List<Artifact> myArtifacts = new List<Artifact>();
    //public FriendlyArmyState armyState = new FriendlyArmyState();
    //public EnemySpawnPlan nextEnemyPlan = null;
    public List<string> chosenEvents = new();
    public List<IUnit> deadUnits = new();
    // 기타 필요한 글로벌 상태 정보...
}
