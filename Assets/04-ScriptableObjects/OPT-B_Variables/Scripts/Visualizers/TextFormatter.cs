using System.Linq;
using UnityEngine;

public class TextFormatter : BaseMultiVisualizer
{
  [SerializeField] private TMPro.TextMeshProUGUI _text;
  [SerializeField] private string _format;

  protected override void _UpdateDisplay()
  {
    _text.text = System.String.Format(_format, data.Select(
      (BaseVariable item) => item.Str()).ToArray());
  }
}
