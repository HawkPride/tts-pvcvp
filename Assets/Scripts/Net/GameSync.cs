using Math;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Net
{
  [Serializable]
  public struct GameSyncPos
  {
    public int posX;
    public int posY;
    public int rot;
  };

  public interface IGameSyncInterface
  {
    void NewFigure(Logic.Figure.EType eType, GameSyncPos curFigurePos);
    void SendPos  (GameSyncPos curFigurePos, bool bFinal);
  }

  public class GameSync : Photon.MonoBehaviour, IGameSyncInterface
  {

    HashSet<IGameSyncInterface> m_setReceivers = new HashSet<IGameSyncInterface>();

    public void AddReceiver(IGameSyncInterface res)
    {
      m_setReceivers.Add(res);
    }

    public void RemoveReceiver(IGameSyncInterface res)
    {
      m_setReceivers.Remove(res);
    }

    public virtual void NewFigure(Logic.Figure.EType eType, GameSyncPos curFigurePos)
    {
      photonView.RPC("OnNewFigure", PhotonTargets.Others, eType, curFigurePos);
    }

    [PunRPC]
    void OnNewFigure(Logic.Figure.EType eType, GameSyncPos curFigurePos)
    {
      foreach (var it in m_setReceivers)
        it.NewFigure(eType, curFigurePos);
    }

    public virtual void SendPos(GameSyncPos curFigurePos, bool bFinal)
    {
      photonView.RPC("OnSendPos", PhotonTargets.Others, curFigurePos, bFinal);
    }

    [PunRPC]
    void OnSendPos(GameSyncPos curFigurePos, bool bFinal)
    {
      foreach (var it in m_setReceivers)
        it.SendPos(curFigurePos, bFinal);
    }

    public void SendData(int nData)
    {
      photonView.RPC("OnSendData", PhotonTargets.Others, nData);
    }

    [PunRPC]
    void OnSendData(int nData)
    {
      Debug.Log("OnSendData " + nData.ToString());
    }
  }
}
