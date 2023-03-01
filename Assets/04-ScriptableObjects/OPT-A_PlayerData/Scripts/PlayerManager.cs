using UnityEngine;

namespace Tutorial04_ScriptableObjects
{

  public class PlayerManager : MonoBehaviour
  {
    public PlayerData data;

    public void DecreaseHealth()
    {
      // decrease player health
      data.health -= 10;
      data.updated.Invoke();
    }

    public void IncreaseXP()
    {
      // increase player XP
      data.XP += 10;
      data.updated.Invoke();
    }
  }

}
