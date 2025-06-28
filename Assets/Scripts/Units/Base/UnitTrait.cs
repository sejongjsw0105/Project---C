using System.Collections.Generic;

public abstract class UnitTrait : BaseActionModifier
{
    public string traitName = "Trait";
    public List<UnitType> unitTypes = new();
}
