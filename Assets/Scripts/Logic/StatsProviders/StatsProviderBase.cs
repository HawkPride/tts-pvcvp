using System;

public abstract class StatsProviderBase
{
  public struct Stats
  {
    string  m_strPlayerName;
    int     m_nScore;
  };

  abstract public Stats[]   GetCurStats (int nStartIndex, int nCount);
  abstract public void      AddStats    (Stats stats);
}
