using System;

public abstract class StatsProviderBase
{
  [Serializable]
  public struct Stats
  {
    public string  m_strPlayerName;
    public int     m_nScore;
  };

  abstract public Stats[]   GetCurStats (int nStartIndex, int nCount);
  abstract public void      AddStats    (Stats stats);
}
