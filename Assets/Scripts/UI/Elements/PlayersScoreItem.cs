using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GUI
{

  public class PlayersScoreItem : MonoBehaviour
  {
    public Text       m_name;
    public Text       m_score;
    public InputField m_input;

    //-----------------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {

    }

    //-----------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {

    }

    //-----------------------------------------------------------------------------------
    public void Set(StatsProviderBase.Stats stats)
    {
      m_name.text = stats.m_strPlayerName;
      m_score.text = stats.m_nScore.ToString();
      m_input.gameObject.SetActive(false);
    }

    //-----------------------------------------------------------------------------------
    public void SetAsInput(StatsProviderBase.Stats defStats)
    {
      ((RectTransform)m_input.textComponent.transform).pivot = new Vector2(0, 1.2f);
      m_input.text = defStats.m_strPlayerName;
      m_score.text = defStats.m_nScore.ToString();
      m_name.gameObject.SetActive(false);
    }

    //-----------------------------------------------------------------------------------
    public string GetName()
    {
      return m_input.text;
    }
  }

}