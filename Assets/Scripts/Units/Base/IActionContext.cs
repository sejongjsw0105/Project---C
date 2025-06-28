public interface IActionContext
{
    Unit Actor { get; }
    Area TargetArea { get; }
    bool IsBlocked { get; set; }
}
