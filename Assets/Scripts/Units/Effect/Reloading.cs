using UnityEngine;
public class Reloading : StatusEffect
{
    public Reloading(int duration) : base("Reloading", StackType.duration, duration, 0)
    {
    }
    public override ResultContext<int> OnBeforeSupport(Unit supporter, Area area, int value)
    {
        var result = new ResultContext<int>(value);
        result.Block("Realoading");
        return result;

    }

}