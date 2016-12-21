using UnityEngine;
using System.Collections;

namespace GameGUI.States
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
    GameResults       m_gameResults = null;

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.PLAYER_SCORE;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      StatsProviderBase sp = Game.instance.stats;
      if (Game.instance.results == null)
      {
        m_nInputIndex = -1;
        var res = sp.GetCurStats(0, 10);
        for (int i = 0; i < res.Count; i++)
        {
          PlayersScoreItem item = m_scrollView.AddListItem<PlayersScoreItem>();
          item.Set(res[i]);
        }
      }
      else
      {
        m_gameResults = Game.instance.results;
        //Clear results
        Game.instance.results = null;
        //Add new results
        m_nInputIndex = sp.GetNewScoreIndex(m_gameResults.score);
        //The results in high score
        if (m_nInputIndex >= 0)
        {
          var res = sp.GetCurStats(0, 10);
          for (int i = 0; i < res.Count + 1; i++)
          {
            var item = m_scrollView.AddListItem<PlayersScoreItem>();
            int nElIdx = i < m_nInputIndex ? i : i - 1;
            if (i == m_nInputIndex)
            {
              var newRes = new StatsProviderBase.Stats();
              newRes.m_nScore = m_gameResults.score;
              newRes.m_strPlayerName = "Enter Name";
              item.SetAsInput(newRes);
            }
            else
              item.Set(res[nElIdx]);
          }
        }
        else
          Game.instance.ui.SwitchToState(new MainMenuParams());
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
        newRes.m_nScore = m_gameResults.score;
        StatsProviderBase sp = Game.instance.stats;
        if (sp.AddStats(newRes))
          sp.Save();
      }
      m_nInputIndex = -1;
      Game.instance.results = null;

      Game.instance.ui.SwitchToState(new MainMenuParams());
    }
  }

}