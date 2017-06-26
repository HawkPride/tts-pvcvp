using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameGUI.States;

namespace GameGUI
{


  public class UserInterface
  {
    //Private
    GameStateParams   m_currStateParams = null;
    GameState         m_currState       = null;

    //Public
    public GameState state { get { return m_currState; } }


    //-----------------------------------------------------------------------------------
    public void Init()
    {
      
    }

    //-----------------------------------------------------------------------------------
    public void SwitchToState(GameStateParams stateParams)
    {
      //Only one state switch alowed
      if (m_currStateParams != null)
        Debug.LogError("There is already state, that being switching to. Cur " + m_currStateParams.GetSceneName() + " new " + stateParams.GetSceneName());


      string strSceneName = stateParams.GetSceneName();
      if (strSceneName.Length > 0)
      {
        m_currStateParams = stateParams;
        SceneManager.LoadScene(strSceneName);
      }
      else
        Debug.LogError("Undefined scene name for state " + stateParams.GetStateType().ToString());
    }

    //-----------------------------------------------------------------------------------
    public void OnSwithcedToState(GameState state)
    {
      m_currState = state;
      m_currState.SetParams(m_currStateParams);
      m_currStateParams = null;
    }

    //-----------------------------------------------------------------------------------
    public void OnStateEnd(GameState state)
    {
      Debug.Assert(m_currState == state);
      m_currState = null;
    }

  }
}