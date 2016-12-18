using UnityEngine;
using Math;

//-----------------------------------------------------------------------------------
namespace Logic
{
  public class GlassLocal : Glass
  {
    //Private
    Figure        m_nextFigure;
    Net.GameSync  m_netSync;

    public Figure NextFigure { get { return m_nextFigure; } }


    //-----------------------------------------------------------------------------------
    public GlassLocal(Net.GameSync netSync)
    {
      m_netSync = netSync;
    }

    //-----------------------------------------------------------------------------------
    public override void Init()
    {
      base.Init();

      CreateNextFigure();
    }

    //-----------------------------------------------------------------------------------
    protected override void ProcessOneStep()
    {
      if (m_curFigure != null)
      {
        DrawCurrentFigure(false);
      }
      else
      {
        //Assert.IsNotNull(m_nextFigure);
        m_curFigure = m_nextFigure;
        //1 for x cause it would be subtracted next lines
        VecInt2 vNextPos = new VecInt2(m_nSizeX/2, m_nSizeY);

        //Check round end
        if (CheckFigurePos(vNextPos))
        {
          CreateNextFigure();
          m_vCurFigurePos = vNextPos;


          if (m_netSync != null)
            m_netSync.NewFigure(m_curFigure.Type, GetPos());
        }
        else
        {
          m_bEnd = true;
          if (m_delGameEnd != null)
            m_delGameEnd();
          return;
        }
      }

      VecInt2 vNewPos = new VecInt2(m_vCurFigurePos.x, m_vCurFigurePos.y - 1);
      bool bStillGo = CheckFigurePos(vNewPos);
      if (bStillGo)
        m_vCurFigurePos = vNewPos;
      //Draw anyway
      DrawCurrentFigure(true);
      //The figure is in place
      if (!bStillGo)
      {
        if (m_netSync != null)
          m_netSync.SendPos(GetPos(), true);
        m_curFigure = null;
        if (FindFullLines())
          CollapseLines();
      }
    }


    //-----------------------------------------------------------------------------------
    void CreateNextFigure()
    {
      Figure.EType eType = (Figure.EType)Random.Range(0, (int)Figure.EType.COUNT);
      //Figure.EType eType = Figure.EType.BOX;
      m_nextFigure = new Figure(eType);
    }


    //-----------------------------------------------------------------------------------
    //Input
    //-----------------------------------------------------------------------------------
    public void OnInputEvent(int nKey, bool bDown)
    {
      ProcessInput(nKey, bDown);

      if (m_netSync != null)
        m_netSync.SendPos(GetPos(), false);
    }

    //-----------------------------------------------------------------------------------
    Net.GameSyncPos GetPos()
    {
      Net.GameSyncPos data;
      data.posX = m_vCurFigurePos.x;
      data.posY = m_vCurFigurePos.y;
      data.rot = m_curFigure.Rotation;
      return data;
    }
  }
}
