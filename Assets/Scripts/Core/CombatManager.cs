using UnityEngine;


public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Combat()
    {
        foreach (var area in AreaManager.Instance.allAreas)
        {
            if (area.areaCondition != AreaCondition.InCombat) continue;
            IUnit firstAttacker = area.firstAttacker;
            IUnit secondAttacker = area.secondAttacker;
            AttackAction.Execute(firstAttacker, area);
            AttackAction.Execute(secondAttacker, area);
        }
    }
}
