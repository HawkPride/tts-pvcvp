using System;
using Net;
using Math;

//-----------------------------------------------------------------------------------
namespace Logic
{
  public class GlassRemote : Glass, Net.IGameSyncInterface
  {
    Net.GameSync  m_netSync;

    //-----------------------------------------------------------------------------------
    public GlassRemote(Net.GameSync netSync)
    {
      m_netSync = netSync;
      m_netSync.AddReceiver(this);
    }

    //-----------------------------------------------------------------------------------
    protected override void ProcessOneStep()
    {
      DrawCurrentFigure(false);
      m_vCurFigurePos = new VecInt2(m_vCurFigurePos.x, m_vCurFigurePos.y - 1);
      //Draw anyway
      DrawCurrentFigure(true);
    }

    //-----------------------------------------------------------------------------------
    public virtual void NewFigure(Figure.EType eType, GameSyncPos curFigurePos)
    {
      DrawCurrentFigure(false);
      m_curFigure = new Figure(eType);
      SetPos(curFigurePos);
      DrawCurrentFigure(true);
    }

    //-----------------------------------------------------------------------------------
    public virtual void SendPos(GameSyncPos curFigurePos, bool bFinal)
    {
      DrawCurrentFigure(false);
      SetPos(curFigurePos);
      DrawCurrentFigure(true);
      if (bFinal)
      {
        m_curFigure = null;
        if (FindFullLines())
          CollapseLines();
      }
    }

    //-----------------------------------------------------------------------------------
    void SetPos(GameSyncPos pos)
    {
      m_vCurFigurePos.x = pos.posX;
      m_vCurFigurePos.y = pos.posY;
      if (m_curFigure != null)
        m_curFigure.Rotation = pos.rot;
    }
  }
}
