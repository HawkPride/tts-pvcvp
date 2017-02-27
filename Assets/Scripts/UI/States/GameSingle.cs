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
    public Net.PlayerGlass      m_player;
    
    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.GAME_SINGLE;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      PhotonNetwork.offlineMode = true;
      PhotonNetwork.CreateRoom("room");

      GameObject go = PhotonNetwork.Instantiate("PlayerGlass", Vector3.zero, Quaternion.identity, 0);
      if (go != null)
        m_player = go.GetComponent<Net.PlayerGlass>();
      if (m_player == null)
      {
        Debug.LogError("Unable to create player");
        return;
      }
      Logic.Glass glass = m_player.glass;

      glass.m_delGameEnd += OnGameEnd;

      if (m_glassRend != null)
        m_glassRend.m_glass = glass;
      if (m_glassPrev != null)
        m_glassPrev.m_glass = glass;
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {
     
    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {
      if (m_player != null)
        m_player.glass.m_delGameEnd -= OnGameEnd;

      PhotonNetwork.Disconnect();
    }

    //-----------------------------------------------------------------------------------
    public void OnGameEnd()
    {
      StatsProviderBase sp = Game.instance.stats;
      sp.GetConfig().m_nGamesPlayed++;
      //New score is not in hi score
      if (sp.GetNewScoreIndex(m_player.glass.score) < 0 || m_player.glass.score == 0)
        GameMessage.Create(GetCanvas(), "Game Over", OnGameOver, 1.5f);
      else
        GameMessage.Create(GetCanvas(), "New High Score", OnHighScore, 1.5f);

    }

    //-----------------------------------------------------------------------------------
    public void OnHighScore()
    {
      GameResults res = new GameResults();
      res.score = m_player.glass.score;
      Game.instance.results = res;
      Game.instance.ui.SwitchToState(new PlayersScoreParams(m_player.glass.score));
    }

    //-----------------------------------------------------------------------------------
    public void OnGameOver()
    {
      Game.instance.stats.Save();
      Game.instance.ui.SwitchToState(new MainMenuParams());
    }


    //-----------------------------------------------------------------------------------
    public void OnExitMatch()
    {
      m_player.glass.Pause(true);

      MessageBox.Create(GetCanvas(), "Exit Match?", MessageBox.EType.YES_NO, 
        () => { OnExitMatch(true); }, 
        () => { OnExitMatch(false); });
    }

    //-----------------------------------------------------------------------------------
    public void OnExitMatch(bool bRes)
    {
      if (bRes)
      {
        StatsProviderBase sp = Game.instance.stats;
        sp.GetConfig().m_nGamesPlayed++;
        sp.Save();
        Game.instance.ui.SwitchToState(new MainMenuParams());
      }
      else
      {
        m_player.glass.Pause(false);
      }
    }
  }
}