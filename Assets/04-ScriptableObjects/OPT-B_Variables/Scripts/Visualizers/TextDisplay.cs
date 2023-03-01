using UnityEngine;

public class TextDisplay : BaseVisualizer
{
  [SerializeField] private TMPro.TextMeshProUGUI _text;

  private StringVariable _strData;

  private void Awake()
  {
    _strData = (StringVariable)data;
  }

  protected override void _UpdateDisplay()
  {
    _text.text = _strData.value;
  }
}
