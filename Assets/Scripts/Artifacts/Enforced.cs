using UnityEngine;

public class Enforced : Artifact
{
    public Enforced()
    {
        artifactName = "Enforced";
    }
    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        base.OnAfterSupport(supporter, area, value);
        supporter.isSupportable = true;
    }
}
