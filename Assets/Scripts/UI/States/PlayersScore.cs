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
        //Show the stored score
        for (int i = 0; i < 10; i++)
        {
          StatsProviderBase.Stats rec;
          rec.m_strPlayerName = "blah";
          rec.m_nScore = i * 10;

          PlayersScoreItem item = m_scrollView.AddListItem().GetComponent<PlayersScoreItem>();
          if (i % 2 == 0)
            item.Set(rec);
          else
            item.SetAsInput(rec);
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