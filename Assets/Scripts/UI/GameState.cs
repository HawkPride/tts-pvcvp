﻿using UnityEngine;
using System.Collections;

namespace GameGUI.States
{
  public enum EGameStateType
  {
    MAIN_MENU,
    GAME_SINGLE,
    GAME_PVP_1x1,
    PLAYER_SCORE,
    WAITING_GAME,
    SETTINGS,
  }

  //Creation params
  public abstract class GameStateParams
  {
    public abstract string          GetSceneName();
    public abstract EGameStateType  GetStateType();
  }
  
  public abstract class GameState
    : MonoBehaviour
  {
    public abstract void            SetParams(GameStateParams stateParams);

    public abstract EGameStateType  GetStateType();

    public abstract void            OnStart();
    public abstract void            OnUpdate();
    public abstract void            OnEnd();


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

      Game.instance.ui.OnSwithcedToState(this);

      OnStart();

    }

    // Update is called once per frame
    //-----------------------------------------------------------------------------------
    void Update()
    {
      OnUpdate();
    }

    //-----------------------------------------------------------------------------------
    void OnDestroy()
    {
      OnEnd();
      Game.instance.ui.OnStateEnd(this);
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

    //-----------------------------------------------------------------------------------
    
    //Members
    //-----------------------------------------------------------------------------------
    Canvas          m_rootCanvas = null;
  }


}