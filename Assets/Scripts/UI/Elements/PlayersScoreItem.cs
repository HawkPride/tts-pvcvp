using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GUI
{

  public class PlayersScoreItem : MonoBehaviour
  {
    public Text m_name;
    public Text m_score;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Set(StatsProviderBase.Stats stats)
    {
      m_name.text = stats.m_strPlayerName;
      m_score.text = stats.m_nScore.ToString();
    }
  }

}