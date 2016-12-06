using UnityEngine;
using Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;


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

  public class NetManager
  {

    //-----------------------------------------------------------------------------------
    // Use this for initialization
    public void Init()
    {

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
      return PhotonNetwork.ConnectToBestCloudServer(Game.Instance.GetConfig().gameVersion);
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







    EGameType       m_eCurGameType = EGameType.UNDEFINED;
    NetActionResult m_cbEnterMatch = null;

  }
}