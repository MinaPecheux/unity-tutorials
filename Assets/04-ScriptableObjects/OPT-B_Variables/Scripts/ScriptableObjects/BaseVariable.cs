using UnityEngine;
using UnityEngine.Events;

public abstract class BaseVariable : ScriptableObject
{
  [HideInInspector] public UnityEvent updated;

  private void OnEnable()
  {
    if (updated == null)
      updated = new UnityEvent();
  }

  private void OnValidate()
  {
    _CheckValue();
    updated.Invoke();
  }

  public abstract string Str();
  protected virtual void _CheckValue() { }
}
