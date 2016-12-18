using UnityEngine;
using Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;

namespace Net
{
  public enum EGameType
  {
    UNDEFINED,
    PvP1x1,
    PvP2x2,
    PvP3x3,
  };

  public delegate void NetActionResult(bool bRes);

  public class NetManager : IPunCallbacks
  {
    public NetActionResult m_delConnected  = null;
    public NetActionResult m_delEnterMatch = null;


    //-----------------------------------------------------------------------------------
    // Use this for initialization
    public void Init()
    {
      PhotonNetwork.messageListener = this;
      CustomTypes.Register();
    }

    //-----------------------------------------------------------------------------------
    // Use this for finalization
    public void End()
    {
      PhotonNetwork.messageListener = null;
    }

    //-----------------------------------------------------------------------------------
    // Update is called once per frame
    public void Update()
    {

    }

    //-----------------------------------------------------------------------------------
    public bool Connect()
    {
      m_eCurGameType = EGameType.UNDEFINED;
      //Already connected
      if (PhotonNetwork.connected)
        return true;
      return PhotonNetwork.ConnectUsingSettings(Game.Instance.GetConfig().gameVersion);
    }

    //-----------------------------------------------------------------------------------
    public void Disconnect()
    {
      if (!IsConnected())
        return;
      PhotonNetwork.Disconnect();
    }

    //-----------------------------------------------------------------------------------
    public bool IsConnected()
    {
      return PhotonNetwork.connected;
    }

    //-----------------------------------------------------------------------------------
    public bool EnterMatch(EGameType eType)
    {
      Debug.Assert(m_eCurGameType == EGameType.UNDEFINED);
      if (PhotonNetwork.connectionStateDetailed != ClientState.ConnectedToMaster)
      {
        Debug.Log("EnterMatch: the game is not connected to master");
        return false;
      }
      m_eCurGameType = eType;

      TypedLobby lobby = new TypedLobby(eType.ToString(), LobbyType.AsyncRandomLobby);
      return PhotonNetwork.JoinLobby(lobby);
    }

    //-----------------------------------------------------------------------------------
    void SuccesfullyEnteredRoom()
    {
      if (m_delEnterMatch != null)
        m_delEnterMatch(true);
    }

    //-----------------------------------------------------------------------------------
    void FailedEnterRoom()
    {
      m_eCurGameType = EGameType.UNDEFINED;
      if (PhotonNetwork.insideLobby)
        PhotonNetwork.LeaveLobby();
      else
      {
        if (m_delEnterMatch != null)
          m_delEnterMatch(false);
      }
    }

    //-----------------------------------------------------------------------------------
    //Photon callbacks
    public void OnConnectedToPhoton()
    {

    }

    public void OnLeftRoom()
    {
    }

    public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
    }

    public void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
      FailedEnterRoom();
    }

    public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
      FailedEnterRoom();
    }

    public void OnCreatedRoom()
    {
      SuccesfullyEnteredRoom();
    }

    public void OnJoinedLobby()
    {
      Hashtable roomProps = new Hashtable();
      PhotonNetwork.JoinRandomRoom(roomProps, 0);
    }

    public void OnLeftLobby()
    {
      //FailedEnterRoom();
    }

    public void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
      if (m_delConnected != null)
        m_delConnected(false);
    }

    public void OnConnectionFail(DisconnectCause cause)
    {
      FailedEnterRoom();
      if (m_delConnected != null)
        m_delConnected(false);
    }

    public void OnDisconnectedFromPhoton()
    {
      if (m_delConnected != null)
        m_delConnected(false);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
    }

    public void OnReceivedRoomListUpdate()
    {
    }

    public void OnJoinedRoom()
    {
      SuccesfullyEnteredRoom();
    }

    public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
    }

    public void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
      //Int16 nReturnCode = (Int16)codeAndMsg[0];
      //string strMessage = (string)codeAndMsg[1];
      if (PhotonNetwork.insideLobby == true)
      {
        //Create room
        RoomOptions opts = new RoomOptions();
        switch (m_eCurGameType)
        {
        case EGameType.PvP1x1:
          opts.MaxPlayers = 2;
          break;
        case EGameType.PvP2x2:
          opts.MaxPlayers = 4;
          break;
        case EGameType.PvP3x3:
          opts.MaxPlayers = 6;
          break;
        default:
          Debug.LogError("Unexpected game type " + m_eCurGameType.ToString());
          break;
        }
        opts.IsOpen = true;
        opts.PlayerTtl = 1000;
        opts.EmptyRoomTtl = 1000;

        PhotonNetwork.CreateRoom(null, opts, null);
      }
      else
        FailedEnterRoom();
    }

    public void OnConnectedToMaster()
    {
      if (m_delConnected != null)
        m_delConnected(true);
    }

    public void OnPhotonMaxCccuReached()
    {
    }

    public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
    }

    public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
    }

    public void OnUpdatedFriendList()
    {
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
    }

    public void OnWebRpcResponse(OperationResponse response)
    {
    }

    public void OnOwnershipRequest(object[] viewAndPlayer)
    {
    }

    public void OnLobbyStatisticsUpdate()
    {
    }

    //End of Photon callbacks

    EGameType       m_eCurGameType = EGameType.UNDEFINED;

  }
}