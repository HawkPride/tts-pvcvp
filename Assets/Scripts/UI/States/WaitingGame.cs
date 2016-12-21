using UnityEngine;
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
      Room room = PhotonNetwork.room;
      if (room != null && room.playerCount == 2)
      {
        PhotonNetwork.isMessageQueueRunning = false;
        Game.instance.ui.SwitchToState(new GamePvp1x1Params());
      }

    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {
      Unsubscribe();
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