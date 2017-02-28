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

  [Serializable]
  public class Config
  {
    public string m_strPlayerName = System.String.Empty;
    public int    m_nGamesPlayed  = 0;
    public int    m_nPvpRating    = 0;
  }


  public abstract Config      GetConfig       ();

  public abstract List<Stats> GetCurStats     (int nStartIndex, int nCount);

  public abstract bool        AddStats        (Stats stats);

  public abstract int         GetNewScoreIndex(int nScore);

  public abstract bool        Load            ();

  public abstract bool        Save            ();
}
