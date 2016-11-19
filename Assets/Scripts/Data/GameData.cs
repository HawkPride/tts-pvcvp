using UnityEngine;
using System;

namespace Data
{
  [CreateAssetMenu(fileName = "GameData", menuName = "GameData", order = 1)]
  public class GameData : ScriptableObject
  {
    public GameObject messageBox;
    public GameObject gameMessage;

    void OnEnable()
    {

    }
  }
}
