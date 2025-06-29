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
    public virtual void OnRemoveArtifact(Area area)
    {
        // ��Ƽ��Ʈ ���� ��, GameContext���� ����
        if (GameContext.Instance.myArtifacts.Contains(this))
        {
            GameContext.Instance.myArtifacts.Remove(this);
        }
    }
    public virtual void AreaOnApply(Area area) { }
    public virtual void AreaOnSupport(Area area, Unit supporter, int value) { }
    public virtual void AreaOnTurnEnd(Area area) { }
    public virtual void OnNodeClicked(Node node){    }

}
