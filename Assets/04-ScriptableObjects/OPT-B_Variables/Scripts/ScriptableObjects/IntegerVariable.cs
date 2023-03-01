using UnityEngine;

using CustomAttributes;

[CreateAssetMenu(menuName = "Scriptable Objects/Variables/Integer")]
public class IntegerVariable : BaseVariable
{
  [SerializeField] private int _value;

  [SerializeField] public bool _hasMin;
  [DrawIf("hasMin", true, ComparisonType.Equals)] [SerializeField] public int min;

  [SerializeField] public bool _hasMax;
  [DrawIf("hasMax", true, ComparisonType.Equals)] [SerializeField] public int max;

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
    if (_hasMin && _value < min) _value = min;
    if (_hasMax && _value > max) _value = max;
  }
}
