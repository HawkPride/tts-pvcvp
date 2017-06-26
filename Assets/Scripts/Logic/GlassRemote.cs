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
      if (figure != null)
      {
        VecInt2 vNewPos = new VecInt2(figure.pos.x, figure.pos.y - 1);
        if (CheckFigurePos(vNewPos))
          figure.pos = vNewPos;
        //Wait for end step from remote client
      }
      //Draw anyway
      DrawCurrentFigure(true);
    }

    //-----------------------------------------------------------------------------------
    public void NewFigure(Figure.EType eType, Net.PosRot curFigurePos)
    {
      DrawCurrentFigure(false);
      SetNewFigure(new Figure(eType), new VecInt2(curFigurePos.x, curFigurePos.y), curFigurePos.r);
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
        SetNewFigure(null, null, 0);
        if (FindFullLines())
          CollapseLines();
      }
    }

    //-----------------------------------------------------------------------------------
    void SetPos(ref Net.PosRot curFigurePos)
    {
      if (figure != null)
      {
        figure.pos.x = curFigurePos.x;
        figure.pos.y = curFigurePos.y;
        figure.rot   = curFigurePos.r;
      }
    }
  }
}
