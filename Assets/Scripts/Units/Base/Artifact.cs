using System.Linq;

public abstract class Artifact : BaseActionModifier
{
    public string artifactName;
    public virtual void OnGetArtifact()
    {
        if (!GameContext.Instance.myArtifacts.Contains(this))
        {
            GameContext.Instance.myArtifacts.Add(this);
        }
    }
    public virtual void OnRemoveArtifact(IArea area)
    {
        // 아티팩트 제거 시, GameContext에서 제거
        if (GameContext.Instance.myArtifacts.Contains(this))
        {
            GameContext.Instance.myArtifacts.Remove(this);
        }
    }
    public virtual void AreaOnApply(IArea area) { }
    public virtual void AreaOnSupport(IArea area, IUnit supporter, int value) { }
    public virtual void AreaOnTurnEnd(IArea area) { }
    public virtual void OnNodeClicked(Node node){    }

}
