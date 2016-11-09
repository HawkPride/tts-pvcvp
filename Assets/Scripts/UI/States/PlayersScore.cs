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

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.PLAYER_SCORE;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      if (Game.Instance.GameResults == null)
      {
        StatsProviderBase sp = new StatsProviderLocal();
        sp.Load();
        var res = sp.GetCurStats(0, 7);
        for (int i = 0; i < res.Count; i++)
        {
          PlayersScoreItem item = m_scrollView.AddListItem().GetComponent<PlayersScoreItem>();
          item.Set(res[i]);
        }
      }
      else
      {
        //Add new results
      }
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {


    }

    //-----------------------------------------------------------------------------------
    public void OnButtonOk()
    {

      Game.Instance.Ui.SwitchToState(new MainMenuParams());
    }
  }

}