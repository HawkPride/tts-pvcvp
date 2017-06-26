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
    public delegate void OnLineAdded(bool[] arBusy);
    
    public GlassEvent   m_delChangePos;
    public GlassEvent   m_delFigurePlaced;
    public OnLineAdded  m_delLineAdded;

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
    public override void AddOneLine(bool[] arBusy)
    {
      base.AddOneLine(arBusy);

      if (m_delLineAdded != null)
        m_delLineAdded(arBusy);
    }

    //-----------------------------------------------------------------------------------
    protected override void ProcessOneStep()
    {
      if (figure != null)
      {
        DrawCurrentFigure(false);
      }
      else
      {
        //1 for x cause it would be subtracted next lines
        VecInt2 vNextPos = new VecInt2(m_nSizeX/2, m_nSizeY);

        //Check round end
        if (SetNewFigure(m_nextFigure, vNextPos, m_nextFigure.rot))
        {
          CreateNextFigure();
        }
        else
        {
          GameEnd();
          return;
        }
      }

      VecInt2 vNewPos = new VecInt2(figure.pos.x, figure.pos.y - 1);
      bool bStillGo = CheckFigurePos(vNewPos);
      if (bStillGo)
      {
        figure.pos = vNewPos;
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
        SetNewFigure(null, null, 0);
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
      if (figure == null)
        return;
      VecInt2 vOldPos = new VecInt2(figure.pos);
      int nOldRot = figure != null ? figure.rot : 0;

      ProcessInput(nKey, bDown);

      int nRot = figure != null ? figure.rot : 0;
      if (vOldPos != figure.pos || nOldRot != nRot)
        if (m_delChangePos != null)
          m_delChangePos();
    }
  }
}
