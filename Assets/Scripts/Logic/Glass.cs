using UnityEngine;
using System.Collections;
using Math;
using UnityEngine.Assertions;

public class Glass : MonoBehaviour
{
  //Private
  Block[,]      m_arField;
  Figure        m_curFigure;
  Figure        m_nextFigure;
  VecInt2       m_vCurFigurePos = new VecInt2(0, 0);
  bool          m_bEnd;
  TimeInterval  m_stepTimer;
  int           m_nCurrPoints = 0;
  int           m_nTotalRawsDone = 0;

  //Public

  public int    m_nSizeX              = 11;
  public int    m_nSizeY              = 25;
  public float  m_fStartStep          = 0.8f;
  public float  m_fDifficultyMult     =  0.8f;
  public int    m_nDifficultyLinesInc = 5; 

  public Block[,] Field   { get { return m_arField;  } }
  public int      Points  { get { return m_nCurrPoints; } }
  public Figure   NextFigure { get { return m_nextFigure; } }

  public float    FrameTime
  {
    get { return m_stepTimer.Interval;  }
    set { m_stepTimer.Interval = value; }
  }

  //Events
  public delegate void OnGameEnd();
  public delegate void OnRowDeleted(int nCount);

  public OnGameEnd    m_listenerGameEnd;
  public OnRowDeleted m_listenerRowDeleted;

  //Unity Events
  //-----------------------------------------------------------------------------------
  // Use this for initialization
  //-----------------------------------------------------------------------------------
  public void Start()
  {
    InputProvider ip = GetComponent<InputProvider>();
    if (ip != null)
      ip.m_eventListeners += OnInputEvent;

    m_nCurrPoints = 0;
    m_arField     = new Block[m_nSizeX, m_nSizeY];
    m_stepTimer   = new TimeInterval(m_fStartStep);
    m_bEnd        = false;

    CreateNextFigure();
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
  public void Update()
  {
    if (m_bEnd)
      return;

    if (!m_stepTimer.StartNewInterval())
      return;

    ProcessOneStep();
  }

  //-----------------------------------------------------------------------------------
  public void OnDestroy()
  {
    InputProvider ip = GetComponent<InputProvider>();
    if (ip != null)
      ip.m_eventListeners -= OnInputEvent;
  }

  //-----------------------------------------------------------------------------------
  //Input
  //-----------------------------------------------------------------------------------
  void OnInputEvent( int nKey, bool bDown )
  {
    ProcessInput(nKey, bDown);
  }



  //-----------------------------------------------------------------------------------
  // Functions
  //-----------------------------------------------------------------------------------
  void ProcessInput( int nKey, bool bDown )
  {
    if (m_bEnd)
      return;
    if (m_curFigure == null)
      return;

    DrawCurrentFigure(false);
    if (nKey == (int)InputProvider.EInputAction.ROTATE && bDown)
    {
      m_curFigure.Rotate(1);
      if (!CheckFigurePos(m_vCurFigurePos))
      {
        //Try move left-right to fit it
        int nSizeX = m_curFigure.Matrix.GetLength(0);
        int nOffsetX = -nSizeX/2;
        bool bOk = false;
        for (int x = 0; x < nSizeX; x++)
        {
          VecInt2 vNewPos = new VecInt2(m_vCurFigurePos.x + x + nOffsetX, m_vCurFigurePos.y);
          if (CheckFigurePos(vNewPos))
          {
            m_vCurFigurePos = vNewPos;
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
      VecInt2 vNewPos = new VecInt2(m_vCurFigurePos.x - 1, m_vCurFigurePos.y);
      if (CheckFigurePos(vNewPos))
        m_vCurFigurePos = vNewPos;
    }
    else if (nKey == (int)InputProvider.EInputAction.RIGHT && bDown)
    {
      VecInt2 vNewPos = new VecInt2(m_vCurFigurePos.x + 1, m_vCurFigurePos.y);
      if (CheckFigurePos(vNewPos))
        m_vCurFigurePos = vNewPos;
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
  void UpdateTimeInterval()
  {
    int nDiff = m_nTotalRawsDone/m_nDifficultyLinesInc;
    FrameTime = m_fStartStep * Mathf.Pow(m_fDifficultyMult, nDiff);
  }

  
  //-----------------------------------------------------------------------------------
  void CreateNextFigure()
  {
    Figure.EType eType = (Figure.EType)Random.Range(0, (int)Figure.EType.COUNT);
    //Figure.EType eType = Figure.EType.BOX;
    m_nextFigure = new Figure(eType);
  }



  //-----------------------------------------------------------------------------------
  void ProcessOneStep()
  {
    if (m_curFigure != null)
    {
      DrawCurrentFigure(false);
    }
    else
    {
      //Assert.IsNotNull(m_nextFigure);
      m_curFigure = m_nextFigure;
      //1 for x cause it would be substracted next lines
      VecInt2 vNextPos = new VecInt2(m_nSizeX/2, m_nSizeY);

      //Check round end
      if (CheckFigurePos(vNextPos))
      {
        CreateNextFigure();
        m_vCurFigurePos = vNextPos;
      }
      else
      {
        m_bEnd = true;
        if (m_listenerGameEnd != null)
          m_listenerGameEnd();
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
      m_curFigure = null;
      if (FindFullLines())
        CollapseLines();
    }
  }


  //-----------------------------------------------------------------------------------
  void DrawCurrentFigure(bool bDraw)
  {
    if (m_curFigure == null)
      return;

    VecInt2 vStartPos = m_vCurFigurePos - m_curFigure.GetCenterPoint();

    var mtx = m_curFigure.Matrix;
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
  bool CheckFigurePos(VecInt2 vCurrPos)
  {
    if (m_curFigure == null)
      return false;

    VecInt2 vStartPos = vCurrPos - m_curFigure.GetCenterPoint();

    var mtx = m_curFigure.Matrix;
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
  bool FindFullLines()
  {
    int nPoints = 2;
    int nLinesFound = 0;
    for (int y = 0; y < m_nSizeY; y++ )
    {
      int nNumFullBlocks = 0;
      for (int x = 0; x < m_nSizeX; x++)
      {
        Block blk = Field[x, y];
        if (blk != null &&
          blk.m_eType != Block.EType.COLLAPSING)
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
          Field[x, y].m_eType = Block.EType.COLLAPSING;
      }
    }

    if (nLinesFound > 0)
    {
      m_nCurrPoints += nLinesFound * nPoints;
      m_nTotalRawsDone += nLinesFound;
      UpdateTimeInterval();

      if (m_listenerRowDeleted != null)
        m_listenerRowDeleted(nLinesFound);
      return true;
    }
    return false;
  }

  //-----------------------------------------------------------------------------------
  void CollapseLines()
  {

    for (int x = 0; x < m_nSizeX; x++)
    {
      int nTargetY = 0;
      int nSourceY = 0;
      while (nTargetY < m_nSizeY)
      {
        Block srcBlock = null;
        if (nSourceY < m_nSizeY)
          srcBlock = Field[x, nSourceY];
        //If this block colapsing, go to next
        if (srcBlock != null && srcBlock.m_eType == Block.EType.COLLAPSING)
          nSourceY++;
        else
        {
          Field[x, nTargetY] = srcBlock;
          nSourceY++;
          nTargetY++;
        }
      }
    }
  }

}
