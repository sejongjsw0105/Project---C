public class BattleEndState : IBattleState
{
    public void Enter()
    {
        foreach (var unit in UnitManager.Instance.GetUnitsByFaction(Faction.Friendly))
        {
            if (unit.currentHealth <= 0) continue;

            unit.UpdateUnitDataFromBattle();

            var existing = GameContext.Instance.myUnitDataList
                .Find(data => data.id == unit.unitId);

            if (existing != null)
            {
                existing.currentHealth = unit.currentHealth;
                existing.statusEffects = unit.unitData.statusEffects;
            }
            else
            {
                GameContext.Instance.myUnitDataList.Add(unit.unitData);
            }
        }
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}