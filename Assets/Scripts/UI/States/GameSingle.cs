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
     StatsProviderBase sp = Game.Instance.Stats;
      //New score is not in hi score
      if (sp.GetNewScoreIndex(m_glass.Score) < 0 || m_glass.Score == 0)
        GameMessage.Create(GetCanvas(), "Game Over", OnGameOver, 3.0f);
      else
        GameMessage.Create(GetCanvas(), "New High Score", OnHighScore, 3.0f);

    }

    //-----------------------------------------------------------------------------------
    public void OnHighScore()
    {
      GameResults res = new GameResults();
      res.Score = m_glass.Score;
      Game.Instance.Results = res;
      Game.Instance.Ui.SwitchToState(new PlayersScoreParams(m_glass.Score));
    }

    //-----------------------------------------------------------------------------------
    public void OnGameOver()
    {
      Game.Instance.Ui.SwitchToState(new MainMenuParams());
    }
  }
}