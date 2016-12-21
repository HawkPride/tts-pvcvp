using System;
using System.Collections.Generic;

public abstract class StatsProviderBase
{
  [Serializable]
  public struct Stats
  {
    public string  m_strPlayerName;
    public int     m_nScore;
  };

  public int gamesPlayed { get; set; }

  public abstract List<Stats> GetCurStats     (int nStartIndex, int nCount);

  public abstract bool        AddStats        (Stats stats);

  public abstract int         GetNewScoreIndex(int nScore);

  public abstract bool        Load            ();

  public abstract bool        Save            ();
}
