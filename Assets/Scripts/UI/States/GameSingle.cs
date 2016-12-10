using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace GameGUI.States
{
  //Creation params
  public class GameSingleParams : GameStateParams
  {
    public override string          GetSceneName() { return "GameSingle"; }
    public override EGameStateType  GetStateType() { return EGameStateType.GAME_SINGLE; }
  }




  public class GameSingle : GameState
  {

    public BlockRendererGlass   m_glassRend;
    public BlockRendererPreview m_glassPrev;

    public Glass                m_glass;
    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.GAME_SINGLE;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      m_glass = new Glass();
      m_glass.Init();

      m_glass.m_listenerGameEnd += OnGameEnd;

      InputProvider ip = GetComponent<InputProvider>();
      if (ip != null)
        ip.m_eventListeners += m_glass.OnInputEvent;

      if (m_glassRend != null)
        m_glassRend.m_glass = m_glass;
      if (m_glassPrev != null)
        m_glassPrev.m_glass = m_glass;
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {
      m_glass.Update();

    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {
      m_glass.m_listenerGameEnd -= OnGameEnd;


      InputProvider ip = GetComponent<InputProvider>();
      if (ip != null)
        ip.m_eventListeners -= m_glass.OnInputEvent;
    }

    //-----------------------------------------------------------------------------------
    public void OnGameEnd()
    {
      StatsProviderBase sp = Game.Instance.Stats;
      sp.m_nGamesPlayed++;
      //New score is not in hi score
      if (sp.GetNewScoreIndex(m_glass.Score) < 0 || m_glass.Score == 0)
        GameMessage.Create(GetCanvas(), "Game Over", OnGameOver, 1.5f);
      else
        GameMessage.Create(GetCanvas(), "New High Score", OnHighScore, 1.5f);

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
      Game.Instance.Stats.Save();
      Game.Instance.Ui.SwitchToState(new MainMenuParams());
    }
  }
}