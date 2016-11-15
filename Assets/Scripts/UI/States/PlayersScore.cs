using UnityEngine;
using System.Collections;

namespace GUI.States
{
  //Creation params
  public class PlayersScoreParams : GameStateParams
  {
    public override string GetSceneName() { return "PlayerScore"; }
    public override EGameStateType GetStateType() { return EGameStateType.PLAYER_SCORE; }
    public PlayersScoreParams(int nScore) { m_nScore = nScore; }

    int m_nScore = -1;
  }


  public class PlayersScore : GameState
  {
    public DynamicScrollView m_scrollView;


    int               m_nInputIndex = -1;
    StatsProviderBase m_statsProv = null;

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.PLAYER_SCORE;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      m_statsProv = new StatsProviderLocal();
      m_statsProv.Load();
      if (Game.Instance.Results == null)
      {
        m_nInputIndex = -1;
        var res = m_statsProv.GetCurStats(0, 10);
        for (int i = 0; i < res.Count; i++)
        {
          PlayersScoreItem item = m_scrollView.AddListItem<PlayersScoreItem>();
          item.Set(res[i]);
        }
      }
      else
      {
        GameResults gameRes = Game.Instance.Results;
        //Add new results
        m_nInputIndex = m_statsProv.GetNewScoreIndex(gameRes.Score);
        //The results in high score
        if (m_nInputIndex >= 0)
        {
          var res = m_statsProv.GetCurStats(0, 10);
          for (int i = 0; i < res.Count; i++)
          {
            var item = m_scrollView.AddListItem<PlayersScoreItem>();
            int nElIdx = i < m_nInputIndex ? i : i - 1;
            if (i == m_nInputIndex)
            {
              var newRes = new StatsProviderBase.Stats();
              newRes.m_nScore = gameRes.Score;
              newRes.m_strPlayerName = "Enter Name";
              item.SetAsInput(newRes);
            }
            else
              item.Set(res[nElIdx]);
          }
        }
        else
          Game.Instance.Ui.SwitchToState(new MainMenuParams());
      }
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {


    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {
    }

    //-----------------------------------------------------------------------------------
    public void OnButtonOk()
    {
      if (m_nInputIndex != -1)
      {
        var item = m_scrollView.GetListItem<PlayersScoreItem>(m_nInputIndex);
        var newRes = new StatsProviderBase.Stats();
        newRes.m_strPlayerName = item.GetName();
        newRes.m_nScore = Game.Instance.Results.Score;
        if (m_statsProv.AddStats(newRes))
          m_statsProv.Save();
      }
      m_nInputIndex = -1;
      Game.Instance.Results = null;

      Game.Instance.Ui.SwitchToState(new MainMenuParams());
    }
  }

}