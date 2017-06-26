using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

namespace GameGUI.States
{
  //Creation params
  public class GamePvp1x1Params : GameStateParams
  {
    public GamePvp1x1Params()
      : this(false)
    {
    }

    public GamePvp1x1Params(bool bAiMode)
    {
      m_bAiMode = bAiMode;
    }

    public override string          GetSceneName() { return "GamePvp1x1"; }
    public override EGameStateType  GetStateType() { return EGameStateType.GAME_PVP_1x1; }

    public bool m_bAiMode;
  }




  public class GamePvp1x1 : GameStateImplTpl<GamePvp1x1Params>
  {
    public BlockRendererGlass   m_glassRend;
    public BlockRendererPreview m_glassPrev;

    public BlockRendererGlass   m_glassOpponentRend;

    public Text                 m_enemyName;

    Net.PlayerGlass      m_localPlayer;
    Net.PlayerGlass      m_otherPlayer;

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
      int nEmptyCount = (int)(nSize * 0.2f);

      for (int i = 0; i < nCount; i++)
      {
        for (int x = 0; x < nSize; x++)
          arBusy[x] = true;

        int nEmptyAdded = 0;
        while (nEmptyAdded < nEmptyCount)
        {
          int nPlace = UnityEngine.Random.Range(0, nSize - 1);
          if (arBusy[nPlace])
          {
            arBusy[nPlace] = false;
            nEmptyAdded++;
          }
        }
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
    public void OnExitMatch()
    {
      MessageBox.Create(GetCanvas(), "Exit Match?", MessageBox.EType.YES_NO,
        () => { OnExitMatch(true); },
        () => { OnExitMatch(false); });
    }

    //-----------------------------------------------------------------------------------
    public void OnExitMatch(bool bRes)
    {
      /*if (bRes)
      {
        StatsProviderBase sp = Game.instance.stats;
        sp.GetConfig().m_nGamesPlayed++;
        sp.Save();
        Game.instance.ui.SwitchToState(new MainMenuParams());
      }
      else
      {
        m_player.glass.Pause(false);
      }*/
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


          //Update name
          m_enemyName.text = it.photonView.owner.name;
          return;
        }

      }
    }
  }
}