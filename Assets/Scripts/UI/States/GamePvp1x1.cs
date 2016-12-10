using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace GameGUI.States
{
  //Creation params
  public class GamePvp1x1Params : GameStateParams
  {
    public override string          GetSceneName() { return "GamePvp1x1"; }
    public override EGameStateType  GetStateType() { return EGameStateType.GAME_PVP_1x1; }
  }




  public class GamePvp1x1 : GameState
  {

    public Glass m_ownerGlass;
    public Glass m_otherGlass;

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.GAME_PVP_1x1;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      m_ownerGlass.m_listenerGameEnd += OnGameEnd;
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {


    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {
      m_ownerGlass.m_listenerGameEnd -= OnGameEnd;
    }

    //-----------------------------------------------------------------------------------
    public void OnGameEnd()
    {
      /*StatsProviderBase sp = Game.Instance.Stats;
      sp.m_nGamesPlayed++;
      //New score is not in hi score
      if (sp.GetNewScoreIndex(m_ownerGlass.Score) < 0 || m_ownerGlass.Score == 0)
        GameMessage.Create(GetCanvas(), "Game Over", OnGameOver, 1.5f);
      else
        GameMessage.Create(GetCanvas(), "New High Score", OnHighScore, 1.5f);*/

    }

    //-----------------------------------------------------------------------------------
    public void OnHighScore()
    {
      /*GameResults res = new GameResults();
      res.Score = m_ownerGlass.Score;
      Game.Instance.Results = res;
      Game.Instance.Ui.SwitchToState(new PlayersScoreParams(m_ownerGlass.Score));*/
    }

    //-----------------------------------------------------------------------------------
    public void OnGameOver()
    {
      /*Game.Instance.Stats.Save();
      Game.Instance.Ui.SwitchToState(new MainMenuParams());*/
    }
  }
}