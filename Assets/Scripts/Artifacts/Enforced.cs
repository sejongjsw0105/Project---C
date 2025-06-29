using UnityEngine;

public class Enforced : Artifact
{
    public Enforced()
    {
        artifactName = "Enforced";
    }
    public override void OnAfterSupport(IUnit supporter, IArea area, int value)
    {
        base.OnAfterSupport(supporter, area, value);
        supporter.state.CanSupport = true;
    }
}
