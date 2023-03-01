using UnityEngine;

public abstract class BaseVisualizer : MonoBehaviour
{
  public BaseVariable data;

  private void OnEnable()
  {
    data.updated.AddListener(_UpdateDisplay);
  }

  private void OnDisable()
  {
    data.updated.RemoveListener(_UpdateDisplay);
  }

  private void Start()
  {
    _UpdateDisplay();
  }

  protected abstract void _UpdateDisplay();
}
