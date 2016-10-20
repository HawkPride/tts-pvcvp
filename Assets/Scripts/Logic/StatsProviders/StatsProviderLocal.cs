using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class StatsProviderLocal
  : StatsProviderBase
{
  //Private
  const int MAX_STORED_RECORDS = 10;

  Stats[]   m_arCurrRecords = new Stats[MAX_STORED_RECORDS];

  //-----------------------------------------------------------------------------------
  override public Stats[] GetCurStats(int nStartIndex, int nCount)
  {
    //var res = new Stats[256];
    //int nSize = res.GetLength(0);
    if (nCount > MAX_STORED_RECORDS)
      nCount = MAX_STORED_RECORDS;
    
    return m_arCurrRecords;
  }

  //-----------------------------------------------------------------------------------
  override public void AddStats(Stats stats)
  {
    Load();
    Array.Sort(m_arCurrRecords, (x, y) => 
      (
        x.m_nScore < y.m_nScore ? -1 : 1 
      )
    );
    m_arCurrRecords[0] = stats;
    Save();
  }

  //-----------------------------------------------------------------------------------
  void Save()
  {
    string strFile = GetFileName();
    FileStream fs = File.Open(strFile, FileMode.OpenOrCreate);
    BinaryFormatter bf = new BinaryFormatter();
    bf.Serialize(fs, m_arCurrRecords);
    fs.Close();
  }


  //-----------------------------------------------------------------------------------
  void Load()
  {
    string strFile = GetFileName();

    FileStream fs = File.Open(strFile, FileMode.OpenOrCreate);
    BinaryFormatter bf = new BinaryFormatter();
    try
    {
      m_arCurrRecords = (Stats[])bf.Deserialize(fs);
    }
    catch (SerializationException)
    {
      m_arCurrRecords = new Stats[MAX_STORED_RECORDS];
    }
    fs.Close();
  }

  //-----------------------------------------------------------------------------------
  string GetFileName()
  {
    return Application.persistentDataPath + "/stats.dat";
  }
}
