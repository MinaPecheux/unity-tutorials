using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
  [HideInInspector] public UnityEvent updated;

  public int XP;

  [Range(0, 100)]
  [SerializeField]
  private int _health;

  public int health
  {
    get { return _health; }
    set
    {
      _health = value;
      if (_health < 0) _health = 0;
      else if (_health > 100) _health = 100;
    }
  }

  private void OnEnable()
  {
    // called when the instance is setup

    if (updated == null)
      updated = new UnityEvent();
  }

  private void OnValidate()
  {
    // called when any value is changed
    // in the inspector

    updated.Invoke();
  }

}
