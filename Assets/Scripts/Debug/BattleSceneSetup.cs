using System.Collections.Generic;
using UnityEngine;

public class BattleSceneSetup : MonoBehaviour
{
    public GameObject sasuPrefab;
    public GameObject minbyeongPrefab;

    private void Awake()
    {
        /*GameContext.Instance.nextEnemyPlan = new EnemySpawnPlan
        {
            units = new List<EnemySpawnPlan.EnemyUnitEntry>
            {
                new EnemySpawnPlan.EnemyUnitEntry
                {
                    unitPrefab = sasuPrefab,
                    spawnPosition = new Vector2Int(1, 3)
                },
                new EnemySpawnPlan.EnemyUnitEntry
                {
                    unitPrefab = minbyeongPrefab,
                    spawnPosition = new Vector2Int(0, 3)
                }
            }
        };*/
    }
}
