public class CommandPointManager
{
    public int Current { get; private set; }
    public int Max { get; private set; }

    public CommandPointManager(int max)
    {
        Max = max;
        Current = max;
    }

    public bool CanAfford(int amount) => Current >= amount;

    public void Spend(int amount)
    {
        if (CanAfford(amount))
            Current -= amount;
    }

    public void Refund(int amount)
    {
        Current += amount;
        if (Current > Max) Current = Max;
    }

    public void Reset() => Current = Max;
}