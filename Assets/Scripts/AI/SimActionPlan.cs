public class SimActionPlan
{
    public Action action;
    public IArea targetArea;

    public SimActionPlan(Action action, IArea area)
    {
        this.action = action;
        this.targetArea = area;
    }
}