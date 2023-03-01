using UnityEngine;

public abstract class BaseMultiVisualizer : MonoBehaviour
{
  public BaseVariable[] data;

  private void OnEnable()
  {
    foreach (BaseVariable item in data)
      item.updated.AddListener(_UpdateDisplay);
  }

  private void OnDisable()
  {
    foreach (BaseVariable item in data)
      item.updated.RemoveListener(_UpdateDisplay);
  }

  private void Start()
  {
    _UpdateDisplay();
  }

  protected abstract void _UpdateDisplay();
}
