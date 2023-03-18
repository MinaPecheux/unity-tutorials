using UnityEngine;

using CustomAttributes;

[CreateAssetMenu(menuName = "Scriptable Objects/Variables/Integer")]
public class IntegerVariable : BaseVariable
{
  [SerializeField] private int _value;

  public bool hasMin;
  [DrawIf("hasMin", true, ComparisonType.Equals)] public int min;

  public bool hasMax;
  [DrawIf("hasMax", true, ComparisonType.Equals)] public int max;

  public int value
  {
    get { return _value; }
    set
    {
      _value = value;
      _CheckValue();
    }
  }

  public override string Str() => $"{_value}";
  protected override void _CheckValue()
  {
    if (hasMin && _value < min) _value = min;
    if (hasMax && _value > max) _value = max;
  }
}
