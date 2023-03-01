using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Variables/Float")]
public class FloatVariable : BaseVariable
{
  public float value;

  public override string Str() => $"{value}";
}
