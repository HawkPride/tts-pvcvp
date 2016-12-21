using System;
using Net;
using Math;

//-----------------------------------------------------------------------------------
namespace Logic
{
  public class GlassRemote : Glass
  {
    //-----------------------------------------------------------------------------------
    protected override void ProcessOneStep()
    {
      DrawCurrentFigure(false);
      if (m_curFigure != null)
      {
        VecInt2 vNewPos = new VecInt2(m_curFigure.pos.x, m_curFigure.pos.y - 1);
        if (CheckFigurePos(vNewPos))
          m_curFigure.pos = vNewPos;
        //Wait for end step from remote client
      }
      //Draw anyway
      DrawCurrentFigure(true);
    }

    //-----------------------------------------------------------------------------------
    public void NewFigure(Figure.EType eType, Net.PosRot curFigurePos)
    {
      DrawCurrentFigure(false);
      m_curFigure = new Figure(eType);
      SetPos(ref curFigurePos);
      DrawCurrentFigure(true);
    }

    //-----------------------------------------------------------------------------------
    public void SetPos(Net.PosRot curFigurePos, bool bFinal)
    {
      DrawCurrentFigure(false);
      SetPos(ref curFigurePos);
      DrawCurrentFigure(true);
      if (bFinal)
      {
        m_curFigure = null;
        if (FindFullLines())
          CollapseLines();
      }
    }

    //-----------------------------------------------------------------------------------
    void SetPos(ref Net.PosRot curFigurePos)
    {
      if (m_curFigure != null)
      {
        m_curFigure.pos.x = curFigurePos.x;
        m_curFigure.pos.y = curFigurePos.y;
        m_curFigure.rot   = curFigurePos.r;
      }
    }
  }
}
