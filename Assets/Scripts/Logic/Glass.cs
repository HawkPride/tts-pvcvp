using UnityEngine;
using System.Collections;
using Math;
using System;


//-----------------------------------------------------------------------------------
namespace Logic
{
  public abstract class Glass
  {
    //Protected
    protected Block[,]      m_arField;
    protected Figure        m_curFigure;
    protected bool          m_bEnd;
    protected TimeInterval  m_stepTimer;
    protected int           m_nCurrPoints = 0;
    protected int           m_nTotalRawsDone = 0;

    //Public

    public int    m_nSizeX              = 10;
    public int    m_nSizeY              = 20;
    public float  m_fStartStep          = 0.8f;
    public float  m_fDifficultyMult     = 0.8f;
    public int    m_nDifficultyLinesInc = 5;

    public Block[,] field { get { return m_arField; } }
    public int score { get { return m_nCurrPoints; } }
    public Figure figure { get { return m_curFigure; } }

    public float frameTime
    {
      get { return m_stepTimer.interval; }
      set { m_stepTimer.interval = value; }
    }

    //Events
    public delegate void OnGameEnd();
    public delegate void OnRowDeleted(int nCount);

    public OnGameEnd      m_delGameEnd;
    public OnRowDeleted   m_delRowDeleted;

    //Unity Events
    //-----------------------------------------------------------------------------------
    // Use this for initialization
    //-----------------------------------------------------------------------------------
    public virtual void Init()
    {
      m_nCurrPoints = 0;
      m_arField = new Block[m_nSizeX, m_nSizeY];
      m_stepTimer = new TimeInterval(m_fStartStep);
      m_bEnd = false;

      /*
      //TODO: Test
      for (int i = 0; i < m_arField.GetLength(0); i++)
        for (int j = 0; j < m_arField.GetLength(1); j++)
          if (Random.value < 0.5f)
            m_arField[i, j] = new Block();

      //m_curFigure = new Figure(Figure.EType.L);*/
    }


    //-----------------------------------------------------------------------------------
    // Update is called once per frame
    //-----------------------------------------------------------------------------------
    public virtual void Update()
    {
      if (m_bEnd)
        return;

      if (!m_stepTimer.StartNewInterval())
        return;

      ProcessOneStep();
    }

    //-----------------------------------------------------------------------------------
    protected abstract void ProcessOneStep();



    //-----------------------------------------------------------------------------------
    // Functions
    //-----------------------------------------------------------------------------------
    public void ProcessInput(int nKey, bool bDown)
    {
      if (m_bEnd)
        return;
      if (m_curFigure == null)
        return;

      DrawCurrentFigure(false);
      if (nKey == (int)InputProvider.EInputAction.ROTATE && bDown)
      {
        m_curFigure.Rotate(1);
        if (!CheckFigurePos(m_curFigure.pos))
        {
          //Try move left-right to fit it
          int nSizeX = m_curFigure.matrix.GetLength(0);
          int nOffsetX = -nSizeX/2;
          bool bOk = false;
          for (int x = 0; x < nSizeX; x++)
          {
            VecInt2 vNewPos = new VecInt2(m_curFigure.pos.x + x + nOffsetX, m_curFigure.pos.y);
            if (CheckFigurePos(vNewPos))
            {
              m_curFigure.pos = vNewPos;
              bOk = true;
              break;
            }
          }
          if (!bOk)
            m_curFigure.Rotate(-1);
        }
      }
      else if (nKey == (int)InputProvider.EInputAction.LEFT && bDown)
      {
        VecInt2 vNewPos = new VecInt2(m_curFigure.pos.x - 1, m_curFigure.pos.y);
        if (CheckFigurePos(vNewPos))
          m_curFigure.pos = vNewPos;
      }
      else if (nKey == (int)InputProvider.EInputAction.RIGHT && bDown)
      {
        VecInt2 vNewPos = new VecInt2(m_curFigure.pos.x + 1, m_curFigure.pos.y);
        if (CheckFigurePos(vNewPos))
          m_curFigure.pos = vNewPos;
      }
      else if (nKey == (int)InputProvider.EInputAction.DOWN)
      {
        if (bDown)
        {
          ProcessOneStep();
          m_stepTimer.Reset();
        }
        //UpdateTimeInterval();
      }

      DrawCurrentFigure(true);
    }

