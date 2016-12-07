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

    //-----------------------------------------------------------------------------------
    // Use this for initialization
    public void Init()
    {
      PhotonNetwork.messageListener = this;
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

    public bool Connect()
    {
      m_eCurGameType = EGameType.UNDEFINED;
      //Already connected
      if (PhotonNetwork.connected)
        return true;
      return PhotonNetwork.ConnectUsingSettings(Game.Instance.GetConfig().gameVersion);
    }

    //-----------------------------------------------------------------------------------
    public bool EnterMatch(EGameType eType, NetActionResult resultCallback)
    {
      Debug.Assert(m_eCurGameType == EGameType.UNDEFINED);
      if (PhotonNetwork.connectionStateDetailed != ClientState.ConnectedToMaster)
      {
        Debug.Log("EnterMatch: the game is not connected to master");
        return false;
      }
      m_cbEnterMatch = resultCallback;
      m_eCurGameType = eType;

      TypedLobby lobby = new TypedLobby(eType.ToString(), LobbyType.AsyncRandomLobby);
      return PhotonNetwork.JoinLobby(lobby);
    }

    //-----------------------------------------------------------------------------------
    void OnSuccesfullyEnteredRoom()
    {
      if (m_cbEnterMatch != null)
      {
        m_cbEnterMatch.Invoke(true);
      }
      m_cbEnterMatch = null;
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
    }

    public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
      

    }

    public void OnCreatedRoom()
    {
      OnSuccesfullyEnteredRoom();
    }

    public void OnJoinedLobby()
    {
      Hashtable roomProps = new Hashtable();

      PhotonNetwork.JoinRandomRoom(roomProps, 2);
    }

    public void OnLeftLobby()
    {
    }

    public void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
    }

    public void OnConnectionFail(DisconnectCause cause)
    {
    }

    public void OnDisconnectedFromPhoton()
    {
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
    }

    public void OnReceivedRoomListUpdate()
    {
    }

    public void OnJoinedRoom()
    {
      OnSuccesfullyEnteredRoom();
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
        opts.MaxPlayers = 2;
        opts.PlayerTtl = 1000;
        opts.EmptyRoomTtl = 0;

        PhotonNetwork.CreateRoom(null, opts, null);
      }
      else if (m_cbEnterMatch != null)
      {
        m_cbEnterMatch.Invoke(false);
      }
      m_cbEnterMatch = null;
      m_eCurGameType = EGameType.UNDEFINED;
    }

    public void OnConnectedToMaster()
    {
      EnterMatch(EGameType.PvP1x1, null);
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
    NetActionResult m_cbEnterMatch = null;

  }
}