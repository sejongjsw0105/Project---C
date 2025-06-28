using UnityEngine;

public class Blitzkrieg : Artifact
{
    public Blitzkrieg()
    {
        artifactName = "Blitzkrieg";
    }
    public override void OnAfterMove(Unit unit, Area target)
    {
        unit.state.CanMove= true;
    }

}
