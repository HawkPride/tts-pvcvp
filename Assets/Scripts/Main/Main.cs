using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
  public Data.GameData m_gameData;

  // Use this for initialization
  void Start()
  {
    DontDestroyOnLoad(gameObject);

    Game.instance.Init(m_gameData);
  }

  // Update is called once per frame
  void Update()
  {
    Game.instance.Update();
  }
}
