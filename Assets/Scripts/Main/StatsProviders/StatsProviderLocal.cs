using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class StatsProviderLocal
  : StatsProviderBase
{
  //Private
  const int MAX_STORED_RECORDS = 10;

  List<Stats>   m_arCurrRecords = new List<Stats>();

  //-----------------------------------------------------------------------------------
  public override List<Stats> GetCurStats(int nStartIndex, int nCount)
  {
    return m_arCurrRecords;
  }

  //-----------------------------------------------------------------------------------
  public override bool AddStats(Stats stats)
  {
    //Stats always sorted, find first with the lower score
    int nIndex = m_arCurrRecords.FindIndex( (x) => ( x.m_nScore <= stats.m_nScore ) );

    if (nIndex == -1)
    {
      if (m_arCurrRecords.Count > 0)
      {
        //No lower result was found, insert to the end
        nIndex = m_arCurrRecords.Count;
      }
      else
      {
        //If the list is empty, insert anyway
        nIndex = 0;
      }
    }
    m_arCurrRecords.Insert(nIndex, stats);

    int nTotalItems = m_arCurrRecords.Count;
    if (nTotalItems > MAX_STORED_RECORDS)
      m_arCurrRecords.RemoveRange(MAX_STORED_RECORDS, nTotalItems - MAX_STORED_RECORDS);
    return true;
  }

  //-----------------------------------------------------------------------------------
  public override bool Save()
  {
    string strFile = GetFileName();
    FileStream fs = File.Open(strFile, FileMode.OpenOrCreate);
    BinaryFormatter bf = new BinaryFormatter();
    bf.Serialize(fs, m_arCurrRecords);
    fs.Close();
    return true;
  }


  //-----------------------------------------------------------------------------------
  public override bool Load()
  {
    string strFile = GetFileName();

    FileStream fs = File.Open(strFile, FileMode.OpenOrCreate);
    BinaryFormatter bf = new BinaryFormatter();
    try
    {
      m_arCurrRecords = (List<Stats>)bf.Deserialize(fs);
    }
    catch (SerializationException)
    {
      m_arCurrRecords = new List<Stats>();
    }
    fs.Close();

    //Keep stats sorted
    Comparison<Stats> f = (x, y) => (  x.m_nScore < y.m_nScore ? 1 : -1 );
    m_arCurrRecords.Sort(f);

    return true;
  }

  //-----------------------------------------------------------------------------------
  string GetFileName()
  {
    return Application.persistentDataPath + "/stats.dat";
  }
}
