using UnityEngine;

public class BarFill : BaseVisualizer
{
  [SerializeField] private UnityEngine.UI.Image _fill;

  private IntegerVariable _intData;

  private void Awake()
  {
    _intData = (IntegerVariable)data;
  }

  protected override void _UpdateDisplay()
  {
    _fill.fillAmount = _intData.value / (float) _intData.max;
  }
}
