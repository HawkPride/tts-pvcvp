using UnityEngine;
using Math;

//-----------------------------------------------------------------------------------
namespace Logic
{
  public class GlassLocal : Glass
  {
    //Private
    Figure        m_nextFigure;

    public Figure nextFigure { get { return m_nextFigure; } }

    //Events
    public delegate void GlassEvent();

    public GlassEvent   m_delNewFigure;
    public GlassEvent   m_delChangePos;
    public GlassEvent   m_delFigurePlaced;

    //-----------------------------------------------------------------------------------
    public GlassLocal()
    {
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
          m_curFigure.pos = vNextPos;

          if (m_delNewFigure != null)
            m_delNewFigure();
        }
        else
        {
          m_bEnd = true;
          if (m_delGameEnd != null)
            m_delGameEnd();
          return;
        }
      }

      VecInt2 vNewPos = new VecInt2(m_curFigure.pos.x, m_curFigure.pos.y - 1);
      bool bStillGo = CheckFigurePos(vNewPos);
      if (bStillGo)
      {
        m_curFigure.pos = vNewPos;
        if (m_delChangePos != null)
          m_delChangePos();
      }
      //Draw anyway
      DrawCurrentFigure(true);
      //The figure is in place
      if (!bStillGo)
      {
        if (m_delFigurePlaced != null)
          m_delFigurePlaced();
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
      if (m_curFigure == null)
        return;
      VecInt2 vOldPos = new VecInt2(m_curFigure.pos);
      int nOldRot = m_curFigure != null ? m_curFigure.rot : 0;

      ProcessInput(nKey, bDown);

      int nRot = m_curFigure != null ? m_curFigure.rot : 0;
      if (vOldPos != m_curFigure.pos || nOldRot != nRot)
        if (m_delChangePos != null)
          m_delChangePos();
    }
  }
}
