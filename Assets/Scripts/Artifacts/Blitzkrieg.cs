using UnityEngine;

public class Blitzkrieg : Artifact
{
    public Blitzkrieg()
    {
        artifactName = "Blitzkrieg";
    }
    public override void OnAfterMove(IUnit unit, IArea target)
    {
        unit.state.CanMove= true;
    }

}
