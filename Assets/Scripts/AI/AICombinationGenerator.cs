using System.Collections.Generic;
using System.Linq;
public static class AICombinationGenerator
{
    public static List<List<(int unitId, SimActionPlan)>> EnumerateAllCombos(
        Dictionary<int, List<SimActionPlan>> unitActions)
    {
        var unitIds = unitActions.Keys.ToList();
        var result = new List<List<(int, SimActionPlan)>>();

        void Recurse(int depth, List<(int, SimActionPlan)> path)
        {
            if (depth == unitIds.Count)
            {
                result.Add(new List<(int, SimActionPlan)>(path));
                return;
            }

            int unitId = unitIds[depth];
            var actions = unitActions[unitId];

            foreach (var action in actions)
            {
                path.Add((unitId, action));
                Recurse(depth + 1, path);
                path.RemoveAt(path.Count - 1);
            }
        }

        Recurse(0, new List<(int, SimActionPlan)>());
        var dependencyCombo = GenerateDependencyCombo(unitActions);
        if (dependencyCombo != null)
            result.Add(dependencyCombo);
        return result;
    }
    public static List<(int unitId, SimActionPlan)> GenerateDependencyCombo(Dictionary<int, List<SimActionPlan>> unitActions)
{
    var result = new List<(int, SimActionPlan)>();

    foreach (var (unitId, plans) in unitActions)
    {
        foreach (var plan in plans)
        {
            var unit = UnitManager.Instance.allUnits.Find(u => u.unitId == unitId);
            if (unit == null) continue;

            // ���� �ִٸ� ���� ��θ� ����
            if (!GridHelper.Instance.IsMoveAllowed(unit, plan.targetArea))
            {
                var chain = GetUnblockMoveChain(unit, plan.targetArea);
                if (chain == null) continue;

                // �ߺ� ����
                HashSet<int> added = new();
                foreach (var (u, a) in chain)
                {
                    if (added.Contains(u.unitId)) continue;
                    result.Add((u.unitId, new SimActionPlan(Action.Move, a)));
                    added.Add(u.unitId);
                }
                return result; // �ϳ��� ������ �߰�
            }
        }
    }

    return null;
}

    public static List<(IUnit unit, IArea moveTo)> GetUnblockMoveChain(IUnit unit, IArea target, HashSet<int> visited = null)
    {
        if (visited == null)
            visited = new HashSet<int>();

        List<(IUnit, IArea)> result = new();

        if (visited.Contains(unit.unitId))
            return null;

        visited.Add(unit.unitId);

        if (!HasAnyMoveOption(unit))
            return null;

        var grid = AreaManager.Instance;
        if (!MoveAction.CanExecute(unit, target))
        {
            var blocker = unit.faction == Faction.Friendly ? target.occupyingFriendlyUnit : target.occupyingEnemyUnit;
            if (blocker == null || blocker == unit)
                return null;

            foreach (var area in grid.allAreas)
            {
                if (MoveAction.CanExecute(unit, area))
                {
                    var chain = GetUnblockMoveChain(blocker, area, visited);
                    if (chain != null)
                    {
                        chain.Add((blocker, area));
                        chain.Add((unit, target));
                        return chain;
                    }
                }
            }
            return null;
        }

        result.Add((unit, target));
        return result;
    }
    public static bool CanMoveWithDependency(IUnit unit, IArea target, HashSet<int> visited = null)
    {
        if (visited == null)
            visited = new HashSet<int>();

        if (visited.Contains(unit.unitId))
            return false; // ���� ���� ����

        visited.Add(unit.unitId);

        if (!HasAnyMoveOption(unit))
            return false;

        if (!GridHelper.Instance.IsMoveAllowed(unit, target))
        {
            var blockingUnit = unit.faction == Faction.Friendly
                ? target.occupyingFriendlyUnit
                : target.occupyingEnemyUnit;

            if (blockingUnit == null || blockingUnit == unit)
                return false; // ���� ���� ���� or �ڱ� �ڽ�

            // blockingUnit�� �̵� �����ϰ� �� �� ĭ�� �����ϴ��� üũ
            foreach (var area in AreaManager.Instance.allAreas)
            {
                if (GridHelper.Instance.IsMoveAllowed(blockingUnit, area))
                {
                    if (CanMoveWithDependency(blockingUnit, area, visited))
                        return true; // ���� �ִ� ������ �̵� �����ϸ�, ���� ����
                }
            }

            return false; // �� ������
        }

        return true; // ó������ �̵� ����
    }
    static bool HasAnyMoveOption(IUnit unit)
    {
        return AreaManager.Instance.allAreas
            .Any(area => MoveAction.CanExecute(unit, area));
    }
}
