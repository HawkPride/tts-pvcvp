using UnityEngine;
using System.Collections;

namespace GUI.States
{
  public enum EGameStateType
  {
    MAIN_MENU,
    GAME_SINGLE,
    PLAYER_SCORE,
    MESSAGE_BOX,
  }

  //Creation params
  public abstract class GameStateParams
  {
    public abstract string          GetSceneName();
    public abstract EGameStateType  GetStateType();
  }

  public abstract class GameState : MonoBehaviour
  {
    public abstract EGameStateType  GetStateType();
    public abstract void            OnStart();
    public abstract void            OnUpdate();


    // Use this for initialization
    //-----------------------------------------------------------------------------------
    void Start()
    {
      var arObjects = gameObject.scene.GetRootGameObjects();
      for (int i = 0; i < arObjects.Length; i++)
      {
        m_rootCanvas = arObjects[i].GetComponent<Canvas>();
        if (m_rootCanvas != null)
          break;
      }
      OnStart();
    }

    // Update is called once per frame
    //-----------------------------------------------------------------------------------
    void Update()
    {
      OnUpdate();
    }

    //-----------------------------------------------------------------------------------
    public void DeleteGameState()
    {
      Destroy(gameObject);
    }

    //-----------------------------------------------------------------------------------
    public Canvas GetCanvas()
    {
      return m_rootCanvas;
    }

    //Members
    //-----------------------------------------------------------------------------------
    Canvas          m_rootCanvas = null;
    GameStateParams m_creationParams = null;
  }


}