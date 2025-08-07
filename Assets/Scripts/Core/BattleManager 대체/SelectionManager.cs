public class SelectionManager
{
    public IUnit CurrentUnit { get; private set; }
    public IArea CurrentArea { get; private set; }
    public Action CurrentAction { get; private set; } = Action.None;

    public void SetUnit(IUnit unit) => CurrentUnit = unit;
    public void SetArea(IArea area) => CurrentArea = area;
    public void SetAction(Action action) => CurrentAction = action;

    public void Clear()
    {
        CurrentUnit = null;
        CurrentArea = null;
        CurrentAction = Action.None;
    }
}