    //-----------------------------------------------------------------------------------
    protected void UpdateTimeInterval()
    {
      int nDiff = m_nTotalRawsDone/m_nDifficultyLinesInc;
      frameTime = m_fStartStep * Mathf.Pow(m_fDifficultyMult, nDiff);
    }



    //-----------------------------------------------------------------------------------
    protected void DrawCurrentFigure(bool bDraw)
    {
      if (m_curFigure == null)
        return;

      VecInt2 vStartPos = m_curFigure.pos - m_curFigure.GetCenterPoint();

      var mtx = m_curFigure.matrix;
      for (int x = 0; x < mtx.GetLength(0); x++)
        for (int y = 0; y < mtx.GetLength(1); y++)
        {
          int nActX = x + vStartPos.x;
          if (nActX < 0 || nActX >= m_nSizeX)
            continue;
          int nActY = y + vStartPos.y;
          if (nActY < 0 || nActY >= m_nSizeY)
            continue;

          if (mtx[x, y] == 0)
            continue;

          if (bDraw)
            m_arField[nActX, nActY] = new Block();
          else
            m_arField[nActX, nActY] = null;
        }
    }

    //-----------------------------------------------------------------------------------
    //Is given pos not taken
    //-----------------------------------------------------------------------------------
    protected bool CheckFigurePos(VecInt2 vCurrPos)
    {
      if (m_curFigure == null)
        return false;

      VecInt2 vStartPos = vCurrPos - m_curFigure.GetCenterPoint();

      var mtx = m_curFigure.matrix;
      for (int x = 0; x < mtx.GetLength(0); x++)
        for (int y = 0; y < mtx.GetLength(1); y++)
        {
          int nActX = x + vStartPos.x;
          if (nActX < 0 || nActX >= m_nSizeX)
            return false;
          int nActY = y + vStartPos.y;
          //No limit from up
          if (nActY < 0/* || nActY >= m_nSizeY*/)
            return false;

          if (nActY >= m_nSizeY)
            continue;

          //No element there
          if (mtx[x, y] == 0)
            continue;

          //Already taken
          if (m_arField[nActX, nActY] != null)
            return false;
        }

      return true;
    }

    //-----------------------------------------------------------------------------------
    protected bool FindFullLines()
    {
      int nPoints = 2;
      int nLinesFound = 0;
      for (int y = 0; y < m_nSizeY; y++)
      {
        int nNumFullBlocks = 0;
        for (int x = 0; x < m_nSizeX; x++)
        {
          Block blk = field[x, y];
          if (blk != null &&
            blk.type != Block.EType.COLLAPSING)
          {
            nNumFullBlocks++;
          }
        }
        if (nNumFullBlocks == m_nSizeX)
        {
          nLinesFound++;
          nPoints *= 2;
          //Mark as collapsing
          for (int x = 0; x < m_nSizeX; x++)
            field[x, y].type = Block.EType.COLLAPSING;
        }
      }

      if (nLinesFound > 0)
      {
        m_nCurrPoints += nLinesFound * nPoints;
        m_nTotalRawsDone += nLinesFound;
        UpdateTimeInterval();

        if (m_delRowDeleted != null)
          m_delRowDeleted(nLinesFound);
        return true;
      }
      return false;
    }

    //-----------------------------------------------------------------------------------
    protected void CollapseLines()
    {

      for (int x = 0; x < m_nSizeX; x++)
      {
        int nTargetY = 0;
        int nSourceY = 0;
        while (nTargetY < m_nSizeY)
        {
          Block srcBlock = null;
          if (nSourceY < m_nSizeY)
            srcBlock = field[x, nSourceY];
          //If this block colapsing, go to next
          if (srcBlock != null && srcBlock.type == Block.EType.COLLAPSING)
            nSourceY++;
          else
          {
            field[x, nTargetY] = srcBlock;
            nSourceY++;
            nTargetY++;
          }
        }
      }
    }

  }
}