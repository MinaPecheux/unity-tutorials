using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Variables/Bool")]
public class BoolVariable : BaseVariable
{
  public bool value;

  public override string Str() => $"{value}";
}
