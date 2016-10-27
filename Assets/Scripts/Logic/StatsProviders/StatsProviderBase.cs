using System;

public abstract class StatsProviderBase
{
  [Serializable]
  public struct Stats
  {
    public string  m_strPlayerName;
    public int     m_nScore;
  };

  public abstract Stats[]   GetCurStats (int nStartIndex, int nCount);
  public abstract void      AddStats    (Stats stats);
}
