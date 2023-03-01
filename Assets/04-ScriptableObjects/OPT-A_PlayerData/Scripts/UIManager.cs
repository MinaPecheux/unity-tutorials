using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tutorial04_ScriptableObjects
{

  public class UIManager : MonoBehaviour
  {
    public PlayerData data;

    [SerializeField] private Image _healthFill;
    [SerializeField] private TextMeshProUGUI _xpLabel;

    private void OnEnable()
    {
      data.updated.AddListener(_UpdateDataDisplay);
    }

    private void OnDisable()
    {
      data.updated.RemoveListener(_UpdateDataDisplay);
    }

    private void Start()
    {
      _UpdateDataDisplay();
    }

    private void _UpdateDataDisplay()
    {
      _healthFill.fillAmount = data.health / 100f;
      _xpLabel.text = $"XP: {data.XP}";
    }
  }

}
