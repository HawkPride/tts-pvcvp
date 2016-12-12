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
      NetManager net = Game.Instance.NetMan;
      net.m_delConnected += OnConnected;
      net.m_delEnterMatch += OnEnteredRoom;

      Game.Instance.NetMan.Connect();

    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {


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
        Game.Instance.NetMan.EnterMatch(EGameType.PvP1x1);
      else
        Disconnect();
    }

    //-----------------------------------------------------------------------------------
    void OnEnteredRoom(bool bRes)
    {
      if (bRes)
      {
        //Switch to game
        Game.Instance.Ui.SwitchToState(new GamePvp1x1Params());
      }
      else
        Disconnect();
    }

    //-----------------------------------------------------------------------------------
    void Disconnect()
    {
      Game.Instance.NetMan.Disconnect();
      Unsubscribe();
      MessageBox.Create(GetCanvas(), "Connection lost", MessageBox.EType.OK, 
        () => { Game.Instance.Ui.SwitchToState(new MainMenuParams()); } );
    }

    //-----------------------------------------------------------------------------------
    void Unsubscribe()
    {
      NetManager net = Game.Instance.NetMan;
      net.m_delConnected -= OnConnected;
      net.m_delEnterMatch -= OnEnteredRoom;
    }
  }

}