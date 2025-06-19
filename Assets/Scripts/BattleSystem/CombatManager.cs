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
        Debug.Log("전투가 시작되었습니다.");

        foreach (var area in AreaManager.Instance.allAreas)
        {
            if (area.areaCondition != AreaCondition.InCombat) continue;
            Unit firstAttacker = area.firstAttacker;
            Unit secondAttacker = area.secondAttacker;
            if (firstAttacker.isAttackable){
                firstAttacker.DoAttack(secondAttacker);
            }
            if (secondAttacker.isAttackable)
            {
                secondAttacker.DoAttack(firstAttacker);
            }
        }
    }
}
