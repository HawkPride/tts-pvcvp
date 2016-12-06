using UnityEngine;
using System;

namespace Data
{
  [CreateAssetMenu(fileName = "GameData", menuName = "GameData", order = 1)]
  public class GameData : ScriptableObject
  {
    public string     gameVersion = "0.0.0.1";
    public GameObject messageBox;
    public GameObject gameMessage;
    
    void OnEnable()
    {

    }
  }
}
