using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Net;

namespace GameGUI.States
{
  //Creation params
  public class WaitingGameParams : GameStateParams
  {
    public override string GetSceneName() { return "WaitingGame"; }
    public override EGameStateType GetStateType() { return EGameStateType.WAITING_GAME; }
    public WaitingGameParams() {}
  }


  public class WaitingGame : GameState
  {

    public Text m_textPlayersCount;

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.WAITING_GAME;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      NetManager net = Game.instance.netMan;
      net.m_delConnected += OnConnected;
      net.m_delEnterMatch += OnEnteredRoom;

      Game.instance.netMan.Connect();

    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {
      const int MAX_COUNT = 2;
      int nCurrCount = 0;
      Room room = PhotonNetwork.room;
      if (room != null)
      {
        nCurrCount = room.playerCount;
         if (room.playerCount == MAX_COUNT)
        {
          PhotonNetwork.isMessageQueueRunning = false;
          Game.instance.ui.SwitchToState(new GamePvp1x1Params());
        }
      }

      m_textPlayersCount.text = nCurrCount.ToString() + "/" + MAX_COUNT.ToString();
    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {
      Unsubscribe();
    }

    //-----------------------------------------------------------------------------------
    public void OnCancel()
    {
      Game.instance.netMan.ExitMatch();
      Unsubscribe();
      Game.instance.ui.SwitchToState(new MainMenuParams());
    }

    //-----------------------------------------------------------------------------------
    void OnConnected(bool bRes)
    {
      if (bRes)
        Game.instance.netMan.EnterMatch(EGameType.PvP1x1);
      else
        Disconnect();
    }

    //-----------------------------------------------------------------------------------
    void OnEnteredRoom(bool bRes)
    {
      if (bRes)
      {
        //Switch to game
        
      }
      else
        Disconnect();
    }

    //-----------------------------------------------------------------------------------
    void Disconnect()
    {
      Game.instance.netMan.Disconnect();
      Unsubscribe();
      MessageBox.Create(GetCanvas(), "Connection lost", MessageBox.EType.OK, 
        () => { Game.instance.ui.SwitchToState(new MainMenuParams()); } );
    }

    //-----------------------------------------------------------------------------------
    void Unsubscribe()
    {
      NetManager net = Game.instance.netMan;
      net.m_delConnected -= OnConnected;
      net.m_delEnterMatch -= OnEnteredRoom;
    }
  }

}