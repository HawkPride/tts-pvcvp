using Math;


//-----------------------------------------------------------------------------------
public class Figure
{
  //Static matrices of all figures by type and position
  static int[,][,] s_arMatrixByTypeNRot;

  EType   m_eType;
  int     m_nCurRot = 0;

  public enum EType
  {
    BOX,
    LINE,
    L,
    L_INV,
    S,
    S_INV,
    T,

    LAST = T,
    COUNT
  }
  

  public EType    Type { get { return m_eType; } }

  public int[,]   Matrix { get { return s_arMatrixByTypeNRot[(int)m_eType, m_nCurRot]; } }


  //-----------------------------------------------------------------------------------
  public Figure( EType eType )
  {
    InitFigures();

    m_eType = eType;
  }

  //-----------------------------------------------------------------------------------
  public void Rotate(int nDelta = 1)
  {
    m_nCurRot += nDelta;
    if (m_nCurRot > 3)
      m_nCurRot = 0;
    else if (m_nCurRot < 0)
      m_nCurRot = 3;
  }

  //-----------------------------------------------------------------------------------
  public VecInt2 GetCenterPoint()
  {
    var curMtx = Matrix;
    return new VecInt2(curMtx.GetLength(0)/2, curMtx.GetLength(1)/2);
  }


  //-----------------------------------------------------------------------------------
  static void InitFigures()
  {
    if (s_arMatrixByTypeNRot != null)
      return;

    s_arMatrixByTypeNRot = new int [(int)EType.COUNT, 4][,];

    for (int i = 0; i < (int)EType.COUNT; i++)
    {
      int[,] newMtx;
      switch ((EType)i)
      {
      case EType.BOX:
        {
          newMtx = new int[,]
            { {1,1}
            , {1,1} };
        }
        break;
      case EType.LINE:
        {
          newMtx = new int[,]
            { {1,1,1,1} };
        }
        break;
      case EType.L:
        {
          newMtx = new int[,]
            { {1,1,1}
            , {1,0,0} };
        }
        break;
      case EType.L_INV:
        {
          newMtx = new int[,]
            { {1,1,1}
            , {0,0,1} };
        }
        break;
      case EType.S:
        {
          newMtx = new int[,]
            { {0,1,1}
            , {1,1,0} };
        }
        break;
      case EType.S_INV:
        {
          newMtx = new int[,]
            { {1,1,0}
            , {0,1,1} };
        }
        break;
      case EType.T:
        {
          newMtx = new int[,]
            { {1,1,1}
            , {0,1,0} };
        }
        break;
      default:
        newMtx = null;
        break;
      }

      var curMtx = newMtx;
      for (int j = 0; j < 4; j++)
      {
        s_arMatrixByTypeNRot[i, j] = curMtx;

        int nCol = curMtx.GetLength(0);
        int nRow = curMtx.GetLength(1);
        var arNew = new int[nRow, nCol];
        for (int c = 0; c < nCol; c++)
        {
          for (int r = 0; r < nRow; r++)
          {
            arNew[nRow - 1 - r, c] = curMtx[c, r];
          }
        }
        curMtx = arNew;
      }
      
    }
  }
}