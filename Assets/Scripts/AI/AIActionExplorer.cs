using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AIActionExplorer
{
    public static Dictionary<int, List<SimActionPlan>> GetAllAvailableActionsByUnit(
            SimWorldSnapshot snapshot, Faction faction)
    {
        var result = new Dictionary<int, List<SimActionPlan>>();
        var units = snapshot.GetUnitsByFaction(faction);
        var allAreas = snapshot.areas;
        foreach (var unit in units)
        {
            if (unit.currentHealth <= 0) continue;
            var actions = new List<SimActionPlan>();
            // 이동
            if (unit.state.CanMove)
            {
                
                foreach (var area in allAreas)
                {
                    
                    if (MoveAction.CanExecute(unit, area))
                    {
                        actions.Add(new SimActionPlan(Action.Move, area));
                    }
                }
            }

            // 지원
            if (unit.state.CanSupport)
            {
                foreach (var area in allAreas)
                {
                    if (SupportAction.CanExecute(unit, area))
                    {
                        actions.Add(new SimActionPlan(Action.Support, area));
                    }
                }
            }

            // 방어
            if (DefendAction.CanExecute(unit,null))
            {
                // 방어는 지역 필요 없이 현재 지역 대상
                actions.Add(new SimActionPlan(Action.Defend, unit.area as SimArea));
            }
            if (actions.Count > 0)
                result[unit.unitId] = actions;
        }

        return result;
    }
}
