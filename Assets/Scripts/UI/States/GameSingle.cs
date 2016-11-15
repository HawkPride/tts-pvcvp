using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace GUI.States
{
  //Creation params
  public class GameSingleParams : GameStateParams
  {
    public override string          GetSceneName() { return "GameSingle"; }
    public override EGameStateType  GetStateType() { return EGameStateType.GAME_SINGLE; }
  }




  public class GameSingle : GameState
  {

    public Glass m_glass;

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.GAME_SINGLE;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      m_glass.m_listenerGameEnd += OnGameEnd;
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {


    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {
      m_glass.m_listenerGameEnd -= OnGameEnd;
    }

    //-----------------------------------------------------------------------------------
    public void OnGameEnd()
    {
      GameResults res = new GameResults();
      res.Score = m_glass.Score;
      Game.Instance.Results = res;
      Game.Instance.Ui.SwitchToState(new PlayersScoreParams(m_glass.Score));
    }
  }
}