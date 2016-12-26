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

    public Net.PlayerGlass      m_localPlayer;
    public Net.PlayerGlass      m_otherPlayer;
    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.GAME_PVP_1x1;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      PhotonNetwork.isMessageQueueRunning = true;

      GameObject go = PhotonNetwork.Instantiate("PlayerGlass", Vector3.zero, Quaternion.identity, 0);
      if (go != null)
        m_localPlayer = go.GetComponent<Net.PlayerGlass>();
      if (m_localPlayer == null)
      {
        Debug.LogError("Unable to create player");
        return;
      }
      Logic.Glass glass = m_localPlayer.glass;

      glass.m_delGameEnd += (() => { OnGameEnd(false); });


      if (m_glassRend != null)
        m_glassRend.m_glass = glass;
      if (m_glassPrev != null)
        m_glassPrev.m_glass = glass;

    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {
      UpdateOtherPlayer();

    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {

      PhotonNetwork.Disconnect();
    }

    //-----------------------------------------------------------------------------------
    void OnOpponentRowDeleted(int nCount)
    {
      int nSize = m_localPlayer.glass.m_nSizeX;
      bool[] arBusy = new bool[nSize];

      for (int i = 0; i < nCount; i++)
      {
        bool bHasEmpty = false;
        for (int x = 0; x < nSize; x++)
        {
          arBusy[x] = (UnityEngine.Random.value < 0.8f);
          if (!arBusy[x])
            bHasEmpty = true;
        }
        if (!bHasEmpty)
          arBusy[0] = false;
        m_localPlayer.glass.AddOneLine(arBusy);
      }
    }

    //-----------------------------------------------------------------------------------
    void OnGameEnd(bool bOther)
    {
      if (bOther)
        GameMessage.Create(GetCanvas(), "You Won!", OnGameOver, 3.0f);
      else
        GameMessage.Create(GetCanvas(), "You Lost!", OnGameOver, 3.0f);

      /*StatsProviderBase sp = Game.Instance.Stats;
      sp.m_nGamesPlayed++;
      //New score is not in hi score
      if (sp.GetNewScoreIndex(m_ownerGlass.Score) < 0 || m_ownerGlass.Score == 0)
        GameMessage.Create(GetCanvas(), "Game Over", OnGameOver, 1.5f);
      else
        GameMessage.Create(GetCanvas(), "New High Score", OnHighScore, 1.5f);*/

    }

    //-----------------------------------------------------------------------------------
    void OnHighScore()
    {
      /*GameResults res = new GameResults();
      res.Score = m_ownerGlass.Score;
      Game.Instance.Results = res;
      Game.Instance.Ui.SwitchToState(new PlayersScoreParams(m_ownerGlass.Score));*/
    }

    //-----------------------------------------------------------------------------------
    void OnGameOver()
    {
      /*Game.Instance.Stats.Save();*/
      Game.instance.ui.SwitchToState(new MainMenuParams());
    }

    //-----------------------------------------------------------------------------------
    void UpdateOtherPlayer()
    {
      if (m_otherPlayer != null)
        return;

      PhotonPlayer player = PhotonNetwork.player;
      if (player == null)
        return;

      foreach (var it in Game.instance.netMan.players)
      {
        if (it.photonView.ownerId != player.ID)
        {
          m_otherPlayer = it;
          m_otherPlayer.glass.m_delGameEnd += (() => { OnGameEnd(true); });
          m_otherPlayer.glass.m_delRowDeleted += OnOpponentRowDeleted;
          if (m_glassOpponentRend != null)
            m_glassOpponentRend.m_glass = m_otherPlayer.glass;

          return;
        }

      }
    }
  }
}