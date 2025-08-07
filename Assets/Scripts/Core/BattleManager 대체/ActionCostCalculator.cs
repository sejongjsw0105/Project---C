public static class ActionCostCalculator
{
    public static int GetCost(Action action)
    {
        return action switch
        {
            Action.None => 0,
            Action.Move => 1,
            Action.Defend => 1,
            Action.Support => 2,
            _ => 1
        };
    }
}