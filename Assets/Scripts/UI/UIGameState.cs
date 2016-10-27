using UnityEngine;
using System.Collections;

namespace GUI
{
  public enum EGameState
  {
    MAIN_MENU,
    GAME_SINGLE,
    PLAYER_SCORE,
    MESSAGE_BOX,
  }

  public abstract class UIGameState : MonoBehaviour
  {
    public abstract EGameState  GetStateType();
    public abstract void        OnStart();
    public abstract void        OnUpdate();


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
    Canvas m_rootCanvas = null;
  }


}