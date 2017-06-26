using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Net;

namespace GameGUI.States
{
  //Creation params
  public class WaitingGameParams : GameStateParams
  {
    public WaitingGameParams(EGameType eRequestedGameType) { m_eRequestedGameType = eRequestedGameType; }
    public WaitingGameParams() : this(EGameType.UNDEFINED) { }

    public override string GetSceneName() { return "WaitingGame"; }
    public override EGameStateType GetStateType() { return EGameStateType.WAITING_GAME; }
    
    public EGameType m_eRequestedGameType;
  }


  public class WaitingGame : GameStateImplTpl<WaitingGameParams>
  {

    public Text m_textPlayersCount;

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
          Game.instance.ui.SwitchToState(new GamePvp1x1Params(false));
        }
      }

      m_textPlayersCount.text = PhotonNetwork.ServerAddress;// nCurrCount.ToString() + "/" + MAX_COUNT.ToString();
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
        Game.instance.netMan.EnterMatch(GetParams().m_eRequestedGameType);
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