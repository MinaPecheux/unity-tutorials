using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Variables/String")]
public class StringVariable : BaseVariable
{
  public string value;

  public override string Str() => value;
}
