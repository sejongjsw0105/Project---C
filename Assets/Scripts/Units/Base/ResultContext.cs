using UnityEngine;

public class ResultContext<T>
{
    public T Value { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsModified { get; set; }
    public string Source { get; set; } // � Ʈ����/ȿ���� �����ߴ��� (�ɼ�)

    public ResultContext(T value)
    {
        Value = value;
        IsBlocked = false;
        IsModified = false;
    }

    public void Block(string source = "")
    {
        IsBlocked = true;
        Source = source;
    }

    public void Modify(T newValue, string source = "")
    {
        Value = newValue;
        IsModified = true;
        Source = source;
    }
}
