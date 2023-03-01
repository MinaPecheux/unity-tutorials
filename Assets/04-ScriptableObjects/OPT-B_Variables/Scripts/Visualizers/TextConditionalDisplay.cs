using UnityEngine;

public class TextConditionalDisplay : BaseVisualizer
{
  [SerializeField] private TMPro.TextMeshProUGUI _text;
  [SerializeField] private string _trueText;
  [SerializeField] private string _falseText;

  private BoolVariable _boolData;

  private void Awake()
  {
    _boolData = (BoolVariable)data;
  }

  protected override void _UpdateDisplay()
  {
    _text.text = _boolData.value ? _trueText : _falseText;
  }
}
