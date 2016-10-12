using System;


public class StatsProviderLocal
  : StatsProviderBase
{
  private const int MAX_STORED_RECORDS = 10;

  //-----------------------------------------------------------------------------------
  override public Stats[] GetCurStats(int nStartIndex, int nCount)
  {
    var res = new Stats[256];
    int nSize = res.GetLength(0);
    if (nCount > MAX_STORED_RECORDS)
      nCount = MAX_STORED_RECORDS;
    Console.WriteLine("size " + nSize.ToString() + " " + nCount.ToString());
    return null;
  }

  //-----------------------------------------------------------------------------------
  override public void AddStats(Stats stats)
  {

  }
}
