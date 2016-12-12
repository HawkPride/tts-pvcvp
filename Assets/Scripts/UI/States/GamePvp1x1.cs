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
    public BlockRendererGlass   m_glassRend;
    public BlockRendererPreview m_glassPrev;

    public BlockRendererGlass   m_glassOpponentRend;

    Glass m_localGlass;
    Glass m_otherGlass;

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.GAME_PVP_1x1;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      //Local
      m_localGlass = new Glass();
      m_localGlass.Init();

      m_localGlass.m_delGameEnd += (() => { OnGameEnd(false); });

      InputProvider ip = GetComponent<InputProvider>();
      if (ip != null)
        ip.m_delEvent += m_localGlass.OnInputEvent;

      if (m_glassRend != null)
        m_glassRend.m_glass = m_localGlass;
      if (m_glassPrev != null)
        m_glassPrev.m_glass = m_localGlass;

      //Other
      m_otherGlass = new Glass();
      m_otherGlass.Init();
      m_otherGlass.m_delGameEnd += (() => { OnGameEnd(true); });

      if (m_glassOpponentRend != null)
        m_glassOpponentRend.m_glass = m_otherGlass;
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {
      m_localGlass.Update();
      m_otherGlass.Update();

    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {
    }

    //-----------------------------------------------------------------------------------
    public void OnGameEnd(bool bOther)
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